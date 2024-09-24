namespace Vixen.Raven.Syntax;

public class SyntaxIdentifierToken : SyntaxToken {
    string text;
    
    public override string Text => text;
    public override object Value => text;
    
    internal SyntaxIdentifierToken(string text) : base(SyntaxKind.IdentifierToken) {
        this.text = text;
    }
}
