namespace Vixen.Raven.Syntax;

public sealed class CompilationUnitSyntax : SyntaxNode {
    public CompilationUnitSyntax(SyntaxTree tree, SyntaxNode? parent) : base(tree, parent) { }

    public override TResult? Accept<TResult>(SyntaxVisitor<TResult> visitor) where TResult : default =>
        visitor.VisitCompilationUnit(this);

    public override void Accept(SyntaxVisitor visitor) => visitor.VisitCompilationUnit(this);
}
