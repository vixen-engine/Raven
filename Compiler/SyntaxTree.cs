using Antlr4.Runtime;
using System.Text;
using Vixen.Raven.Antlr;
using Vixen.Raven.Antlr.SyntaxBuilder;
using Vixen.Raven.Syntax;

namespace Vixen.Raven;

public sealed class SyntaxTree {
    SyntaxNode? root;

    public Encoding? Encoding { get; private init; }
    public string FilePath { get; private init; }
    public int Length { get; private init; }
    public ParseOptions Options { get; private init; }

    // public SyntaxTree() { }

    public SyntaxNode GetRoot() => root!;

    public bool TryGetRoot(out SyntaxNode? root) {
        root = this.root;
        return root != null;
    }

    public static SyntaxTree Create(
        SyntaxNode root,
        ParseOptions? options = default,
        string? path = "",
        Encoding? encoding = default
    ) =>
        new() { Encoding = encoding, FilePath = path ?? string.Empty, Options = options ?? new ParseOptions(), root = root };

    public static SyntaxTree ParseText(
        string text,
        ParseOptions? options = default,
        string? path = "",
        Encoding? encoding = default
    ) {
        var syntaxTree = new SyntaxTree {
            Encoding = encoding,
            FilePath = path ?? string.Empty,
            Options = options ?? new ParseOptions(),
            Length = text.Length
        };
        
        // Antlr
        var stream = new AntlrInputStream(text);
        var lexer = new RavenLexer(stream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new RavenParser(tokenStream);
        
        // Internal visitor to transform ANTLR into Syntax Tree
        var visitor = new SyntaxAntlrVisitor();
        var tree = parser.compilation_unit();

        syntaxTree.root = tree.Accept(visitor);
        return syntaxTree;
    }
}
