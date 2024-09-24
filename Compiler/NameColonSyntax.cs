using Vixen.Raven.Syntax;

namespace Vixen.Raven;

public partial class NameColonSyntax {
    public override ExpressionSyntax Expression => Name;
    // internal override BaseExpressionColonSyntax WithExpressionCore(ExpressionSyntax expression) => throw new NotImplementedException();

    internal override BaseExpressionColonSyntax WithExpressionCore(ExpressionSyntax expression) {
        if (expression is IdentifierNameSyntax identifierName) {
            return WithName(identifierName);
        }

        return SyntaxFactory.ExpressionColon(expression, ColonToken);
    }
}

public partial class SyntaxFactory {
    public static NameColonSyntax NameColon(IdentifierNameSyntax name) => NameColon(name, Token(SyntaxKind.ColonToken));

    public static NameColonSyntax NameColon(string name) => NameColon(IdentifierName(name));
}
