using Antlr4.Runtime.Tree;
using Vixen.Raven.Antlr;
using Vixen.Raven.Ast;

namespace Vixen.Raven;

public partial class BuildAstVisitor {
    public override Node VisitExpression(RavenParser.ExpressionContext context) => Visit(context.GetChild(0));

    public override Node VisitNon_assignment_expression(RavenParser.Non_assignment_expressionContext context) =>
        Visit(context.GetChild(0));

    public override Node VisitAssignment(RavenParser.AssignmentContext context) {
        var left = Visit(context.unary_expression()) as Expression;

        var opToken = context.assignment_operator().GetChild(0);
        if (opToken is not ITerminalNode terminalNode) {
            throw new("fatal");
        }

        var op = terminalNode.Symbol.Type switch {
            RavenParser.OP_COALESCING_ASSIGNMENT => AssignmentOperator.CoalescingAssignment,
            RavenParser.ASSIGNMENT => AssignmentOperator.Default,
            RavenParser.OP_ADD_ASSIGNMENT => AssignmentOperator.Addition,
            RavenParser.OP_SUB_ASSIGNMENT => AssignmentOperator.Subtraction,
            RavenParser.OP_MULT_ASSIGNMENT => AssignmentOperator.Multiplication,
            RavenParser.OP_DIV_ASSIGNMENT => AssignmentOperator.Division,
            RavenParser.OP_MOD_ASSIGNMENT => AssignmentOperator.Modulo,
            RavenParser.OP_AND_ASSIGNMENT => AssignmentOperator.BitwiseAnd,
            RavenParser.OP_OR_ASSIGNMENT => AssignmentOperator.BitwiseOr,
            RavenParser.OP_XOR_ASSIGNMENT => AssignmentOperator.BitwiseXor,
            RavenParser.OP_LEFT_SHIFT_ASSIGNMENT => AssignmentOperator.BitwiseShiftLeft,
            // TODO: right shift assignment

            _ => throw new("fatal")
        };

        var right = Visit(context.expression()) as Expression;
        return new AssignmentExpression(left, right, op);
    }

    public override Node VisitConditional_expression(RavenParser.Conditional_expressionContext context) {
        var left = Visit(context.null_coalescing_expression()) as Expression;

        if (context.INTERR() != null) {
            var then = Visit(context.GetChild(2)) as Expression;
            var @else = Visit(context.GetChild(4)) as Expression;

            return new ConditionalExpression(left, then, @else);
        }

        return left;
    }

    public override Node VisitNull_coalescing_expression(RavenParser.Null_coalescing_expressionContext context) {
        var left = Visit(context.conditional_or_expression()) as Expression;

        if (context.OP_COALESCING() != null) {
            var right = Visit(context.GetChild(2)) as Expression;
            return new NullCoalescingExpression(left, right);
        }

        return left;
    }

    public override Node VisitConditional_or_expression(RavenParser.Conditional_or_expressionContext context) =>
        VisitBinaryExpression(context);

    public override Node VisitConditional_and_expression(RavenParser.Conditional_and_expressionContext context) =>
        VisitBinaryExpression(context);

    public override Node VisitInclusive_or_expression(RavenParser.Inclusive_or_expressionContext context) =>
        VisitBinaryExpression(context);

    public override Node VisitExclusive_or_expression(RavenParser.Exclusive_or_expressionContext context) =>
        VisitBinaryExpression(context);

    public override Node VisitAnd_expression(RavenParser.And_expressionContext context) =>
        VisitBinaryExpression(context);

    public override Node VisitEquality_expression(RavenParser.Equality_expressionContext context) =>
        VisitBinaryExpression(context);

    // TODO: relational expression should be handled differently

    public override Node VisitShift_expression(RavenParser.Shift_expressionContext context) =>
        VisitBinaryExpression(context);

    public override Node VisitAdditive_expression(RavenParser.Additive_expressionContext context) =>
        VisitBinaryExpression(context);

    public override Node VisitMultiplicative_expression(RavenParser.Multiplicative_expressionContext context) =>
        VisitBinaryExpression(context);


    // TODO: switch

    public override Node VisitRange_expression(RavenParser.Range_expressionContext context) {
        if (context.unary_expression().Length == 2) {
            var start = Visit(context.unary_expression(0)) as Expression;
            var end = Visit(context.unary_expression(1)) as Expression;
            return new RangeExpression(start, end);
        }

        return Visit(context.unary_expression(0));
    }

    public override Node VisitUnary_expression(RavenParser.Unary_expressionContext context) {
        if (context.cast_expression() is { } castExpression) {
            return Visit(castExpression);
        }

        if (context.primary_expression() is { } primaryExpression) {
            return Visit(primaryExpression);
        }

        // TODO: post increment expressions needs to be handled elsewhere

        var opTerminalNode = context.GetChild(0) as ITerminalNode;
        var op = opTerminalNode.Symbol.Type switch {
            RavenParser.PLUS => UnaryOperator.Plus,
            RavenParser.MINUS => UnaryOperator.Minus,
            RavenParser.BANG => UnaryOperator.LogicalNot,
            RavenParser.TILDE => UnaryOperator.BitwiseNot,
            RavenParser.OP_INC => UnaryOperator.PreIncrement,
            RavenParser.OP_DEC => UnaryOperator.PreDecrement,
            _ => throw new("fatal")
        };

        var expr = Visit(context.unary_expression()) as Expression;
        return new UnaryExpression(op, expr);
    }

    public override Node VisitCast_expression(RavenParser.Cast_expressionContext context) {
        // TODO: type
        var expression = Visit(context.unary_expression()) as Expression;

        return new CastExpression(expression, null);
    }

    Node VisitBinaryExpression(IParseTree context) {
        if (context.ChildCount == 1) {
            return Visit(context.GetChild(0));
        }

        var left = Visit(context.GetChild(0)) as Expression;
        for (var i = 1; i < context.ChildCount; i++) {
            var opToken = context.GetChild(i++);
            if (opToken is not ITerminalNode terminalNode) {
                throw new("fatal");
            }

            var op = terminalNode.Symbol.Type switch {
                RavenParser.STAR => BinaryOperator.Multiply,
                RavenParser.DIV => BinaryOperator.Divide,
                RavenParser.PERCENT => BinaryOperator.Modulo,
                RavenParser.PLUS => BinaryOperator.Plus,
                RavenParser.MINUS => BinaryOperator.Minus,
                RavenParser.OP_LEFT_SHIFT => BinaryOperator.LeftShift,
                // TODO: right shift
                RavenParser.OP_EQ => BinaryOperator.Equality,
                RavenParser.OP_NE => BinaryOperator.Inequality,
                RavenParser.AMP => BinaryOperator.BitwiseAnd,
                RavenParser.CARET => BinaryOperator.BitwiseXor,
                RavenParser.BITWISE_OR => BinaryOperator.BitwiseOr,
                RavenParser.OP_AND => BinaryOperator.LogicalAnd,
                RavenParser.OP_OR => BinaryOperator.LogicalOr,
                _ => throw new("fatal")
            };

            var right = Visit(context.GetChild(i)) as Expression;
            left = new BinaryExpression(left, right, op);
        }

        return left;
    }
}
