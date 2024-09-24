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

    public static SyntaxToken Literal(long value) => SyntaxToken.WithValue(value);
    public static SyntaxToken Literal(char value) => SyntaxToken.WithValue(value);
    public static SyntaxToken Literal(double value) => SyntaxToken.WithValue(value);
    
    
    public static SyntaxToken Global() => new(SyntaxKind.GlobalKeyword);
    public static SyntaxToken Static() => new(SyntaxKind.StaticKeyword);
}

// IMethodSymbol
