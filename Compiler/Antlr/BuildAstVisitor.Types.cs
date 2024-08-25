using Antlr4.Runtime.Tree;
using Vixen.Raven.Antlr;
using Vixen.Raven.Ast;

namespace Vixen.Raven;

public partial class BuildAstVisitor {
    // TODO: finish these

    public override Node VisitType_(RavenParser.Type_Context context) => base.VisitType_(context);

    public override Node VisitBase_type(RavenParser.Base_typeContext context) => Visit(context.GetChild(0));

    public override Node VisitTuple_type(RavenParser.Tuple_typeContext context) => base.VisitTuple_type(context);

    public override Node VisitTuple_element(RavenParser.Tuple_elementContext context) =>
        base.VisitTuple_element(context);

    public override Node VisitSimple_type(RavenParser.Simple_typeContext context) =>
        context.BOOL() != null ? ScalarType.Bool : Visit(context.numeric_type());

    public override Node VisitNumeric_type(RavenParser.Numeric_typeContext context) => Visit(context.GetChild(0));

    public override Node VisitIntegral_type(RavenParser.Integral_typeContext context) {
        if (context.GetChild(0) is not ITerminalNode terminalNode) {
            throw new("fatal");
        }

        return terminalNode.Symbol.Type switch {
            RavenParser.BYTE => ScalarType.Byte,
            RavenParser.CHAR => ScalarType.Char,
            RavenParser.SHORT => ScalarType.Short,
            RavenParser.USHORT => ScalarType.UShort,
            RavenParser.INT => ScalarType.Int,
            RavenParser.UINT => ScalarType.UInt,
            RavenParser.LONG => ScalarType.Long,
            RavenParser.ULONG => ScalarType.ULong,

            _ => throw new("fatal")
        };
    }

    public override Node VisitFloating_point_type(RavenParser.Floating_point_typeContext context) {
        if (context.FLOAT() != null) {
            return ScalarType.Float;
        }

        if (context.DOUBLE() != null) {
            return ScalarType.Double;
        }

        throw new("fatal");
    }

    public override Node VisitClass_type(RavenParser.Class_typeContext context) => base.VisitClass_type(context);

    public override Node VisitType_argument_list(RavenParser.Type_argument_listContext context) =>
        base.VisitType_argument_list(context);
}
