namespace Vixen.Raven.Syntax;

public abstract partial class SyntaxVisitor {
    public virtual void Visit(SyntaxNode? node) {
        node?.Accept(this);
    }

    public virtual void DefaultVisit(SyntaxNode node) { }
}

public abstract partial class SyntaxVisitor<TResult> {
    public virtual TResult? Visit(SyntaxNode? node) {
        if (node != null) {
            return node.Accept(this);
        }

        return default;
    }

    public virtual TResult? DefaultVisit(SyntaxNode node) => default;
}
