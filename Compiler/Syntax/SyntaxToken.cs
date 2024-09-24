namespace Vixen.Raven.Syntax;

public class SyntaxToken : SyntaxNode {
    public override SyntaxNode? GetSlot(int index) => throw new NotImplementedException();

    public override TResult? Accept<TResult>(SyntaxVisitor<TResult> visitor) where TResult : default => throw new NotImplementedException();

    public override void Accept(SyntaxVisitor visitor) {
        throw new NotImplementedException();
    }

    public virtual object Value { get; }
    
    // TODO             get { return SyntaxFacts.GetText(this.Kind); }
    public virtual string Text { get; }

    internal SyntaxToken(SyntaxKind kind) : base(kind) {
    }

    internal static SyntaxToken Create(SyntaxNode parent, SyntaxKind syntaxKind) {
        throw new NotImplementedException();
        // return new(parent, syntaxKind, null!);
    }

    public override string ToString() => Text;

    internal static SyntaxToken WithValue(long value) {
        return new SyntaxToken(SyntaxKind.None); // TODO
    }
    
    internal static SyntaxToken WithValue(char value) {
        return new SyntaxToken(SyntaxKind.None); // TODO
    }
    
    internal static SyntaxToken WithValue(double value) {
        return new SyntaxToken(SyntaxKind.None); // TODO
    }
}