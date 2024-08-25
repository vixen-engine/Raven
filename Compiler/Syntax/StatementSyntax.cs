namespace Vixen.Raven.Syntax;

public abstract class StatementSyntax : SyntaxNode {
    protected StatementSyntax(SyntaxTree tree, SyntaxNode? parent) : base(tree, parent) { }
}
