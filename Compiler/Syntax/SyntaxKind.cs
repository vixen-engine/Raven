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
    
    BreakStatement,
    ContinueStatement,
    RepeatStatement,
    EmptyStatement,
    ExpressionStatement,
    ForStatement,
    IfStatement,
    ElseClause,
    ReturnStatement,
    LocalFunctionStatement,
    UsingKeyword,
    LocalDeclarationStatement,
    WhileStatement,
    UsingStatement,
    SwitchStatement,
    SwitchSection,
    CaseKeyword,
    CasePatternSwitchLabel,
    CaseSwitchLabel,
    DefaultSwitchLabel,
    VariableDeclaration,
    ValKeyword,
    VarKeyword,
    ArrowExpressionClause
}


static class SyntaxKindExtensions {
    internal static SyntaxToken AsToken(this SyntaxKind kind) => new(kind);
}


// TODO
public partial class TypeParameterListSyntax : SyntaxToken {
    internal TypeParameterListSyntax(SyntaxKind kind) : base(kind) { }
}

public partial class TypeParameterConstraintClauseSyntax : SyntaxToken {
    internal TypeParameterConstraintClauseSyntax(SyntaxKind kind) : base(kind) { }
}