using Vixen.Raven.Syntax;

namespace Vixen.Raven;

public partial class SyntaxFactory {
    // TODO

    // public static SyntaxToken Create(SyntaxNode parent, SyntaxKind syntaxKind) {
    //     return SyntaxToken.Create(parent, syntaxKind);
    // }


    public static SyntaxToken Token(SyntaxKind kind) => new(kind);

    public static SyntaxToken Identifier(string text) => new SyntaxIdentifierToken(text);
    public static IdentifierNameSyntax IdentifierName(string name) => IdentifierName(Identifier(name));
}

// IMethodSymbol
