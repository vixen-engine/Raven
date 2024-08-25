namespace Vixen.Raven.Syntax;

public sealed class PackageDeclarationSyntax : MemberDeclarationSyntax {
    public PackageDeclarationSyntax(SyntaxTree tree, SyntaxNode? parent) : base(tree, parent) { }

    public override TResult? Accept<TResult>(SyntaxVisitor<TResult> visitor) where TResult : default =>
        visitor.VisitPackageDeclaration(this);

    public override void Accept(SyntaxVisitor visitor) => visitor.VisitPackageDeclaration(this);
}
