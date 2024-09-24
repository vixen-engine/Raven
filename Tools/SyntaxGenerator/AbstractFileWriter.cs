using SyntaxGenerator.Model;

namespace SyntaxGenerator;

abstract class AbstractFileWriter {
    const int IndentSize = 4;
    int indentLevel;
    bool needIndent = true;
    readonly IDictionary<string, Node> nodeMap;
    readonly IDictionary<string, TreeType> typeMap;
    readonly IDictionary<string, string> parentMap;
    readonly TextWriter writer;

    protected Tree Tree { get; }
    protected CancellationToken CancellationToken { get; }

    protected AbstractFileWriter(TextWriter writer, Tree tree, CancellationToken cancellationToken) {
        this.writer = writer;
        Tree = tree;
        CancellationToken = cancellationToken;

        nodeMap = tree.Types.OfType<Node>().ToDictionary(n => n.Name);
        typeMap = tree.Types.ToDictionary(n => n.Name);
        parentMap = tree.Types.ToDictionary(n => n.Name, n => n.Base);
        parentMap.Add(tree.Root, null!);
    }

    public static bool IsAnyNodeList(string typeName) => IsNodeList(typeName) || IsSeparatedNodeList(typeName);

    void WriteIndentIfNeeded() {
        if (needIndent) {
            writer.Write(new string(' ', indentLevel * IndentSize));
            needIndent = false;
        }
    }

    static bool IsKeyword(string name) {
        switch (name) {
            case "bool":
            case "byte":
            case "sbyte":
            case "short":
            case "ushort":
            case "int":
            case "uint":
            case "long":
            case "ulong":
            case "double":
            case "float":
            case "decimal":
            case "string":
            case "char":
            case "object":
            case "typeof":
            case "sizeof":
            case "null":
            case "true":
            case "false":
            case "if":
            case "else":
            case "while":
            case "for":
            case "foreach":
            case "do":
            case "switch":
            case "case":
            case "default":
            case "lock":
            case "try":
            case "throw":
            case "catch":
            case "finally":
            case "goto":
            case "break":
            case "continue":
            case "return":
            case "public":
            case "private":
            case "internal":
            case "protected":
            case "static":
            case "readonly":
            case "sealed":
            case "const":
            case "new":
            case "override":
            case "abstract":
            case "virtual":
            case "partial":
            case "ref":
            case "out":
            case "in":
            case "where":
            case "params":
            case "this":
            case "base":
            case "namespace":
            case "using":
            case "class":
            case "struct":
            case "interface":
            case "delegate":
            case "checked":
            case "get":
            case "set":
            case "add":
            case "remove":
            case "operator":
            case "implicit":
            case "explicit":
            case "fixed":
            case "extern":
            case "event":
            case "enum":
            case "unsafe":
                return true;
            default:
                return false;
        }
    }

    protected static string StripPost(string name, string post) =>
        name.EndsWith(post, StringComparison.Ordinal)
            ? name.Substring(0, name.Length - post.Length)
            : name;


    // TODO: check this as we are not supporting red/green trees??
    protected static string GetFieldType(Field field, bool green) {
        // Fields in red trees are lazily initialized, with null as the uninitialized value
        return GetNullableAwareType(field.Type, field.IsOptional || !green, green);

        static string GetNullableAwareType(string fieldType, bool optionalOrLazy, bool green) {
            if (IsAnyList(fieldType)) {
                if (optionalOrLazy) {
                    return green ? "GreenNode?" : "SyntaxNode?";
                }

                return green ? "GreenNode?" : "SyntaxNode";
            }

            switch (fieldType) {
                case var _ when !optionalOrLazy:
                    return fieldType;

                case "bool":
                case "SyntaxToken" when !green:
                    return fieldType;

                default:
                    return fieldType + "?";
            }
        }
    }

    protected static bool IsSeparatedNodeList(string typeName) =>
        typeName.StartsWith("SeparatedSyntaxList<", StringComparison.Ordinal);

    protected static bool IsNodeList(string typeName) => typeName.StartsWith("SyntaxList<", StringComparison.Ordinal);

    protected static bool IsAnyList(string typeName) =>
        IsNodeList(typeName) || IsSeparatedNodeList(typeName) || typeName == "SyntaxNodeOrTokenList";

    protected static string FixKeyword(string name) => IsKeyword(name) ? "@" + name : name;

    protected static string CamelCase(string name) {
        if (char.IsUpper(name[0])) {
            name = char.ToLowerInvariant(name[0]) + name[1..];
        }

        return FixKeyword(name);
    }

    protected static string GetElementType(string typeName) {
        if (!typeName.Contains("<")) {
            return string.Empty;
        }

        var iStart = typeName.IndexOf('<');
        var iEnd = typeName.IndexOf('>', iStart + 1);
        if (iEnd < iStart) {
            return string.Empty;
        }

        var sub = typeName.Substring(iStart + 1, iEnd - iStart - 1);
        return sub;
    }

    protected static string OverrideOrNewModifier(Field field) =>
        field.IsOverride ? "override " : field.IsNew ? "new " : "";


    protected bool IsDerivedType(string? typeName, string? derivedTypeName) {
        if (typeName == derivedTypeName) {
            return true;
        }

        if (derivedTypeName != null && parentMap.TryGetValue(derivedTypeName, out var baseType)) {
            return IsDerivedType(typeName, baseType);
        }

        return false;
    }

    protected bool IsDerivedOrListOfDerived(string baseType, string derivedType) =>
        IsDerivedType(baseType, derivedType)
        || ((IsNodeList(derivedType) || IsSeparatedNodeList(derivedType))
            && IsDerivedType(baseType, GetElementType(derivedType)));

    protected TreeType? GetTreeType(string typeName) => typeMap.TryGetValue(typeName, out var node) ? node : null;

    protected Node? GetNode(string typeName) => nodeMap.TryGetValue(typeName, out var node) ? node : null;

    protected void Indent() => indentLevel++;

    protected void Unindent() {
        if (indentLevel <= 0) {
            throw new InvalidOperationException("Cannot unindent from base level");
        }

        indentLevel--;
    }

    protected void Write(string msg) {
        WriteIndentIfNeeded();
        writer.Write(msg);
    }

    protected void WriteLine(string msg = "") {
        CancellationToken.ThrowIfCancellationRequested();

        if (msg != "") {
            WriteIndentIfNeeded();
        }

        writer.WriteLine(msg);
        needIndent = true; // need an indent after each line break
    }

    protected void WriteLineWithoutIndent(string msg) {
        writer.WriteLine(msg);
        needIndent = true; // need an indent after each line break
    }

    /// <summary>
    ///     Joins all the values together in <paramref name="values" /> into one string with each
    ///     value separated by a comma.  Values can be either <see cref="string" />s or
    ///     <see
    ///         cref="IEnumerable{T}" />
    ///     s of <see cref="string" />.  All of these are flattened into a
    ///     single sequence that is joined. Empty strings are ignored.
    /// </summary>
    protected string CommaJoin(params object[] values) => Join(", ", values);

    protected string Join(string separator, params object[] values) =>
        string.Join(
            separator,
            values.SelectMany(
                v => (v switch {
                    string s => [s],
                    IEnumerable<string> ss => ss,
                    _ => throw new InvalidOperationException("Join must be passed strings or collections of strings")
                }).Where(s => s != "")
            )
        );

    protected void OpenBlock() {
        WriteLine(" {");
        Indent();
    }

    protected void CloseBlock(string extra = "") {
        Unindent();
        WriteLine("}" + extra);
    }

    protected bool IsNodeOrNodeList(string typeName) =>
        IsNode(typeName)
        || IsNodeList(typeName)
        || IsSeparatedNodeList(typeName)
        || typeName == "SyntaxNodeOrTokenList";

    protected bool IsNode(string typeName) => parentMap.ContainsKey(typeName);
    protected bool IsValueField(Field field) => !IsNodeOrNodeList(field.Type);
}
