namespace Vixen.Raven.Syntax;

public abstract class SyntaxNode {
    readonly List<SyntaxNodeOrToken> children = [];
    // public SyntaxTree SyntaxTree { get; }
    // public SyntaxNode? Parent { get; }
    public SyntaxKind Kind { get; }
    public virtual int SlotCount { get; internal set; }
    public abstract SyntaxNode? GetSlot(int index);

    public IEnumerable<SyntaxNodeOrToken> ChildNodesOrTokens => children;
    public IEnumerable<SyntaxNode> ChildNodes => children.Where(x => x.Node != null).Select(x => x.Node!);
    // public IEnumerable<SyntaxToken> ChildTokens => children.Where(x => x.Token != null).Select(x => x.Token!.Value);
    

    internal SyntaxNode(SyntaxKind kind) {
        Kind = kind;
    }

    public abstract TResult? Accept<TResult>(SyntaxVisitor<TResult> visitor);
    public abstract void Accept(SyntaxVisitor visitor);
}
