namespace Vixen.Raven.Syntax;

public readonly record struct SyntaxToken {
    public SyntaxNode Parent { get; }
    public object Value { get; }

    internal SyntaxToken(SyntaxNode parent, object value) {
        Parent = parent;
        Value = value;
    }

    public override string ToString() => Value.ToString();
}