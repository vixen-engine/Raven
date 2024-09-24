using Vixen.Raven.Syntax;

namespace Vixen.Raven;

abstract class SyntaxList() : SyntaxNode(SyntaxKind.ListKind) {
    public override TResult? Accept<TResult>(SyntaxVisitor<TResult> visitor) where TResult : default =>
        throw new NotImplementedException();

    public override void Accept(SyntaxVisitor visitor) {
        throw new NotImplementedException();
    }

    internal static SyntaxNode List(SyntaxNode child) => child;
    internal static SyntaxNode List(SyntaxNode child0, SyntaxNode child1) => new WithTwoChildren(child0, child1);
    internal static SyntaxNode List(SyntaxNode?[] children) => new WithManyChildren(children!);


    sealed class WithTwoChildren(SyntaxNode child0, SyntaxNode child1) : SyntaxList {
        public override int SlotCount => 2;

        public override SyntaxNode? GetSlot(int index) {
            return index switch {
                0 => child0,
                1 => child1,
                _ => null
            };
        }
    }

    sealed class WithManyChildren(SyntaxNode[] children) : SyntaxList {
        public override int SlotCount => children.Length;
        public override SyntaxNode? GetSlot(int index) => children[index];
    }
}
