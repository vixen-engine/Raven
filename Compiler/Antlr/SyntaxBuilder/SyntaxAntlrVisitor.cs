using Vixen.Raven.Syntax;

namespace Vixen.Raven.Antlr.SyntaxBuilder;

public partial class SyntaxAntlrVisitor : RavenParserBaseVisitor<SyntaxNode> {
    public override SyntaxNode VisitCompilation_unit(RavenParser.Compilation_unitContext context) {
        var package = Visit(context.package_declaration()) as PackageDeclarationSyntax;

        // TODO
        return new CompilationUnitSyntax(null!, null);
    }
    
    public override SyntaxNode VisitPackage_declaration(RavenParser.Package_declarationContext context) {
        var identifier = Visit(context.qualified_identifier());//  as Identifier;

        // TODO
        return new PackageDeclarationSyntax(null!, null);
    }

    public override SyntaxNode VisitLiteralExpression(RavenParser.LiteralExpressionContext context) =>
        base.VisitLiteralExpression(context);

    public override SyntaxNode VisitSimpleNameExpression(RavenParser.SimpleNameExpressionContext context) =>
        base.VisitSimpleNameExpression(context);

    public override SyntaxNode VisitParenthesisExpressions(RavenParser.ParenthesisExpressionsContext context) =>
        base.VisitParenthesisExpressions(context);

    public override SyntaxNode VisitMemberAccessExpression(RavenParser.MemberAccessExpressionContext context) =>
        base.VisitMemberAccessExpression(context);

    public override SyntaxNode VisitLiteralAccessExpression(RavenParser.LiteralAccessExpressionContext context) =>
        base.VisitLiteralAccessExpression(context);

    public override SyntaxNode VisitSelfReferenceExpression(RavenParser.SelfReferenceExpressionContext context) =>
        base.VisitSelfReferenceExpression(context);

    public override SyntaxNode VisitBaseAccessExpression(RavenParser.BaseAccessExpressionContext context) =>
        base.VisitBaseAccessExpression(context);

    public override SyntaxNode VisitTupleExpression(RavenParser.TupleExpressionContext context) =>
        base.VisitTupleExpression(context);

    public override SyntaxNode VisitDefaultValueExpression(RavenParser.DefaultValueExpressionContext context) =>
        base.VisitDefaultValueExpression(context);

    public override SyntaxNode VisitSizeofExpression(RavenParser.SizeofExpressionContext context) =>
        base.VisitSizeofExpression(context);

    public override SyntaxNode VisitNameofExpression(RavenParser.NameofExpressionContext context) =>
        base.VisitNameofExpression(context);

    public override SyntaxNode VisitExpressionStatement(RavenParser.ExpressionStatementContext context) =>
        base.VisitExpressionStatement(context);

    public override SyntaxNode VisitIfStatement(RavenParser.IfStatementContext context) =>
        base.VisitIfStatement(context);

    public override SyntaxNode VisitWhileStatement(RavenParser.WhileStatementContext context) =>
        base.VisitWhileStatement(context);

    public override SyntaxNode VisitRepeatStatement(RavenParser.RepeatStatementContext context) =>
        base.VisitRepeatStatement(context);

    public override SyntaxNode VisitForStatement(RavenParser.ForStatementContext context) =>
        base.VisitForStatement(context);

    public override SyntaxNode VisitBreakStatement(RavenParser.BreakStatementContext context) =>
        base.VisitBreakStatement(context);

    public override SyntaxNode VisitContinueStatement(RavenParser.ContinueStatementContext context) =>
        base.VisitContinueStatement(context);

    public override SyntaxNode VisitReturnStatement(RavenParser.ReturnStatementContext context) =>
        base.VisitReturnStatement(context);

    public override SyntaxNode VisitUsingStatement(RavenParser.UsingStatementContext context) =>
        base.VisitUsingStatement(context);

    public override SyntaxNode VisitImportPackageDirective(RavenParser.ImportPackageDirectiveContext context) =>
        base.VisitImportPackageDirective(context);


    public override SyntaxNode VisitNamespace_or_type_name(RavenParser.Namespace_or_type_nameContext context) =>
        base.VisitNamespace_or_type_name(context);

    public override SyntaxNode VisitType_(RavenParser.Type_Context context) => base.VisitType_(context);

    public override SyntaxNode VisitBase_type(RavenParser.Base_typeContext context) => base.VisitBase_type(context);

    public override SyntaxNode VisitTuple_type(RavenParser.Tuple_typeContext context) => base.VisitTuple_type(context);

    public override SyntaxNode VisitTuple_element(RavenParser.Tuple_elementContext context) =>
        base.VisitTuple_element(context);

    public override SyntaxNode VisitSimple_type(RavenParser.Simple_typeContext context) =>
        base.VisitSimple_type(context);

    public override SyntaxNode VisitNumeric_type(RavenParser.Numeric_typeContext context) =>
        base.VisitNumeric_type(context);

    public override SyntaxNode VisitIntegral_type(RavenParser.Integral_typeContext context) =>
        base.VisitIntegral_type(context);

    public override SyntaxNode VisitFloating_point_type(RavenParser.Floating_point_typeContext context) =>
        base.VisitFloating_point_type(context);

    public override SyntaxNode VisitClass_type(RavenParser.Class_typeContext context) => base.VisitClass_type(context);

    public override SyntaxNode VisitType_argument_list(RavenParser.Type_argument_listContext context) =>
        base.VisitType_argument_list(context);

    public override SyntaxNode VisitArgument_list(RavenParser.Argument_listContext context) =>
        base.VisitArgument_list(context);

    public override SyntaxNode VisitArgument(RavenParser.ArgumentContext context) => base.VisitArgument(context);

    public override SyntaxNode VisitExpression(RavenParser.ExpressionContext context) => base.VisitExpression(context);

    public override SyntaxNode VisitNon_assignment_expression(RavenParser.Non_assignment_expressionContext context) =>
        base.VisitNon_assignment_expression(context);

    public override SyntaxNode VisitAssignment(RavenParser.AssignmentContext context) => base.VisitAssignment(context);

    public override SyntaxNode VisitAssignment_operator(RavenParser.Assignment_operatorContext context) =>
        base.VisitAssignment_operator(context);

    public override SyntaxNode VisitConditional_expression(RavenParser.Conditional_expressionContext context) =>
        base.VisitConditional_expression(context);

    public override SyntaxNode VisitNull_coalescing_expression(RavenParser.Null_coalescing_expressionContext context) =>
        base.VisitNull_coalescing_expression(context);

    public override SyntaxNode VisitConditional_or_expression(RavenParser.Conditional_or_expressionContext context) =>
        base.VisitConditional_or_expression(context);

    public override SyntaxNode VisitConditional_and_expression(RavenParser.Conditional_and_expressionContext context) =>
        base.VisitConditional_and_expression(context);

    public override SyntaxNode VisitInclusive_or_expression(RavenParser.Inclusive_or_expressionContext context) =>
        base.VisitInclusive_or_expression(context);

    public override SyntaxNode VisitExclusive_or_expression(RavenParser.Exclusive_or_expressionContext context) =>
        base.VisitExclusive_or_expression(context);

    public override SyntaxNode VisitAnd_expression(RavenParser.And_expressionContext context) =>
        base.VisitAnd_expression(context);

    public override SyntaxNode VisitEquality_expression(RavenParser.Equality_expressionContext context) =>
        base.VisitEquality_expression(context);

    public override SyntaxNode VisitRelational_expression(RavenParser.Relational_expressionContext context) =>
        base.VisitRelational_expression(context);

    public override SyntaxNode VisitShift_expression(RavenParser.Shift_expressionContext context) =>
        base.VisitShift_expression(context);

    public override SyntaxNode VisitAdditive_expression(RavenParser.Additive_expressionContext context) =>
        base.VisitAdditive_expression(context);

    public override SyntaxNode VisitMultiplicative_expression(RavenParser.Multiplicative_expressionContext context) =>
        base.VisitMultiplicative_expression(context);

    public override SyntaxNode VisitSwitch_expression(RavenParser.Switch_expressionContext context) =>
        base.VisitSwitch_expression(context);

    public override SyntaxNode VisitSwitch_expression_arms(RavenParser.Switch_expression_armsContext context) =>
        base.VisitSwitch_expression_arms(context);

    public override SyntaxNode VisitSwitch_expression_arm(RavenParser.Switch_expression_armContext context) =>
        base.VisitSwitch_expression_arm(context);

    public override SyntaxNode VisitRange_expression(RavenParser.Range_expressionContext context) =>
        base.VisitRange_expression(context);

    public override SyntaxNode VisitUnary_expression(RavenParser.Unary_expressionContext context) =>
        base.VisitUnary_expression(context);

    public override SyntaxNode VisitCast_expression(RavenParser.Cast_expressionContext context) =>
        base.VisitCast_expression(context);

    public override SyntaxNode VisitPrimary_expression(RavenParser.Primary_expressionContext context) =>
        base.VisitPrimary_expression(context);

    public override SyntaxNode VisitPrimary_expression_start(RavenParser.Primary_expression_startContext context) =>
        base.VisitPrimary_expression_start(context);

    public override SyntaxNode VisitMember_access(RavenParser.Member_accessContext context) =>
        base.VisitMember_access(context);

    public override SyntaxNode VisitBracket_expression(RavenParser.Bracket_expressionContext context) =>
        base.VisitBracket_expression(context);

    public override SyntaxNode VisitIndexer_argument(RavenParser.Indexer_argumentContext context) =>
        base.VisitIndexer_argument(context);

    public override SyntaxNode VisitPredefined_type(RavenParser.Predefined_typeContext context) =>
        base.VisitPredefined_type(context);

    public override SyntaxNode VisitStatement(RavenParser.StatementContext context) => base.VisitStatement(context);

    public override SyntaxNode VisitDeclaration_statement(RavenParser.Declaration_statementContext context) =>
        base.VisitDeclaration_statement(context);

    public override SyntaxNode VisitLocal_function_declaration(RavenParser.Local_function_declarationContext context) =>
        base.VisitLocal_function_declaration(context);

    public override SyntaxNode VisitLocal_function_header(RavenParser.Local_function_headerContext context) =>
        base.VisitLocal_function_header(context);

    public override SyntaxNode VisitLocal_function_body(RavenParser.Local_function_bodyContext context) =>
        base.VisitLocal_function_body(context);

    public override SyntaxNode VisitEmbedded_statement(RavenParser.Embedded_statementContext context) =>
        base.VisitEmbedded_statement(context);

    public override SyntaxNode VisitSimple_embedded_statement(RavenParser.Simple_embedded_statementContext context) =>
        base.VisitSimple_embedded_statement(context);

    public override SyntaxNode VisitBlock(RavenParser.BlockContext context) => base.VisitBlock(context);

    public override SyntaxNode VisitLocal_variable_declaration(RavenParser.Local_variable_declarationContext context) =>
        base.VisitLocal_variable_declaration(context);

    public override SyntaxNode VisitLocal_variable_initializer(RavenParser.Local_variable_initializerContext context) =>
        base.VisitLocal_variable_initializer(context);

    public override SyntaxNode VisitLocal_constant_declaration(RavenParser.Local_constant_declarationContext context) =>
        base.VisitLocal_constant_declaration(context);

    public override SyntaxNode VisitStatement_list(RavenParser.Statement_listContext context) =>
        base.VisitStatement_list(context);

    public override SyntaxNode VisitResource_acquisition(RavenParser.Resource_acquisitionContext context) =>
        base.VisitResource_acquisition(context);


    public override SyntaxNode VisitQualified_identifier(RavenParser.Qualified_identifierContext context) =>
        base.VisitQualified_identifier(context);

    public override SyntaxNode VisitImport_directives(RavenParser.Import_directivesContext context) =>
        base.VisitImport_directives(context);

    public override SyntaxNode VisitImport_directive(RavenParser.Import_directiveContext context) =>
        base.VisitImport_directive(context);

    public override SyntaxNode VisitType_declaration(RavenParser.Type_declarationContext context) =>
        base.VisitType_declaration(context);

    public override SyntaxNode VisitQualified_alias_member(RavenParser.Qualified_alias_memberContext context) =>
        base.VisitQualified_alias_member(context);

    public override SyntaxNode VisitType_parameter_list(RavenParser.Type_parameter_listContext context) =>
        base.VisitType_parameter_list(context);

    public override SyntaxNode VisitType_parameter(RavenParser.Type_parameterContext context) =>
        base.VisitType_parameter(context);

    public override SyntaxNode VisitClass_base(RavenParser.Class_baseContext context) => base.VisitClass_base(context);

    public override SyntaxNode VisitType_parameter_constraints_clauses(
        RavenParser.Type_parameter_constraints_clausesContext context
    ) =>
        base.VisitType_parameter_constraints_clauses(context);

    public override SyntaxNode VisitType_parameter_constraints_clause(
        RavenParser.Type_parameter_constraints_clauseContext context
    ) =>
        base.VisitType_parameter_constraints_clause(context);

    public override SyntaxNode VisitType_parameter_constraints(RavenParser.Type_parameter_constraintsContext context) =>
        base.VisitType_parameter_constraints(context);

    public override SyntaxNode VisitPrimary_constraint(RavenParser.Primary_constraintContext context) =>
        base.VisitPrimary_constraint(context);

    public override SyntaxNode VisitSecondary_constraints(RavenParser.Secondary_constraintsContext context) =>
        base.VisitSecondary_constraints(context);

    public override SyntaxNode VisitClass_body(RavenParser.Class_bodyContext context) => base.VisitClass_body(context);

    public override SyntaxNode VisitClass_member_declarations(RavenParser.Class_member_declarationsContext context) =>
        base.VisitClass_member_declarations(context);

    public override SyntaxNode VisitClass_member_declaration(RavenParser.Class_member_declarationContext context) =>
        base.VisitClass_member_declaration(context);

    public override SyntaxNode VisitAll_member_modifiers(RavenParser.All_member_modifiersContext context) =>
        base.VisitAll_member_modifiers(context);

    public override SyntaxNode VisitAll_member_modifier(RavenParser.All_member_modifierContext context) =>
        base.VisitAll_member_modifier(context);

    public override SyntaxNode VisitCommon_member_declaration(RavenParser.Common_member_declarationContext context) =>
        base.VisitCommon_member_declaration(context);

    public override SyntaxNode VisitConstant_declarator(RavenParser.Constant_declaratorContext context) =>
        base.VisitConstant_declarator(context);

    public override SyntaxNode VisitVariable_declarator(RavenParser.Variable_declaratorContext context) =>
        base.VisitVariable_declarator(context);

    public override SyntaxNode VisitVariable_initializer(RavenParser.Variable_initializerContext context) =>
        base.VisitVariable_initializer(context);

    public override SyntaxNode VisitFormal_parameter_list(RavenParser.Formal_parameter_listContext context) =>
        base.VisitFormal_parameter_list(context);

    public override SyntaxNode VisitFixed_parameters(RavenParser.Fixed_parametersContext context) =>
        base.VisitFixed_parameters(context);

    public override SyntaxNode VisitFixed_parameter(RavenParser.Fixed_parameterContext context) =>
        base.VisitFixed_parameter(context);

    public override SyntaxNode VisitStruct_interfaces(RavenParser.Struct_interfacesContext context) =>
        base.VisitStruct_interfaces(context);

    public override SyntaxNode VisitStruct_body(RavenParser.Struct_bodyContext context) =>
        base.VisitStruct_body(context);

    public override SyntaxNode VisitStruct_member_declaration(RavenParser.Struct_member_declarationContext context) =>
        base.VisitStruct_member_declaration(context);

    public override SyntaxNode VisitRank_specifier(RavenParser.Rank_specifierContext context) =>
        base.VisitRank_specifier(context);

    public override SyntaxNode VisitArray_initializer(RavenParser.Array_initializerContext context) =>
        base.VisitArray_initializer(context);

    public override SyntaxNode
        VisitVariant_type_parameter_list(RavenParser.Variant_type_parameter_listContext context) =>
        base.VisitVariant_type_parameter_list(context);

    public override SyntaxNode VisitVariant_type_parameter(RavenParser.Variant_type_parameterContext context) =>
        base.VisitVariant_type_parameter(context);

    public override SyntaxNode VisitVariance_annotation(RavenParser.Variance_annotationContext context) =>
        base.VisitVariance_annotation(context);

    public override SyntaxNode VisitProtocol_type_list(RavenParser.Protocol_type_listContext context) =>
        base.VisitProtocol_type_list(context);

    public override SyntaxNode VisitProtocol_base(RavenParser.Protocol_baseContext context) =>
        base.VisitProtocol_base(context);

    public override SyntaxNode VisitAttributes(RavenParser.AttributesContext context) => base.VisitAttributes(context);

    public override SyntaxNode VisitAttribute(RavenParser.AttributeContext context) => base.VisitAttribute(context);

    public override SyntaxNode VisitAttribute_argument(RavenParser.Attribute_argumentContext context) =>
        base.VisitAttribute_argument(context);

    public override SyntaxNode VisitLiteral(RavenParser.LiteralContext context) => base.VisitLiteral(context);

    public override SyntaxNode VisitBoolean_literal(RavenParser.Boolean_literalContext context) =>
        base.VisitBoolean_literal(context);

    public override SyntaxNode VisitString_literal(RavenParser.String_literalContext context) =>
        base.VisitString_literal(context);

    public override SyntaxNode VisitShader_definition(RavenParser.Shader_definitionContext context) =>
        base.VisitShader_definition(context);

    public override SyntaxNode VisitClass_definition(RavenParser.Class_definitionContext context) =>
        base.VisitClass_definition(context);

    public override SyntaxNode VisitStruct_definition(RavenParser.Struct_definitionContext context) =>
        base.VisitStruct_definition(context);

    public override SyntaxNode VisitProtocol_definition(RavenParser.Protocol_definitionContext context) =>
        base.VisitProtocol_definition(context);

    public override SyntaxNode VisitEnum_definition(RavenParser.Enum_definitionContext context) =>
        base.VisitEnum_definition(context);

    public override SyntaxNode VisitField_declaration(RavenParser.Field_declarationContext context) =>
        base.VisitField_declaration(context);

    public override SyntaxNode VisitConstant_declaration(RavenParser.Constant_declarationContext context) =>
        base.VisitConstant_declaration(context);

    public override SyntaxNode VisitConstructor_declaration(RavenParser.Constructor_declarationContext context) =>
        base.VisitConstructor_declaration(context);

    public override SyntaxNode VisitMethod_declaration(RavenParser.Method_declarationContext context) =>
        base.VisitMethod_declaration(context);

    public override SyntaxNode VisitMethod_member_name(RavenParser.Method_member_nameContext context) =>
        base.VisitMethod_member_name(context);

    public override SyntaxNode VisitArg_declaration(RavenParser.Arg_declarationContext context) =>
        base.VisitArg_declaration(context);

    public override SyntaxNode VisitMethod_invocation(RavenParser.Method_invocationContext context) =>
        base.VisitMethod_invocation(context);

    public override SyntaxNode VisitIdentifier(RavenParser.IdentifierContext context) => base.VisitIdentifier(context);
}
