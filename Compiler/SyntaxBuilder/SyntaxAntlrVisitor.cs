using Vixen.Raven.Grammar;
using Vixen.Raven.Syntax;

namespace Vixen.Raven.SyntaxBuilder;

public class SyntaxAntlrVisitor : RavenParser2BaseVisitor<SyntaxNode> {
    public override SyntaxNode VisitAliasQualifiedName(RavenParser2.AliasQualifiedNameContext context) {
        var identifier = Visit(context.identifier_name()) as IdentifierNameSyntax;
        var name = Visit(context.simple_name()) as SimpleNameSyntax;

        return SyntaxFactory.AliasQualifiedName(identifier!, name!);
    }

    public override SyntaxNode VisitQualifiedName(RavenParser2.QualifiedNameContext context) {
        var left = Visit(context.name()) as NameSyntax;
        var right = Visit(context.simple_name()) as SimpleNameSyntax;

        return SyntaxFactory.QualifiedName(left!, right!);
    }

    public override SyntaxNode VisitClassOrStructConstraint(RavenParser2.ClassOrStructConstraintContext context) =>
        base.VisitClassOrStructConstraint(context);

    public override SyntaxNode VisitConstructorConstraint(RavenParser2.ConstructorConstraintContext context) =>
        base.VisitConstructorConstraint(context);

    public override SyntaxNode VisitDefaultConstraint(RavenParser2.DefaultConstraintContext context) =>
        base.VisitDefaultConstraint(context);

    public override SyntaxNode VisitTypeContraint(RavenParser2.TypeContraintContext context) =>
        base.VisitTypeContraint(context);

    public override SyntaxNode
        VisitAnonymousFunctionExpression(RavenParser2.AnonymousFunctionExpressionContext context) =>
        base.VisitAnonymousFunctionExpression(context);

    public override SyntaxNode VisitAnonymousObjectCreationExpression(
        RavenParser2.AnonymousObjectCreationExpressionContext context
    ) =>
        base.VisitAnonymousObjectCreationExpression(context);

    public override SyntaxNode VisitAssignmentExpression(RavenParser2.AssignmentExpressionContext context) =>
        base.VisitAssignmentExpression(context);

    public override SyntaxNode VisitBinaryExpression(RavenParser2.BinaryExpressionContext context) =>
        base.VisitBinaryExpression(context);

    public override SyntaxNode VisitCastExpression(RavenParser2.CastExpressionContext context) =>
        base.VisitCastExpression(context);

    public override SyntaxNode VisitCollectionExpression(RavenParser2.CollectionExpressionContext context) =>
        base.VisitCollectionExpression(context);

    public override SyntaxNode
        VisitConditionalAccessExpression(RavenParser2.ConditionalAccessExpressionContext context) =>
        base.VisitConditionalAccessExpression(context);

    public override SyntaxNode VisitConditionalExpression(RavenParser2.ConditionalExpressionContext context) =>
        base.VisitConditionalExpression(context);

    public override SyntaxNode VisitDeclarationExpression(RavenParser2.DeclarationExpressionContext context) =>
        base.VisitDeclarationExpression(context);

    public override SyntaxNode VisitDefaultExpression(RavenParser2.DefaultExpressionContext context) =>
        base.VisitDefaultExpression(context);

    public override SyntaxNode VisitElementAccessExpression(RavenParser2.ElementAccessExpressionContext context) =>
        base.VisitElementAccessExpression(context);

    public override SyntaxNode VisitImplicitElementAccess(RavenParser2.ImplicitElementAccessContext context) =>
        base.VisitImplicitElementAccess(context);

    public override SyntaxNode VisitInstanceExpression(RavenParser2.InstanceExpressionContext context) =>
        base.VisitInstanceExpression(context);

    public override SyntaxNode VisitInvocationExpression(RavenParser2.InvocationExpressionContext context) =>
        base.VisitInvocationExpression(context);

    public override SyntaxNode VisitIsPatternExpression(RavenParser2.IsPatternExpressionContext context) =>
        base.VisitIsPatternExpression(context);

    public override SyntaxNode VisitLiteralExpression(RavenParser2.LiteralExpressionContext context) =>
        base.VisitLiteralExpression(context);

    public override SyntaxNode VisitMemberAccessExpression(RavenParser2.MemberAccessExpressionContext context) =>
        base.VisitMemberAccessExpression(context);

    public override SyntaxNode VisitMemberBindingExpression(RavenParser2.MemberBindingExpressionContext context) =>
        base.VisitMemberBindingExpression(context);

    public override SyntaxNode VisitParenthesizedExpression(RavenParser2.ParenthesizedExpressionContext context) =>
        base.VisitParenthesizedExpression(context);

    public override SyntaxNode VisitPostfixUnaryExpression(RavenParser2.PostfixUnaryExpressionContext context) =>
        base.VisitPostfixUnaryExpression(context);

    public override SyntaxNode VisitPrefixUnaryExpression(RavenParser2.PrefixUnaryExpressionContext context) =>
        base.VisitPrefixUnaryExpression(context);

    public override SyntaxNode VisitRangeExpression(RavenParser2.RangeExpressionContext context) =>
        base.VisitRangeExpression(context);

    public override SyntaxNode VisitRefExpression(RavenParser2.RefExpressionContext context) =>
        base.VisitRefExpression(context);

    public override SyntaxNode VisitSizeofExpression(RavenParser2.SizeofExpressionContext context) =>
        base.VisitSizeofExpression(context);

    public override SyntaxNode VisitSwitchExpression(RavenParser2.SwitchExpressionContext context) =>
        base.VisitSwitchExpression(context);

    public override SyntaxNode VisitTupleExpression(RavenParser2.TupleExpressionContext context) =>
        base.VisitTupleExpression(context);

    public override SyntaxNode VisitTypeExpression(RavenParser2.TypeExpressionContext context) =>
        base.VisitTypeExpression(context);

    public override SyntaxNode VisitTypeofExpression(RavenParser2.TypeofExpressionContext context) =>
        base.VisitTypeofExpression(context);

    public override SyntaxNode VisitExpressionElement(RavenParser2.ExpressionElementContext context) =>
        base.VisitExpressionElement(context);

    public override SyntaxNode VisitSpreadElement(RavenParser2.SpreadElementContext context) =>
        base.VisitSpreadElement(context);

    public override SyntaxNode VisitBinaryPattern(RavenParser2.BinaryPatternContext context) =>
        base.VisitBinaryPattern(context);

    public override SyntaxNode VisitConstantPattern(RavenParser2.ConstantPatternContext context) =>
        base.VisitConstantPattern(context);

    public override SyntaxNode VisitDeclarationPattern(RavenParser2.DeclarationPatternContext context) =>
        base.VisitDeclarationPattern(context);

    public override SyntaxNode VisitDiscardPattern(RavenParser2.DiscardPatternContext context) =>
        SyntaxFactory.DiscardPattern();

    public override SyntaxNode VisitListPattern(RavenParser2.ListPatternContext context) =>
        base.VisitListPattern(context);

    public override SyntaxNode VisitParenthesizedPattern(RavenParser2.ParenthesizedPatternContext context) =>
        base.VisitParenthesizedPattern(context);

    public override SyntaxNode VisitRelationalPattern(RavenParser2.RelationalPatternContext context) =>
        base.VisitRelationalPattern(context);

    public override SyntaxNode VisitSlicePattern(RavenParser2.SlicePatternContext context) =>
        base.VisitSlicePattern(context);

    public override SyntaxNode VisitTypePattern(RavenParser2.TypePatternContext context) =>
        base.VisitTypePattern(context);

    public override SyntaxNode VisitUnaryPattern(RavenParser2.UnaryPatternContext context) =>
        base.VisitUnaryPattern(context);

    public override SyntaxNode VisitVarPattern(RavenParser2.VarPatternContext context) => base.VisitVarPattern(context);

    public override SyntaxNode VisitDiscardDesignation(RavenParser2.DiscardDesignationContext context) =>
        base.VisitDiscardDesignation(context);

    public override SyntaxNode VisitParenthesizedVariableDesignation(
        RavenParser2.ParenthesizedVariableDesignationContext context
    ) =>
        base.VisitParenthesizedVariableDesignation(context);

    public override SyntaxNode VisitSimpleVariableDesignation(RavenParser2.SimpleVariableDesignationContext context) =>
        base.VisitSimpleVariableDesignation(context);

    public override SyntaxNode VisitArrayType(RavenParser2.ArrayTypeContext context) => base.VisitArrayType(context);

    public override SyntaxNode VisitNameType(RavenParser2.NameTypeContext context) => base.VisitNameType(context);

    public override SyntaxNode VisitNullableType(RavenParser2.NullableTypeContext context) =>
        base.VisitNullableType(context);

    public override SyntaxNode VisitPointerType(RavenParser2.PointerTypeContext context) =>
        base.VisitPointerType(context);

    public override SyntaxNode VisitPredefinedType(RavenParser2.PredefinedTypeContext context) =>
        base.VisitPredefinedType(context);

    public override SyntaxNode VisitTupleType(RavenParser2.TupleTypeContext context) => base.VisitTupleType(context);

    public override SyntaxNode VisitCompilation_unit(RavenParser2.Compilation_unitContext context) {
        var package = Visit(context.package_declaration()) as PackageDirectiveSyntax;
        var imports = context.import_directive().Select(Visit).ToArray();
        var members = context.member_declaration().Select(Visit).ToArray();

        return SyntaxFactory.CompilationUnit(
            package!,
            new(SyntaxList.List(imports)),
            new(SyntaxList.List(members))
        );
    }

    public override SyntaxNode VisitPackage_declaration(RavenParser2.Package_declarationContext context) {
        var name = Visit(context.name()) as NameSyntax;
        return SyntaxFactory.PackageDirective(name!);
    }

    public override SyntaxNode VisitImport_directive(RavenParser2.Import_directiveContext context) {
        var global = context.GLOBAL() != null ? SyntaxFactory.Global() : null;
        var @static = context.STATIC() != null ? SyntaxFactory.Static() : null;

        var name = Visit(context.name()) as NameSyntax;
        return SyntaxFactory.ImportDirective(global, @static, name!);
    }

    public override SyntaxNode VisitAttribute_list(RavenParser2.Attribute_listContext context) {
        var target = context.attribute_target_specifier() != null
            ? Visit(context.attribute_target_specifier()) as AttributeTargetSpecifierSyntax
            : null;

        var attributes = context.attribute().Select(Visit).ToArray();
        return SyntaxFactory.AttributeList(target, new(SyntaxList.List(attributes)));
    }

    public override SyntaxNode VisitAttribute_target_specifier(RavenParser2.Attribute_target_specifierContext context) {
        var identifier = Visit(context.syntax_token()) as SyntaxToken;
        return SyntaxFactory.AttributeTargetSpecifier(identifier!);
    }

    public override SyntaxNode VisitAttribute(RavenParser2.AttributeContext context) {
        var name = Visit(context.name()) as NameSyntax;
        var list = context.attribute_argument_list() != null
            ? Visit(context.attribute_argument_list()) as AttributeArgumentListSyntax
            : null;

        return SyntaxFactory.Attribute(name!, list);
    }

    public override SyntaxNode VisitAttribute_argument_list(RavenParser2.Attribute_argument_listContext context) {
        var args = context.attribute_argument().Select(Visit).ToArray();
        var list = SyntaxList.List(args);

        return SyntaxFactory.AttributeArgumentList(new(list));
    }

    public override SyntaxNode VisitAttribute_argument(RavenParser2.Attribute_argumentContext context) {
        NameEqualsSyntax? nameEqualsSyntax = null;
        NameColonSyntax? nameColonSyntax = null;

        if (context.name_equals() != null) {
            nameEqualsSyntax = Visit(context.name_equals()) as NameEqualsSyntax;
        }

        if (context.name_colon() != null) {
            nameColonSyntax = Visit(context.name_colon()) as NameColonSyntax;
        }

        var expression = Visit(context.expression()) as ExpressionSyntax;
        return SyntaxFactory.AttributeArgument(nameEqualsSyntax, nameColonSyntax, expression!);
    }

    public override SyntaxNode VisitParameter_list(RavenParser2.Parameter_listContext context) {
        var parameters = context.parameter().Select(Visit).ToArray();
        return SyntaxFactory.ParameterList(new(SyntaxList.List(parameters)));
    }

    public override SyntaxNode VisitParameter(RavenParser2.ParameterContext context) {
        var attributes = context.attribute_list().Select(Visit).ToArray();
        var modifiers = context.modifier().Select(Visit).ToArray();
        var identifier = Visit(context.identifier_token()) as SyntaxToken;
        var type = context.type() != null ? Visit(context.type()) as TypeSyntax : null;
        var @default = context.equals_value_clause() != null
            ? Visit(context.equals_value_clause()) as EqualsValueClauseSyntax
            : null;

        return SyntaxFactory.Parameter(
            new(SyntaxList.List(attributes)),
            new(SyntaxList.List(modifiers)),
            identifier!,
            type,
            @default
        );
    }

    public override SyntaxNode VisitGeneric_name(RavenParser2.Generic_nameContext context) {
        var identifier = Visit(context.identifier_token()) as SyntaxToken;
        var types = Visit(context.type_argument_list()) as TypeArgumentListSyntax;

        return SyntaxFactory.GenericName(identifier!, types!);
    }

    public override SyntaxNode VisitType_argument_list(RavenParser2.Type_argument_listContext context) {
        var args = context.type().Select(Visit).ToArray();
        var list = SyntaxList.List(args);
        return SyntaxFactory.TypeArgumentList(new(list));
    }

    public override SyntaxNode VisitName_equals(RavenParser2.Name_equalsContext context) {
        var identifier = Visit(context.identifier_name()) as IdentifierNameSyntax;
        return SyntaxFactory.NameEquals(identifier!);
    }

    public override SyntaxNode VisitName_colon(RavenParser2.Name_colonContext context) {
        var identifier = Visit(context.identifier_name()) as IdentifierNameSyntax;
        return SyntaxFactory.NameColon(identifier!);
    }

    public override SyntaxNode VisitIdentifier_name(RavenParser2.Identifier_nameContext context) {
        if (context.GLOBAL() != null) {
            throw new NotImplementedException();
        }

        var token = Visit(context.identifier_token()) as SyntaxIdentifierToken;
        return SyntaxFactory.IdentifierName(token!);
    }

    public override SyntaxNode VisitField_declaration(RavenParser2.Field_declarationContext context) =>
        base.VisitField_declaration(context);

    public override SyntaxNode VisitConstructor_declaration(RavenParser2.Constructor_declarationContext context) =>
        base.VisitConstructor_declaration(context);

    public override SyntaxNode VisitConstructor_initializer(RavenParser2.Constructor_initializerContext context) =>
        base.VisitConstructor_initializer(context);

    public override SyntaxNode VisitDestructor_declaration(RavenParser2.Destructor_declarationContext context) =>
        base.VisitDestructor_declaration(context);

    public override SyntaxNode VisitMethod_declaration(RavenParser2.Method_declarationContext context) =>
        base.VisitMethod_declaration(context);

    public override SyntaxNode VisitExplicit_interface_specifier(
        RavenParser2.Explicit_interface_specifierContext context
    ) =>
        base.VisitExplicit_interface_specifier(context);

    public override SyntaxNode VisitProperty_declaration(RavenParser2.Property_declarationContext context) =>
        base.VisitProperty_declaration(context);

    public override SyntaxNode VisitAccessor_list(RavenParser2.Accessor_listContext context) =>
        base.VisitAccessor_list(context);

    public override SyntaxNode VisitAccessor_declaration(RavenParser2.Accessor_declarationContext context) =>
        base.VisitAccessor_declaration(context);

    public override SyntaxNode VisitIndexer_declaration(RavenParser2.Indexer_declarationContext context) =>
        base.VisitIndexer_declaration(context);

    public override SyntaxNode VisitBracketed_parameter_list(RavenParser2.Bracketed_parameter_listContext context) {
        var parameters = context.parameter().Select(Visit).ToArray();
        return SyntaxFactory.BracketedParameterList(new(SyntaxList.List(parameters)));
    }

    public override SyntaxNode VisitDelegate_declaration(RavenParser2.Delegate_declarationContext context) =>
        base.VisitDelegate_declaration(context);

    public override SyntaxNode VisitConversion_operator_declaration(
        RavenParser2.Conversion_operator_declarationContext context
    ) =>
        base.VisitConversion_operator_declaration(context);

    public override SyntaxNode VisitOperator_declaration(RavenParser2.Operator_declarationContext context) =>
        base.VisitOperator_declaration(context);

    public override SyntaxNode VisitClass_declaration(RavenParser2.Class_declarationContext context) =>
        base.VisitClass_declaration(context);

    public override SyntaxNode VisitShader_declaration(RavenParser2.Shader_declarationContext context) =>
        base.VisitShader_declaration(context);

    public override SyntaxNode VisitProtocol_declaration(RavenParser2.Protocol_declarationContext context) =>
        base.VisitProtocol_declaration(context);

    public override SyntaxNode VisitRecord_declaration(RavenParser2.Record_declarationContext context) =>
        base.VisitRecord_declaration(context);

    public override SyntaxNode VisitStruct_declaration(RavenParser2.Struct_declarationContext context) =>
        base.VisitStruct_declaration(context);

    public override SyntaxNode VisitEnum_declaration(RavenParser2.Enum_declarationContext context) =>
        base.VisitEnum_declaration(context);

    public override SyntaxNode VisitEnum_member_declaration(RavenParser2.Enum_member_declarationContext context) =>
        base.VisitEnum_member_declaration(context);

    public override SyntaxNode VisitType_parameter_list(RavenParser2.Type_parameter_listContext context) =>
        base.VisitType_parameter_list(context);

    public override SyntaxNode VisitType_parameter(RavenParser2.Type_parameterContext context) =>
        base.VisitType_parameter(context);

    public override SyntaxNode VisitType_parameter_constraint_clause(
        RavenParser2.Type_parameter_constraint_clauseContext context
    ) =>
        base.VisitType_parameter_constraint_clause(context);

    public override SyntaxNode VisitType_parameter_constraint(RavenParser2.Type_parameter_constraintContext context) =>
        base.VisitType_parameter_constraint(context);

    public override SyntaxNode
        VisitClass_or_struct_constraint(RavenParser2.Class_or_struct_constraintContext context) =>
        base.VisitClass_or_struct_constraint(context);

    public override SyntaxNode VisitBase_list(RavenParser2.Base_listContext context) => base.VisitBase_list(context);

    public override SyntaxNode VisitPrimary_constructor_base_type(
        RavenParser2.Primary_constructor_base_typeContext context
    ) {
        var type = Visit(context.type()) as TypeSyntax;
        var args = Visit(context.argument_list()) as ArgumentListSyntax;
        return SyntaxFactory.PrimaryConstructorBaseType(type!, args!);
    }

    public override SyntaxNode VisitSimple_base_type(RavenParser2.Simple_base_typeContext context) {
        var type = Visit(context.type()) as TypeSyntax;
        return SyntaxFactory.SimpleBaseType(type!);
    }

    public override SyntaxNode VisitVariable_declaration(RavenParser2.Variable_declarationContext context) =>
        base.VisitVariable_declaration(context);

    public override SyntaxNode VisitArgument_list(RavenParser2.Argument_listContext context) {
        var args = context.argument().Select(Visit).ToArray();
        return SyntaxFactory.ArgumentList(new(SyntaxList.List(args)));
    }

    public override SyntaxNode VisitArgument(RavenParser2.ArgumentContext context) {
        var nameColon = context.name_colon() != null
            ? Visit(context.name_colon()) as NameColonSyntax
            : null;

        SyntaxToken? refKind = null;
        if (context.REF() != null) {
            refKind = new(SyntaxKind.RefKeyword);
        }

        if (context.OUT() != null) {
            refKind = new(SyntaxKind.OutKeyword);
        }

        if (context.IN() != null) {
            refKind = new(SyntaxKind.InKeyword);
        }

        var expression = Visit(context.expression()) as ExpressionSyntax;
        return SyntaxFactory.Argument(nameColon, refKind, expression!);
    }

    public override SyntaxNode VisitBracketed_argument_list(RavenParser2.Bracketed_argument_listContext context) {
        var args = context.argument().Select(Visit).ToArray();

        return SyntaxFactory.BracketedArgumentList(new(SyntaxList.List(args)));
    }

    public override SyntaxNode VisitBlock(RavenParser2.BlockContext context) {
        var statements = context.statement().Select(Visit).ToArray();
        return SyntaxFactory.Block(new(), new(SyntaxList.List(statements)));
    }

    public override SyntaxNode VisitBreak_statement(RavenParser2.Break_statementContext context) =>
        base.VisitBreak_statement(context);

    public override SyntaxNode VisitContinue_statement(RavenParser2.Continue_statementContext context) =>
        base.VisitContinue_statement(context);

    public override SyntaxNode VisitRepeat_statement(RavenParser2.Repeat_statementContext context) =>
        base.VisitRepeat_statement(context);

    public override SyntaxNode VisitEmpty_statement(RavenParser2.Empty_statementContext context) =>
        base.VisitEmpty_statement(context);

    public override SyntaxNode VisitExpression_statement(RavenParser2.Expression_statementContext context) =>
        base.VisitExpression_statement(context);

    public override SyntaxNode VisitFor_statement(RavenParser2.For_statementContext context) =>
        base.VisitFor_statement(context);

    public override SyntaxNode VisitIf_statement(RavenParser2.If_statementContext context) =>
        base.VisitIf_statement(context);

    public override SyntaxNode VisitElse_clause(RavenParser2.Else_clauseContext context) =>
        base.VisitElse_clause(context);

    public override SyntaxNode VisitReturn_statement(RavenParser2.Return_statementContext context) =>
        base.VisitReturn_statement(context);

    public override SyntaxNode VisitLocal_function_statement(RavenParser2.Local_function_statementContext context) =>
        base.VisitLocal_function_statement(context);

    public override SyntaxNode
        VisitLocal_declaration_statement(RavenParser2.Local_declaration_statementContext context) =>
        base.VisitLocal_declaration_statement(context);

    public override SyntaxNode VisitWhile_statement(RavenParser2.While_statementContext context) =>
        base.VisitWhile_statement(context);

    public override SyntaxNode VisitUsing_statement(RavenParser2.Using_statementContext context) =>
        base.VisitUsing_statement(context);

    public override SyntaxNode VisitSwitch_statement(RavenParser2.Switch_statementContext context) =>
        base.VisitSwitch_statement(context);

    public override SyntaxNode VisitSwitch_section(RavenParser2.Switch_sectionContext context) =>
        base.VisitSwitch_section(context);

    public override SyntaxNode VisitCase_pattern_switch_label(RavenParser2.Case_pattern_switch_labelContext context) =>
        base.VisitCase_pattern_switch_label(context);

    public override SyntaxNode VisitCase_switch_label(RavenParser2.Case_switch_labelContext context) =>
        base.VisitCase_switch_label(context);

    public override SyntaxNode VisitDefault_switch_label(RavenParser2.Default_switch_labelContext context) =>
        base.VisitDefault_switch_label(context);

    public override SyntaxNode VisitExpression(RavenParser2.ExpressionContext context) => base.VisitExpression(context);

    public override SyntaxNode VisitBase_expression(RavenParser2.Base_expressionContext context) =>
        SyntaxFactory.BaseExpression();

    public override SyntaxNode VisitSelf_expression(RavenParser2.Self_expressionContext context) =>
        SyntaxFactory.SelfExpression();

    public override SyntaxNode VisitLiteral_expression(RavenParser2.Literal_expressionContext context) =>
        base.VisitLiteral_expression(context);


    public override SyntaxNode
        VisitAnonymous_method_expression(RavenParser2.Anonymous_method_expressionContext context) =>
        base.VisitAnonymous_method_expression(context);

    public override SyntaxNode VisitParenthesized_lambda_expression(
        RavenParser2.Parenthesized_lambda_expressionContext context
    ) =>
        base.VisitParenthesized_lambda_expression(context);

    public override SyntaxNode VisitSimple_lambda_expression(RavenParser2.Simple_lambda_expressionContext context) =>
        base.VisitSimple_lambda_expression(context);

    public override SyntaxNode VisitEquals_value_clause(RavenParser2.Equals_value_clauseContext context) {
        var expression = Visit(context.expression()) as ExpressionSyntax;
        return SyntaxFactory.EqualsValueClause(expression!);
    }

    public override SyntaxNode VisitArrow_expression_clause(RavenParser2.Arrow_expression_clauseContext context) =>
        base.VisitArrow_expression_clause(context);

    public override SyntaxNode VisitCollection_element(RavenParser2.Collection_elementContext context) =>
        base.VisitCollection_element(context);

    public override SyntaxNode VisitAnonymous_object_member_declarator(
        RavenParser2.Anonymous_object_member_declaratorContext context
    ) =>
        base.VisitAnonymous_object_member_declarator(context);

    public override SyntaxNode VisitPrefix_unary_expression(RavenParser2.Prefix_unary_expressionContext context) =>
        base.VisitPrefix_unary_expression(context);

    public override SyntaxNode VisitSwitch_expression_arm(RavenParser2.Switch_expression_armContext context) =>
        base.VisitSwitch_expression_arm(context);

    public override SyntaxNode VisitPattern(RavenParser2.PatternContext context) => base.VisitPattern(context);

    public override SyntaxNode VisitRelational_pattern(RavenParser2.Relational_patternContext context) =>
        base.VisitRelational_pattern(context);

    public override SyntaxNode VisitVariable_designation(RavenParser2.Variable_designationContext context) =>
        base.VisitVariable_designation(context);

    public override SyntaxNode VisitWhen_clause(RavenParser2.When_clauseContext context) {
        var condition = Visit(context.expression()) as ExpressionSyntax;
        return SyntaxFactory.WhenClause(condition!);
    }

    public override SyntaxNode VisitType(RavenParser2.TypeContext context) => base.VisitType(context);

    public override SyntaxNode VisitTuple_element(RavenParser2.Tuple_elementContext context) =>
        base.VisitTuple_element(context);

    public override SyntaxNode VisitArray_type(RavenParser2.Array_typeContext context) => base.VisitArray_type(context);

    public override SyntaxNode VisitArray_rank_specifier(RavenParser2.Array_rank_specifierContext context) =>
        base.VisitArray_rank_specifier(context);

    public override SyntaxNode VisitPredefined_type(RavenParser2.Predefined_typeContext context) {
        if (context.BOOL() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.BoolKeyword));
        }

        if (context.BYTE() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.ByteKeyword));
        }

        if (context.SBYTE() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.SByteKeyword));
        }

        if (context.INT() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.IntKeyword));
        }

        if (context.UINT() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.UIntKeyword));
        }

        if (context.SHORT() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.ShortKeyword));
        }

        if (context.USHORT() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.UShortKeyword));
        }

        if (context.LONG() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.LongKeyword));
        }

        if (context.ULONG() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.ULongKeyword));
        }

        if (context.FLOAT() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.FloatKeyword));
        }

        if (context.DOUBLE() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.DoubleKeyword));
        }

        if (context.STRING() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.StringKeyword));
        }

        if (context.CHAR() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.CharKeyword));
        }

        if (context.OBJECT() != null) {
            return SyntaxFactory.PredefinedType(new(SyntaxKind.ObjectKeyword));
        }

        throw new ArgumentOutOfRangeException();
    }

    public override SyntaxNode VisitRegular_string_literal_token(
        RavenParser2.Regular_string_literal_tokenContext context
    ) =>
        base.VisitRegular_string_literal_token(context);

    public override SyntaxNode VisitOperator_token(RavenParser2.Operator_tokenContext context) =>
        base.VisitOperator_token(context);

    public override SyntaxNode VisitPunctuation_token(RavenParser2.Punctuation_tokenContext context) =>
        base.VisitPunctuation_token(context);

    public override SyntaxNode VisitIdentifier_token(RavenParser2.Identifier_tokenContext context) =>
        SyntaxFactory.Identifier((context.AT() != null ? "@" : string.Empty) + context.IDENTIFIER().GetText());

    public override SyntaxNode VisitCharacter_literal_token(RavenParser2.Character_literal_tokenContext context) =>
        SyntaxFactory.Literal(context.CHARACTER_LITERAL().GetText()[1]);

    public override SyntaxNode VisitReal_literal_token(RavenParser2.Real_literal_tokenContext context) {
        var text = context.REAL_LITERAL().GetText();
        if (text.EndsWith('f')) {
            return SyntaxFactory.Literal(float.Parse(context.REAL_LITERAL().GetText()[..^1]));
        }

        return SyntaxFactory.Literal(double.Parse(context.REAL_LITERAL().GetText()));
    }

    public override SyntaxNode VisitInteger_literal_token(RavenParser2.Integer_literal_tokenContext context) {
        // TODO: how to parse ulong, etc?

        if (context.INTEGER_LITERAL() != null) {
            return SyntaxFactory.Literal(long.Parse(context.INTEGER_LITERAL().GetText()));
        }

        if (context.HEX_INTEGER_LITERAL() != null) {
            return SyntaxFactory.Literal(long.Parse(context.HEX_INTEGER_LITERAL().GetText()));
        }

        if (context.BIN_INTEGER_LITERAL() != null) {
            return SyntaxFactory.Literal(long.Parse(context.BIN_INTEGER_LITERAL().GetText()));
        }

        throw new ArgumentOutOfRangeException();
    }

    public override SyntaxNode VisitKeyword(RavenParser2.KeywordContext context) => base.VisitKeyword(context);

    public override SyntaxNode VisitModifier(RavenParser2.ModifierContext context) {
        if (context.ABSTRACT() != null) {
            return new SyntaxToken(SyntaxKind.AbstractKeyword);
        }
        
        if (context.ABSTRACT() != null) {
            return new SyntaxToken(SyntaxKind.AbstractKeyword);
        }
        
        if (context.CONST() != null) {
            return new SyntaxToken(SyntaxKind.ConstKeyword);
        }
        
        if (context.OVERRIDE() != null) {
            return new SyntaxToken(SyntaxKind.OverrideKeyword);
        }
        
        if (context.PARTIAL() != null) {
            return new SyntaxToken(SyntaxKind.PartialKeyword);
        }
        
        if (context.PRIVATE() != null) {
            return new SyntaxToken(SyntaxKind.PrivateKeyword);
        }
        
        if (context.PROTECTED() != null) {
            return new SyntaxToken(SyntaxKind.ProtectedKeyword);
        }
        
        if (context.PUBLIC() != null) {
            return new SyntaxToken(SyntaxKind.PublicKeyword);
        }
        
        if (context.READONLY() != null) {
            return new SyntaxToken(SyntaxKind.ReadOnlyKeyword);
        }
        
        if (context.STATIC() != null) {
            return new SyntaxToken(SyntaxKind.StaticKeyword);
        }
        
        if (context.VIRTUAL() != null) {
            return new SyntaxToken(SyntaxKind.VirtualKeyword);
        }

        throw new ArgumentOutOfRangeException();
    }
}
