using SyntaxGenerator.Model;

namespace SyntaxGenerator;

class SourceWriter(TextWriter writer, Tree tree, CancellationToken cancellationToken = default)
    : AbstractFileWriter(writer, tree, cancellationToken) {
    public static void WriteMain(TextWriter writer, Tree tree, CancellationToken cancellationToken = default) =>
        new SourceWriter(writer, tree, cancellationToken).WriteMain();

    public static void WriteSyntax(TextWriter writer, Tree tree, CancellationToken cancellationToken = default) =>
        new SourceWriter(writer, tree, cancellationToken).WriteSyntax();

    void WriteFileHeader() {
        WriteLine("// <auto-generated />");
        WriteLine();
        WriteLine("#nullable enable");
        WriteLine();
        WriteLine("using System;");
        WriteLine("using System.Collections.Generic;");
        WriteLine("using Vixen.Raven.Syntax;");
        WriteLine();
    }

    void WriteSyntax() {
        WriteFileHeader();
        WriteLine("namespace Vixen.Raven;");
        WriteTypes();
    }

    void WriteMain() {
        WriteFileHeader();
        WriteLine("namespace Vixen.Raven;");

        WriteVisitor(true);
        WriteVisitor(false);
        WriteRewriter();
        WriteStaticFactories();
    }

    void WriteTypes() {
        var nodes = Tree.Types.Where(n => n is not PredefinedNode).ToList();
        foreach (var node in nodes) {
            WriteLine();
            WriteType(node);
        }
    }

    List<Field> GetNodeOrNodeListFields(TreeType node) =>
        node switch {
            AbstractNode an => an.Fields.Where(n => IsNodeOrNodeList(n.Type)).ToList(),
            Node nd => nd.Fields.Where(n => IsNodeOrNodeList(n.Type)).ToList(),
            _ => []
        };

    bool IsRequiredFactoryField(Node node, Field field) =>
        (!field.IsOptional && !IsAnyList(field.Type) && !CanBeAutoCreated(node, field)) || IsValueField(field);

    bool CanBeAutoCreated(Node node, Field field) => IsAutoCreatableToken(node, field) || IsAutoCreatableNode(field);

    bool IsAutoCreatableToken(Node node, Field field) =>
        field is { Type: "SyntaxToken", Kinds: not null }
        && ((field.Kinds.Count == 1
                && field.Kinds[0].Name != "IdentifierToken"
                && !field.Kinds[0].Name!.EndsWith("LiteralToken", StringComparison.Ordinal))
            || (field.Kinds.Count > 1 && field.Kinds.Count == node.Kinds.Count));

    bool IsAutoCreatableNode(Field field) {
        var referencedNode = GetNode(field.Type);
        return referencedNode != null && RequiredFactoryArgumentCount(referencedNode) == 0;
    }

    int RequiredFactoryArgumentCount(Node nd, bool includeKind = true) {
        var count = 0;

        // kind must be specified in the factory
        if (nd.Kinds.Count > 1 && includeKind) {
            count++;
        }

        count += nd.Fields.Count(field => IsRequiredFactoryField(nd, field));
        return count;
    }

    Node? TryGetNodeForNestedList(Field field) {
        var referencedNode = GetNode(field.Type);
        if (referencedNode != null && (!field.IsOptional || RequiredFactoryArgumentCount(referencedNode) == 0)) {
            return referencedNode;
        }

        return null;
    }

    string GetRedPropertyType(Field field) {
        if (field.Type == "SyntaxList<SyntaxToken>") {
            return "SyntaxTokenList";
        }

        if (field.IsOptional && IsNode(field.Type) && field.Type != "SyntaxToken") {
            return field.Type + "?";
        }

        return field.Type;
    }

    void WriteType(TreeType node) {
        WriteComment(node.TypeComment, "");

        if (node is AbstractNode abstractNode) {
            Write($"public abstract partial class {abstractNode.Name} : {abstractNode.Base}");
            OpenBlock();

            // ctor with diagnostics and annotations
            // TODO: not supported, yet
            // WriteLine(
            //     $"internal {node.Name}(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations)"
            // );
            // WriteLine("  : base(kind, diagnostics, annotations)");
            // OpenBlock();
            // if (node.Name == "DirectiveTriviaSyntax") {
            //     WriteLine("SetFlags(NodeFlags.ContainsDirectives);");
            // }
            //
            // CloseBlock();
            // WriteLine();

            // ctor without diagnostics and annotations
            Write($"internal {abstractNode.Name}(SyntaxKind kind) : base(kind)");
            if (abstractNode.Name == "DirectiveTriviaSyntax") {
                OpenBlock();
                WriteLine("SetFlags(NodeFlags.ContainsDirectives);");
                CloseBlock();
            } else {
                WriteLine(" { }");
            }

            var valueFields = abstractNode.Fields.Where(n => !IsNodeOrNodeList(n.Type)).ToList();
            var nodeFields = abstractNode.Fields.Where(n => IsNodeOrNodeList(n.Type)).ToList();

            foreach (var field in nodeFields) {
                if (IsNodeOrNodeList(field.Type)) {
                    WriteLine();
                    WriteComment(field.PropertyComment, "");

                    // TODO: verify this
                    if (IsSeparatedNodeList(field.Type) || IsNodeList(field.Type)) {
                        WriteLine(
                            $"public abstract {(field.IsNew ? "new " : "")}CoreSyntax.{field.Type} {field.Name} {{ get; }}"
                        );
                    } else {
                        WriteLine(
                            $"public abstract {(field.IsNew ? "new " : "")}{GetFieldType(field, true)} {field.Name} {{ get; }}"
                        );
                    }


                    // TODO: merging this

                    WriteLine();
                    WriteLine(
                        $"public {node.Name} With{field.Name}({field.Type} {CamelCase(field.Name)}) => With{field.Name}Core({CamelCase(field.Name)});"
                    );
                    WriteLine(
                        $"internal abstract {node.Name} With{field.Name}Core({field.Type} {CamelCase(field.Name)});"
                    );

                    if (IsAnyList(field.Type)) {
                        var argType = GetElementType(field.Type);
                        WriteLine();
                        WriteLine(
                            $"public {node.Name} Add{field.Name}(params {argType}[] items) => Add{field.Name}Core(items);"
                        );
                        WriteLine($"internal abstract {node.Name} Add{field.Name}Core(params {argType}[] items);");
                    } else {
                        var referencedNode = TryGetNodeForNestedList(field);
                        if (referencedNode != null) {
                            foreach (var referencedNodeField in referencedNode.Fields) {
                                if (IsAnyList(referencedNodeField.Type)) {
                                    var argType = GetElementType(referencedNodeField.Type);

                                    WriteLine();
                                    WriteLine(
                                        $"public {node.Name} Add{StripPost(field.Name, "Opt")}{referencedNodeField.Name}(params {argType}[] items) => Add{StripPost(field.Name, "Opt")}{referencedNodeField.Name}Core(items);"
                                    );
                                    WriteLine(
                                        $"internal abstract {node.Name} Add{StripPost(field.Name, "Opt")}{referencedNodeField.Name}Core(params {argType}[] items);"
                                    );
                                }
                            }
                        }
                    }
                }
            }

            foreach (var field in valueFields) {
                WriteLine();
                WriteComment(field.PropertyComment, "");
                WriteLine($"public abstract {(field.IsNew ? "new " : "")}{field.Type} {field.Name} {{ get; }}");
            }

            CloseBlock();
        } else if (node is Node nNode) {
            WriteComment("<remarks>");
            WriteComment("<para>This node is associated with the following syntax kinds:</para>");
            WriteComment("<list type=\"bullet\">");

            foreach (var kind in nNode.Kinds) {
                WriteComment($"<item><description><see cref=\"SyntaxKind.{kind.Name}\"/></description></item>");
            }

            WriteComment("</list>");
            WriteComment("</remarks>");

            Write($"public sealed partial class {nNode.Name} : {nNode.Base}");
            OpenBlock();

            var valueFields = nNode.Fields.Where(n => !IsNodeOrNodeList(n.Type)).ToList();
            var nodeFields = nNode.Fields.Where(n => IsNodeOrNodeList(n.Type)).ToList();

            foreach (var field in nodeFields) {
                var type = GetFieldType(field, true);
                WriteLine($"internal readonly {type} {CamelCase(field.Name)};");
            }

            foreach (var field in valueFields) {
                WriteLine($"internal readonly {field.Type} {CamelCase(field.Name)};");
            }

            // TODO: not supported, yet
            // write a constructor with diagnostics and annotations
            // WriteLine();
            // Write($"internal {node.Name}(SyntaxKind kind");
            //
            // WriteGreenNodeConstructorArgs(nodeFields, valueFields);
            //
            // WriteLine(", DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations)");
            // WriteLine("  : base(kind, diagnostics, annotations)");
            // OpenBlock();
            // WriteCtorBody(nd, valueFields, nodeFields);
            // CloseBlock();

            // TODO: not supported either
            // write constructor with async
            // WriteLine();
            // Write($"internal {node.Name}(SyntaxKind kind");
            //
            // WriteGreenNodeConstructorArgs(nodeFields, valueFields);
            //
            // WriteLine(", SyntaxFactoryContext context)");
            // WriteLine("  : base(kind)");
            // OpenBlock();
            // WriteLine("this.SetFactoryContext(context);");
            // WriteCtorBody(nd, valueFields, nodeFields);
            // CloseBlock();

            // write constructor without diagnostics and annotations
            WriteLine();
            Write($"internal {nNode.Name}(SyntaxKind kind");

            WriteNodeConstructorArgs(nodeFields, valueFields);

            WriteLine(")");
            Write("  : base(kind)");
            OpenBlock();
            WriteCtorBody(nNode, valueFields, nodeFields);
            CloseBlock();
            WriteLine();

            // property accessors
            foreach (var field in nodeFields) {
                WriteComment(field.PropertyComment, "");
                if (IsNodeList(field.Type)) {
                    var type = $"CoreSyntax.{field.Type}";
                    WriteLine(
                        $"public {OverrideOrNewModifier(field)}{type} {field.Name} => new {type}(this.{CamelCase(field.Name)});"
                    );
                } else if (IsSeparatedNodeList(field.Type)) {
                    var type = $"CoreSyntax.{field.Type}";
                    WriteLine(
                        $"public {OverrideOrNewModifier(field)}{type} {field.Name} => new {type}(new CoreSyntax.SyntaxList<CSharpSyntaxNode>(this.{CamelCase(field.Name)}));"
                    );
                } else if (field.Type == "SyntaxNodeOrTokenList") {
                    const string type = "CoreSyntax.SyntaxList<CSharpSyntaxNode>";
                    WriteLine(
                        $"public {OverrideOrNewModifier(field)}{type} {field.Name} => new {type}(this.{CamelCase(field.Name)});"
                    );
                } else {
                    WriteLine(
                        $"public {OverrideOrNewModifier(field)}{GetFieldType(field, true)} {field.Name} => this.{CamelCase(field.Name)};"
                    );
                }
            }

            foreach (var field in valueFields) {
                WriteComment(field.PropertyComment, "");
                WriteLine(
                    $"public {OverrideOrNewModifier(field)}{field.Type} {field.Name} => this.{CamelCase(field.Name)};"
                );
            }

            // GetSlot
            WriteLine();
            Write("public override SyntaxNode? GetSlot(int index)");

            if (nodeFields.Count == 0) {
                WriteLine(" => null;");
            } else if (nodeFields.Count == 1) {
                WriteLine();
                Indent();
                WriteLine($"=> index == 0 ? this.{CamelCase(nodeFields[0].Name)} : null;");
                Unindent();
            } else {
                WriteLine();
                Indent();
                Write("=> index switch");
                OpenBlock();
                for (int i = 0, n = nodeFields.Count; i < n; i++) {
                    var field = nodeFields[i];
                    WriteLine($"{i} => this.{CamelCase(field.Name)},");
                }

                WriteLine("_ => null");
                CloseBlock(";");
                Unindent();
            }

            WriteAcceptMethods(nNode);
            WriteUpdateMethod(nNode);
            // TODO: not supported yet
            // this.WriteSetDiagnostics(nd);
            // this.WriteSetAnnotations(nd);
            WriteWithMethods(nNode);
            WriteListHelperMethods(nNode);

            CloseBlock();
        }
    }

    void WriteListHelperMethods(Node node) {
        var wroteNewLine = false;
        foreach (var field in node.Fields) {
            if (IsAnyList(field.Type)) {
                if (!wroteNewLine) {
                    WriteLine();
                    wroteNewLine = true;
                }

                // write list helper methods for list properties
                WriteListHelperMethods(node, field);
            } else {
                var referencedNode = TryGetNodeForNestedList(field);
                if (referencedNode != null) {
                    // look for list members...
                    foreach (var referencedNodeField in referencedNode.Fields) {
                        if (IsAnyList(referencedNodeField.Type)) {
                            if (!wroteNewLine) {
                                WriteLine();
                                wroteNewLine = true;
                            }

                            WriteNestedListHelperMethods(node, field, referencedNode, referencedNodeField);
                        }
                    }
                }
            }
        }
    }

    void WriteListHelperMethods(Node node, Field field) {
        var argType = GetElementType(field.Type);

        var isNew = false;
        if (field.IsOverride) {
            var (baseType, baseField) = GetHighestBaseTypeWithField(node, field.Name);
            if (baseType != null) {
                var baseArgType = GetElementType(baseField!.Type);
                WriteLine(
                    $"internal override {baseType.Name} Add{field.Name}Core(params {baseArgType}[] items) => Add{field.Name}(items);"
                );
                isNew = true;
            }
        }

        WriteLine(
            $"public{(isNew ? " new " : " ")}{node.Name} Add{field.Name}(params {argType}[] items) => With{StripPost(field.Name, "Opt")}(this.{field.Name}.AddRange(items));"
        );
    }

    void WriteNestedListHelperMethods(Node node, Field field, Node referencedNode, Field referencedNodeField) {
        var argType = GetElementType(referencedNodeField.Type);

        var isNew = false;
        if (field.IsOverride) {
            var (baseType, _) = GetHighestBaseTypeWithField(node, field.Name);
            if (baseType != null) {
                WriteLine(
                    $"internal override {baseType.Name} Add{StripPost(field.Name, "Opt")}{referencedNodeField.Name}Core(params {argType}[] items) => Add{StripPost(field.Name, "Opt")}{referencedNodeField.Name}(items);"
                );
                isNew = true;
            }
        }

        // AddBaseListTypes
        Write(
            $"public{(isNew ? " new " : " ")}{node.Name} Add{StripPost(field.Name, "Opt")}{referencedNodeField.Name}(params {argType}[] items)"
        );

        if (field.IsOptional) {
            WriteLine();
            OpenBlock();
            var factoryName = StripPost(referencedNode.Name, "Syntax");
            var varName = StripPost(CamelCase(field.Name), "Opt");
            WriteLine($"var {varName} = this.{field.Name} ?? SyntaxFactory.{factoryName}();");
            WriteLine(
                $"return With{StripPost(field.Name, "Opt")}({varName}.With{StripPost(referencedNodeField.Name, "Opt")}({varName}.{referencedNodeField.Name}.AddRange(items)));"
            );
            CloseBlock();
        } else {
            WriteLine(
                $" => With{StripPost(field.Name, "Opt")}(this.{field.Name}.With{StripPost(referencedNodeField.Name, "Opt")}(this.{field.Name}.{referencedNodeField.Name}.AddRange(items)));"
            );
        }
    }

    void WriteWithMethods(Node node) {
        foreach (var field in node.Fields) {
            var type = GetRedPropertyType(field);

            if (field == node.Fields.First()) {
                WriteLine();
            }

            var isNew = false;
            if (field.IsOverride) {
                var (baseType, baseField) = GetHighestBaseTypeWithField(node, field.Name);
                if (baseType != null) {
                    Write(
                        $"internal override {baseType.Name} With{field.Name}Core({GetRedPropertyType(baseField!)} {CamelCase(field.Name)}) => With{field.Name}({CamelCase(field.Name)}"
                    );

                    if (baseField!.Type != "SyntaxToken" && baseField.IsOptional && !field.IsOptional) {
                        Write($" ?? throw new ArgumentNullException(nameof({CamelCase(field.Name)}))");
                    }

                    WriteLine(");");
                    isNew = true;
                }
            }

            Write(
                $"public{(isNew ? " new " : " ")}{node.Name} With{StripPost(field.Name, "Opt")}({type} {CamelCase(field.Name)})"
                + " => Update("
            );

            // call update inside each setter
            Write(CommaJoin(node.Fields.Select(f => f == field ? CamelCase(f.Name) : $"this.{f.Name}")));
            WriteLine(");");
        }
    }

    (TreeType? type, Field? field) GetHighestBaseTypeWithField(TreeType node, string name) {
        TreeType? bestType = null;
        Field? bestField = null;

        for (var current = node; current != null; current = TryGetBaseType(current)) {
            var fields = GetNodeOrNodeListFields(current);
            var field = fields.FirstOrDefault(f => f.Name == name);

            if (field != null) {
                bestType = current;
                bestField = field;
            }
        }

        return (bestType, bestField);
    }

    TreeType? TryGetBaseType(TreeType node) =>
        node switch {
            AbstractNode an => GetTreeType(an.Base),
            Node n => GetTreeType(n.Base),
            _ => null
        };

    void WriteUpdateMethod(Node node) {
        WriteLine();
        Write($"public {node.Name} Update(");
        Write(
            CommaJoin(
                node.Fields.Select(
                    f => {
                        var type =
                            f.Type == "SyntaxNodeOrTokenList" ? "CoreSyntax.SyntaxList<CSharpSyntaxNode>" :
                            f.Type == "SyntaxTokenList" ? "CoreSyntax.SyntaxList<SyntaxToken>" :
                            IsNodeList(f.Type) ? "CoreSyntax." + f.Type :
                            IsSeparatedNodeList(f.Type) ? "CoreSyntax." + f.Type :
                            f.Type;

                        return $"{type} {CamelCase(f.Name)}";
                    }
                )
            )
        );
        Write(")");
        OpenBlock();

        Write("if (");
        var nCompared = 0;
        foreach (var field in node.Fields) {
            if (
                IsDerivedOrListOfDerived("SyntaxNode", field.Type)
                || IsDerivedOrListOfDerived("SyntaxToken", field.Type)
                || field.Type == "SyntaxNodeOrTokenList"
            ) {
                if (nCompared > 0) {
                    Write(" || ");
                }

                Write($"{CamelCase(field.Name)} != {field.Name}");
                nCompared++;
            }
        }

        if (nCompared > 0) {
            Write(")");
            OpenBlock();
            Write($"var newNode = SyntaxFactory.{StripPost(node.Name, "Syntax")}(");
            Write(
                CommaJoin(
                    node.Kinds.Count > 1 ? "this.Kind" : "",
                    node.Fields.Select(f => CamelCase(f.Name))
                )
            );
            WriteLine(");");

            // WriteLine("var diags = GetDiagnostics();");
            // WriteLine("if (diags?.Length > 0)");
            // WriteLine("    newNode = newNode.WithDiagnosticsGreen(diags);");
            // WriteLine("var annotations = GetAnnotations();");
            // WriteLine("if (annotations?.Length > 0)");
            // WriteLine("    newNode = newNode.WithAnnotationsGreen(annotations);");
            WriteLine("return newNode;");
            CloseBlock();
        }

        WriteLine();
        WriteLine("return this;");
        CloseBlock();
    }

    void WriteAcceptMethods(Node node) {
        WriteLine();
        WriteLine(
            $"public override void Accept(SyntaxVisitor visitor) => visitor.Visit{StripPost(node.Name, "Syntax")}(this);"
        );

        // TODO: Fix nullable
        WriteLine(
            $"public override TResult Accept<TResult>(SyntaxVisitor<TResult> visitor) => visitor.Visit{StripPost(node.Name, "Syntax")}(this);"
        );
    }

    void WriteNodeConstructorArgs(List<Field> nodeFields, List<Field> valueFields) {
        foreach (var field in nodeFields) {
            Write($", {GetFieldType(field, true)} {CamelCase(field.Name)}");
        }

        foreach (var field in valueFields) {
            Write($", {field.Type} {CamelCase(field.Name)}");
        }
    }

    void WriteCtorBody(Node node, List<Field> valueFields, List<Field> nodeFields) {
        if (node.Name == "AttributeSyntax") {
            WriteLine("SetFlags(NodeFlags.ContainsAttributes);");
        }

        // constructor body
        WriteLine($"this.SlotCount = {nodeFields.Count};");

        foreach (var field in nodeFields) {
            if (IsAnyList(field.Type) || field.IsOptional) {
                WriteLine($"if ({CamelCase(field.Name)} != null)");
                OpenBlock();
                WriteLine($"this.AdjustFlagsAndWidth({CamelCase(field.Name)});");
                WriteLine($"this.{CamelCase(field.Name)} = {CamelCase(field.Name)};");
                CloseBlock();
            } else {
                // TODO?
                // WriteLine($"this.AdjustFlagsAndWidth({CamelCase(field.Name)});");
                WriteLine($"this.{CamelCase(field.Name)} = {CamelCase(field.Name)};");
            }
        }

        foreach (var field in valueFields) {
            WriteLine($"this.{CamelCase(field.Name)} = {CamelCase(field.Name)};");
        }
    }

    void WriteVisitor(bool genericResult) {
        var nodes = Tree.Types.Where(n => n is not PredefinedNode).ToList();

        WriteLine();
        Write("public abstract partial class SyntaxVisitor" + (genericResult ? "<TResult>" : ""));
        OpenBlock();

        foreach (var node in nodes.OfType<Node>()) {
            WriteLine(
                $"public virtual {(genericResult ? "TResult?" : "void")} Visit{StripPost(node.Name, "Syntax")}({node.Name} node) => this.DefaultVisit(node);"
            );
        }

        CloseBlock();
    }

    void WriteRewriter() {
        var nodes = Tree.Types.Where(n => n is not PredefinedNode).ToList();

        WriteLine();
        Write("public partial class SyntaxRewriter : SyntaxVisitor<SyntaxNode>");
        OpenBlock();

        var first = true;
        foreach (var node in nodes.OfType<Node>()) {
            var nodeFields = node.Fields.Where(nd => IsNodeOrNodeList(nd.Type)).ToList();

            if (!first) {
                WriteLine();
            }

            first = false;

            WriteLine($"public override SyntaxNode Visit{StripPost(node.Name, "Syntax")}({node.Name} node)");
            Indent();

            if (nodeFields.Count == 0) {
                WriteLine("=> node;");
            } else {
                Write("=> node.Update(");
                Write(
                    CommaJoin(
                        node.Fields.Select(
                            f => {
                                if (IsAnyList(f.Type)) {
                                    return $"VisitList(node.{f.Name})";
                                }

                                if (IsNode(f.Type)) {
                                    return $"({f.Type})Visit(node.{f.Name})";
                                }

                                return $"node.{f.Name}";
                            }
                        )
                    )
                );
                WriteLine(");");
            }

            Unindent();
        }

        CloseBlock();
    }

    void WriteComment(string? comment) {
        if (comment != null) {
            var lines = comment.Split(["\r", "\n", "\r\n"], StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l))) {
                WriteLine($"/// {line.TrimStart()}");
            }
        }
    }

    void WriteComment(Comment? comment, string indent) {
        if (comment != null) {
            foreach (var element in comment.Body) {
                var lines = element.OuterXml.Split(["\r", "\n", "\r\n"], StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l))) {
                    WriteLine($"{indent}/// {line.TrimStart()}");
                }
            }
        }
    }

    void WriteStaticFactories() {
        var nodes = Tree.Types.Where(n => n is not PredefinedNode and not AbstractNode).ToList();
        WriteLine();
        Write("public static partial class SyntaxFactory");
        OpenBlock();
        WriteFactories(nodes);
        CloseBlock();
    }

    void WriteFactories(List<TreeType> nodes, bool withSyntaxFactoryContext = false) {
        var first = true;
        foreach (var node in nodes.OfType<Node>()) {
            if (!first) {
                WriteLine();
            }

            first = false;
            WriteFactory(node, withSyntaxFactoryContext);
        }
    }

    void WriteFactory(Node nd, bool withSyntaxFactoryContext = false) {
        var valueFields = nd.Fields.Where(n => !IsNodeOrNodeList(n.Type)).ToList();
        var nodeFields = nd.Fields.Where(n => IsNodeOrNodeList(n.Type)).ToList();

        Write($"public {(withSyntaxFactoryContext ? "" : "static ")}{nd.Name} {StripPost(nd.Name, "Syntax")}(");
        WriteFactoryParameters(nd);
        Write(")");
        OpenBlock();

        // validate kind
        if (nd.Kinds.Count >= 2) {
            WriteLine("switch (kind)");
            OpenBlock();
            var kinds = nd.Kinds.Distinct().ToList();
            foreach (var kind in kinds) {
                WriteLine($"case SyntaxKind.{kind.Name}:{(kind == kinds.Last() ? " break;" : "")}");
            }

            WriteLine("default: throw new ArgumentException(nameof(kind));");
            CloseBlock();
        }

        // validate parameters
        WriteLineWithoutIndent("#if DEBUG");
        foreach (var field in nodeFields) {
            var pName = CamelCase(field.Name);

            if (!IsAnyList(field.Type) && !field.IsOptional) {
                WriteLine($"ArgumentNullException.ThrowIfNull({CamelCase(field.Name)});");
            }

            if (field is { Type: "SyntaxToken", Kinds.Count: > 0 }) {
                if (field.IsOptional) {
                    WriteLine($"if ({CamelCase(field.Name)} != null)");
                    OpenBlock();
                }

                if (field.Kinds.Count == 1 && !field.IsOptional) {
                    WriteLine(
                        $"if ({pName}.Kind != SyntaxKind.{field.Kinds[0].Name}) throw new ArgumentException(nameof({pName}));"
                    );
                } else {
                    Write($"switch ({pName}.Kind)");
                    OpenBlock();
                    var kinds = field.Kinds.Distinct().ToList();

                    //we need to check for Kind=None as well as node == null because that's what the red factory will pass
                    if (field.IsOptional) {
                        kinds.Add(new() { Name = "None" });
                    }

                    foreach (var kind in kinds) {
                        WriteLine($"case SyntaxKind.{kind.Name}:");
                    }

                    Indent();
                    WriteLine("break;");
                    Unindent();

                    WriteLine("default:");
                    Indent();
                    WriteLine($"throw new ArgumentException(nameof({pName}));");
                    Unindent();
                    CloseBlock();
                }

                if (field.IsOptional) {
                    CloseBlock();
                }
            }
        }

        WriteLineWithoutIndent("#endif");

        // TODO: cache not supported now
        if (false
            && nd.Name != "SkippedTokensTriviaSyntax"
            && nd.Name != "DocumentationCommentTriviaSyntax"
            && nd.Name != "IncompleteMemberSyntax"
            && nd.Name != "AttributeSyntax"
            && valueFields.Count + nodeFields.Count <= 3
           ) {
            WriteLine();
            //int hash;
            WriteLine("int hash;");
            //SyntaxNode cached = SyntaxNodeCache.TryGetNode(SyntaxKind.IdentifierName, identifier, this.context, out hash);
            if (withSyntaxFactoryContext) {
                Write("var cached = CSharpSyntaxNodeCache.TryGetNode((int)");
            } else {
                Write("var cached = SyntaxNodeCache.TryGetNode((int)");
            }

            WriteCtorArgList(nd, withSyntaxFactoryContext, valueFields, nodeFields);
            WriteLine(", out hash);");
            //    if (cached != null) return (IdentifierNameSyntax)cached;
            WriteLine($"if (cached != null) return ({nd.Name})cached;");
            WriteLine();

            //var result = new IdentifierNameSyntax(SyntaxKind.IdentifierName, identifier);
            Write($"var result = new {nd.Name}(");
            WriteCtorArgList(nd, withSyntaxFactoryContext, valueFields, nodeFields);
            WriteLine(");");
            //if (hash >= 0)
            WriteLine("if (hash >= 0)");
            //{
            OpenBlock();
            //    SyntaxNodeCache.AddNode(result, hash);
            WriteLine("SyntaxNodeCache.AddNode(result, hash);");
            //}
            CloseBlock();
            WriteLine();

            //return result;
            WriteLine("return result;");
        }

        WriteLine();
        Write($"return new {nd.Name}(");
        WriteCtorArgList(nd, withSyntaxFactoryContext, valueFields, nodeFields);
        WriteLine(");");

        CloseBlock();
    }

    void WriteFactoryParameters(Node nd) {
        Write(
            CommaJoin(
                nd.Kinds.Count > 1 ? "SyntaxKind kind" : "",
                nd.Fields.Select(
                    f => {
                        var type = f.Type switch {
                            "SyntaxNodeOrTokenList" => "CoreSyntax.SyntaxList<CSharpSyntaxNode>",
                            _ when IsSeparatedNodeList(f.Type) || IsNodeList(f.Type) => $"CoreSyntax.{f.Type}",
                            _ => GetFieldType(f, true)
                        };

                        return $"{type} {CamelCase(f.Name)}";
                    }
                )
            )
        );
    }

    void WriteCtorArgList(Node nd, bool withSyntaxFactoryContext, List<Field> valueFields, List<Field> nodeFields) {
        Write(
            CommaJoin(
                nd.Kinds.Count == 1 ? $"SyntaxKind.{nd.Kinds[0].Name}" : "kind",
                nodeFields.Select(
                    f =>
                        f.Type == "SyntaxList<SyntaxToken>" || IsAnyList(f.Type)
                            ? $"{CamelCase(f.Name)}.Node"
                            : CamelCase(f.Name)
                ),
                // values are at the end
                valueFields.Select(f => CamelCase(f.Name)),
                withSyntaxFactoryContext ? "this.context" : ""
            )
        );
    }
}