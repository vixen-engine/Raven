namespace Vixen.Raven.Syntax;

public abstract class MemberDeclarationSyntax : SyntaxNode {
    protected MemberDeclarationSyntax(SyntaxTree tree, SyntaxNode? parent) : base(tree, parent) { }
}
