using Vixen.Raven.Ast;

namespace Vixen.Raven;

public class VisitorBase {
    protected virtual Node Visit(Node node) => node;

    protected virtual Node VisitDynamic(Node node) => Visit((dynamic)node);
}
