namespace Vixen.Raven.Syntax;

public readonly struct SeparatedSyntaxList<TNode> : IEquatable<SeparatedSyntaxList<TNode>> //, IReadOnlyList<TNode>
    where TNode : SyntaxNode {
    readonly SyntaxList<SyntaxNode> list;

    public int Count => (list.Count + 1) >> 1;
    public int SeparatorCount => list.Count >> 1;
    internal SyntaxNode? Node => list.Node;

    public TNode? this[int index] => (TNode?)list[index << 1];

    internal SeparatedSyntaxList(SyntaxList<SyntaxNode> list) {
        // Validate(list);
        this.list = list;
    }

    // [Conditional("DEBUG")]
    // static void Validate(SyntaxList<SyntaxNode> list) {
    //     for (var i = 0; i < list.Count; i++) {
    //         // TODO
    //         // var item = list.GetRequiredItem(i);
    //         var item = list[i];
    //         if ((i & 1) == 0) {
    //             Debug.Assert(!item.IsToken, "even elements of a separated list must be nodes");
    //         } else {
    //             Debug.Assert(item.IsToken, "odd elements of a separated list must be tokens");
    //         }
    //     }
    // }

    public static bool operator ==(in SeparatedSyntaxList<TNode> left, in SeparatedSyntaxList<TNode> right) =>
        left.Equals(right);

    public static bool operator !=(in SeparatedSyntaxList<TNode> left, in SeparatedSyntaxList<TNode> right) =>
        !left.Equals(right);

    public bool Equals(SeparatedSyntaxList<TNode> other) => list == other.list;
    public override bool Equals(object? obj) => obj is SeparatedSyntaxList<TNode> nodes && Equals(nodes);
    public override int GetHashCode() => list.GetHashCode();


    public SeparatedSyntaxList<TNode> AddRange(IEnumerable<TNode> nodes) => throw new NotImplementedException();
    // return InsertRange(this.Count, nodes);
}
