namespace Vixen.Raven.Syntax;

public abstract class SyntaxNode {
    public SyntaxTree SyntaxTree { get; }
    public SyntaxNode? Parent { get; }
    
    // Spans and other shit
    
    internal SyntaxNode(SyntaxTree tree, SyntaxNode? parent) {
        Parent = parent;
        SyntaxTree = tree;
    }
    
    public abstract TResult? Accept<TResult>(SyntaxVisitor<TResult> visitor);
    public abstract void Accept(SyntaxVisitor visitor);
    
}

public abstract class StatementSyntax : SyntaxNode {
    protected StatementSyntax(SyntaxTree tree, SyntaxNode? parent) : base(tree, parent) { }
}
