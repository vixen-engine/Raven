namespace Vixen.Raven.Syntax;

public enum SyntaxKind : ushort {
    None,
    PercentToken,
    OpenBraceToken,
    // TODO
    
    FalseKeyword,
    
    IdentifierName,
    // ETC
    
    
    
    
    // non sorted stuff from the generator
    ListKind,
    GlobalKeyword,
    IdentifierToken,
    DotToken,
    QualifiedName,
    UnderscoreToken,
    DiscardPattern,
    CompilationUnit,
    PackageKeyword,
    PackageDirective,
    EndOfFileToken,
    ImportKeyword,
    StaticKeyword,
    ImportDirective,
    ColonToken,
    NameColon,
    ExpressionColon,
    EqualsToken,
    NameEquals,
    AttributeArgument,
    OpenParenToken,
    Attribute,
    CloseParenToken,
    AttributeArgumentList,
    OpenBracketToken,
    CloseBracketToken,
    AttributeTargetSpecifier,
    AttributeList,
    
}


static class SyntaxKindExtensions {
    internal static SyntaxToken AsToken(this SyntaxKind kind) => new SyntaxToken(kind);
}