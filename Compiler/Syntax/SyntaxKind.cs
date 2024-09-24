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
    QualifiedName,
    DiscardPattern,
    CompilationUnit,
    PackageDirective,
    ImportDirective,
    NameColon,
    ExpressionColon,
    NameEquals,
    AttributeArgument,
    Attribute,
    AttributeArgumentList,
    AttributeTargetSpecifier,
    AttributeList,
    GenericName,
    TypeArgumentList,
    AliasQualifiedName,
    PredefinedType,
    BoolKeyword,
    ByteKeyword,
    SByteKeyword,
    IntKeyword,
    UIntKeyword,
    ShortKeyword,
    UShortKeyword,
    LongKeyword,
    ULongKeyword,
    FloatKeyword,
    DoubleKeyword,
    StringKeyword,
    CharKeyword,
    ObjectKeyword,
    SimpleBaseType,
    PrimaryConstructorBaseType,
    ArgumentList,
    BracketedArgumentList,
    Argument,
    RefKeyword,
    OutKeyword,
    InKeyword,
    EqualsValueClause,
    // ArgListKeyword
    Parameter,
    ParameterList,
    BracketedParameterList,
    
    AbstractKeyword,
    ConstKeyword,
    OverrideKeyword,
    PartialKeyword,
    PrivateKeyword,
    ProtectedKeyword,
    PublicKeyword,
    ReadOnlyKeyword,
    StaticKeyword,
    VirtualKeyword,
    
    SelfExpression,
    BaseExpression,
    WhenClause,
    Block,
}


static class SyntaxKindExtensions {
    internal static SyntaxToken AsToken(this SyntaxKind kind) => new(kind);
}