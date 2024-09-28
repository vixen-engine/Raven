namespace Vixen.Raven.Syntax;

public partial class SyntaxFactory {
    public static SyntaxToken Token(SyntaxKind kind) => new(kind);

    public static SyntaxToken Identifier(string text) => new SyntaxIdentifierToken(text);
    public static IdentifierNameSyntax IdentifierName(string name) => IdentifierName(Identifier(name));

    public static SyntaxToken Literal(string value) => SyntaxToken.WithValue(value);
    public static SyntaxToken Literal(long value) => SyntaxToken.WithValue(value);
    public static SyntaxToken Literal(char value) => SyntaxToken.WithValue(value);
    public static SyntaxToken Literal(double value) => SyntaxToken.WithValue(value);
    
    
    public static SyntaxToken Global() => new(SyntaxKind.GlobalKeyword);
    public static SyntaxToken Static() => new(SyntaxKind.StaticKeyword);
}

// IMethodSymbol
