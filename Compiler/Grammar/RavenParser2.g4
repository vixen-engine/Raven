parser grammar RavenParser2;

options { tokenVocab=RavenLexer2; superClass=RavenParserBase2; }


compilation_unit
    : package_declaration import_directive* member_declaration* EOF
    ;
    
package_declaration
    : PACKAGE name NL+
    ;
    
import_directive
    : GLOBAL? IMPORT STATIC? type NL+
    ;





// [property: FooBar(foo: "bar"), ...]
attribute_list
    : '[' attribute_target_specifier? attribute (',' attribute)* ']'
    ;

attribute_target_specifier
    : syntax_token ':'
    ;
  
attribute
    : name attribute_argument_list?
    ;
    
attribute_argument_list
    : '(' (attribute_argument (',' attribute_argument)*)? ')'
    ;
    
attribute_argument
    : (name_equals? | name_colon?) expression
    ;

parameter_list
    : '(' (parameter (',' parameter)*)? ')'
    ;

parameter
//    : attribute_list* modifier* type? (identifier_token | '__arglist') equals_value_clause?
//    : attribute_list* modifier* type? identifier_token equals_value_clause?
    // TODO: verify if type can be optional or not
    : attribute_list* modifier* identifier_token (':' type) equals_value_clause?
    ;




// Names
name
    : identifier_name '::' simple_name  #AliasQualifiedName
    | name '.' simple_name              #QualifiedName
    | simple_name                       #SimpleName
    ;

simple_name
    : generic_name
    | identifier_name
    ;

generic_name
    : identifier_token type_argument_list
    ;

type_argument_list
    : '<' (type (',' type)*)? '>'
    ;

name_equals
    : identifier_name '='
    ;

name_colon
    : identifier_name ':'
    ;

identifier_name
    : GLOBAL
    | identifier_token;




// =====================================================================================================================
// ================================================= Declarations ======================================================
// =====================================================================================================================
member_declaration
    : field_declaration NL*
    | base_method_declaration NL*
    | base_property_declaration NL*
    | base_type_declaration NL*
    | delegate_declaration NL*
//    | enum_member_declaration // TODO: is this valid?
//    | global_statement // TODO: is this valid??
    ;
    
base_property_declaration
    : indexer_declaration
    | property_declaration
    ;
    
field_declaration
    : attribute_list* modifier* variable_declaration NL+
    ;
    
base_method_declaration
    : constructor_declaration
    | conversion_operator_declaration
    | destructor_declaration
    | method_declaration
    | operator_declaration
    ;

constructor_declaration
    : attribute_list* modifier* INIT parameter_list constructor_initializer? (block | (arrow_expression_clause NL))
    ;
  
constructor_initializer
  : ':' (BASE | SELF) argument_list
  ;
  
destructor_declaration
    : attribute_list* modifier* '~' INIT parameter_list (block | (arrow_expression_clause NL))
    ;
    
method_declaration
// TODO format
//  : attribute_list* modifier* type explicit_interface_specifier? identifier_token type_parameter_list? parameter_list type_parameter_constraint_clause* (block | (arrow_expression_clause NL))
  : attribute_list* modifier* FUNC explicit_interface_specifier? identifier_token type_parameter_list? parameter_list type_parameter_constraint_clause* (':' type)? (block | (arrow_expression_clause NL))
  ;
  
explicit_interface_specifier
    : name '.'
    ;
  
property_declaration
    : attribute_list* modifier* type explicit_interface_specifier? identifier_token (accessor_list | ((arrow_expression_clause | equals_value_clause) NL))
    ;
    
accessor_list
    : '{' accessor_declaration* '}'
    ;

accessor_declaration
// TODO
//    : attribute_list* modifier* ('get' | 'set' | 'init' | 'add' | 'remove' | identifier_token) (block | (arrow_expression_clause ';'))
    : attribute_list* modifier* identifier_token (block | (arrow_expression_clause NL))
    ;

indexer_declaration
    : attribute_list* modifier* type explicit_interface_specifier? SELF bracketed_parameter_list (accessor_list | (arrow_expression_clause NL))
    ;

bracketed_parameter_list
    : '[' parameter (',' parameter)* ']'
    ;
    
delegate_declaration
    : attribute_list* modifier* DELEGATE type identifier_token type_parameter_list? parameter_list type_parameter_constraint_clause* NL
    ;

global_statement
  : attribute_list* modifier* statement
  ;
  
conversion_operator_declaration
    : attribute_list* modifier* (IMPLICIT | EXPLICIT) explicit_interface_specifier? OPERATOR type parameter_list (block | (arrow_expression_clause NL))
    ; 
    
operator_declaration
//    : attribute_list* modifier* type explicit_interface_specifier? OPERATOR ('+' | '-' | '!' | '~' | '++' | '--' | '*' | '/' | '%' | '<<' | '>>' | '>>>' | '|' | '&' | '^' | '==' | '!=' | '<' | '<=' | '>' | '>=' | 'false' | 'true' | 'is') parameter_list (block | (arrow_expression_clause NL))
    : attribute_list* modifier* type explicit_interface_specifier? OPERATOR ('+' | '-' | '!' | '~' | '++' | '--' | '*' | '/' | '%' | '<<' | '>>' | '|' | '&' | '^' | '==' | '!=' | '<' | '<=' | '>' | '>=' | 'false' | 'true' | 'is') parameter_list (block | (arrow_expression_clause NL))
    ;
  
  
  
  
base_type_declaration
    : enum_declaration
    | type_declaration
    ;

type_declaration
    : class_declaration
    | shader_declaration
    | protocol_declaration
    | record_declaration
    | struct_declaration
    ;
    
// TODO: check the colon at the end; how to handle that with the new line stuff
class_declaration
     : attribute_list* modifier* CLASS identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* '{'? member_declaration* '}'? ';'?
     ; 
     
shader_declaration
//     : attribute_list* modifier* SHADER identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* '{'? member_declaration* '}'? ';'?
     : attribute_list* modifier* SHADER identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* (('{' NL+ member_declaration* NL+ '}') | NL)
     ; 

protocol_declaration
    : attribute_list* modifier* PROTOCOL identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* '{'? member_declaration* '}'? ';'?
    ;

record_declaration
//  : attribute_list* modifier* syntax_token (CLASS | STRUCT)? identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* '{'? member_declaration* '}'? ';'?
    : attribute_list* modifier* RECORD (CLASS | STRUCT)? identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* '{'? member_declaration* '}'? ';'?
    ;

struct_declaration
    : attribute_list* modifier* STRUCT identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* '{'? member_declaration* '}'? ';'?
    ;

enum_declaration
    : attribute_list* modifier* ENUM identifier_token base_list? '{'? (enum_member_declaration (',' enum_member_declaration)* ','?)? '}'? ';'?
    ;
    
enum_member_declaration
    : attribute_list* modifier* identifier_token equals_value_clause?
    ;



type_parameter_list
    : '<' type_parameter (',' type_parameter)* '>'
    ;

type_parameter
    : attribute_list* (IN | OUT)? identifier_token
    ;
    
    
// Constrants
type_parameter_constraint_clause
    : WHERE identifier_name ':' type_parameter_constraint (',' type_parameter_constraint)*
    ;
    
// TODO: check the allows
type_parameter_constraint
//    : allows_constraint_clause
    : class_or_struct_constraint    #ClassOrStructConstraint
    | NEW '(' ')'                   #ConstructorConstraint
    | DEFAULT                       #DefaultConstraint
    | type                          #TypeContraint
    ;
    
//allows_constraint_clause
//    : 'allows' allows_constraint (',' allows_constraint)*
//    ;
    
//allows_constraint
//    : ref_struct_constraint
//    ;
    
//ref_struct_constraint
//    : 'ref' 'struct'
//    ;
    
class_or_struct_constraint
    : CLASS '?'?
    | STRUCT '?'?
    ;
    
    
    
base_list
    : ':' base_type (',' base_type)*
    ;

base_type
    : primary_constructor_base_type
    | simple_base_type
    ;

primary_constructor_base_type
    : type argument_list
    ;

simple_base_type
    : type
    ;










// TODO: verify if the bracketed_argument_list is needed
variable_declaration
//    : type variable_declarator (',' variable_declarator)*
//    : identifier_token bracketed_argument_list? equals_value_clause?
    : (VAR | VAL) identifier_token (':' type)? equals_value_clause?
    ;
    
argument_list
    : '(' (argument (',' argument)*)? ')'
    ;

argument
    : name_colon? (REF | OUT | IN)? expression
    ;

bracketed_argument_list
    : '[' argument (',' argument)* ']'
    ;











// =====================================================================================================================
// ================================================= Statements ========================================================
// =====================================================================================================================
block
    // TODO: verify if attribute_list is correct here
    : attribute_list* '{' NL* statement* NL* '}'
    ;

statement
    : block
    | break_statement
//  | checked_statement
//  | common_for_each_statement
    | continue_statement
    | repeat_statement
    | empty_statement
    | expression_statement
//  | fixed_statement
    | for_statement
//  | goto_statement
    | if_statement
//  | labeled_statement // TODO: this is goto shit
    | local_declaration_statement
    | local_function_statement
//  | lock_statement
    | return_statement
//  | switch_statement
//  | throw_statement
//  | try_statement
//  | unsafe_statement
//  | using_statement
//  | while_statement
//  | yield_statement
  ;


break_statement
    : attribute_list* BREAK NL+
    ;

continue_statement
    : attribute_list* CONTINUE NL+
    ;

repeat_statement
    : attribute_list* REPEAT statement WHILE '(' expression ')' NL+
    ;
    
empty_statement
    : attribute_list* NL+
    ;
    
expression_statement
    : attribute_list* expression NL+
    ;

for_statement
    : attribute_list* FOR '(' identifier_token IN expression ')' block
    ;

if_statement
    : attribute_list* IF '(' expression ')' block else_clause?
    ;

else_clause
    : ELSE block
    ;

return_statement
    : attribute_list* 'return' expression? NL
    ;

local_function_statement
    : attribute_list* modifier* FUNC identifier_token type_parameter_list? parameter_list type_parameter_constraint_clause* (':' type)? (block | (arrow_expression_clause NL))
    ;
    
local_declaration_statement
//    : attribute_list* 'await'? 'using'? modifier* variable_declaration ';'
    : attribute_list* modifier* variable_declaration NL
    ;










syntax_token
    : character_literal_token
    | identifier_token
    | keyword
    | numeric_literal_token
    | operator_token
    | punctuation_token
    | string_literal_token
    ;


// =====================================================================================================================
// ================================================= Expressions =======================================================
// =====================================================================================================================
expression
    : anonymous_function_expression                                             #AnonymousFunctionExpression
    | '{' NL* (anonymous_object_member_declarator (',' NL* anonymous_object_member_declarator)*)? NL* '}' #AnonymousObjectCreationExpression
//  | array_creation_expression
//    | expression ('=' | '+=' | '-=' | '*=' | '/=' | '%=' | '&=' | '^=' | '|=' | '<<=' | '>>=' | '>>>=' | '??=') expression #AssignmentExpression
    | expression ('=' | '+=' | '-=' | '*=' | '/=' | '%=' | '&=' | '^=' | '|=' | '<<=' | '>>=' | '??=') expression #AssignmentExpression
//  | await_expression
//  | base_object_creation_expression
//    | expression ('+' | '-' | '*' | '/' | '%' | '<<' | '>>' | '>>>' | '||' | '&&' | '|' | '&' | '^' | '==' | '!=' | '<' | '<=' | '>' | '>=' | 'is' | 'as' | '??') expression #BinaryExpression
    | expression ('+' | '-' | '*' | '/' | '%' | '<<' | '>>' | '||' | '&&' | '|' | '&' | '^' | '==' | '!=' | '<' | '<=' | '>' | '>=' | 'is' | 'as' | '??') expression #BinaryExpression
    | '(' type ')' expression               #CastExpression
//  | checked_expression
    | '[' NL* (collection_element (',' NL* collection_element)*)? NL* ']'       #CollectionExpression
    | expression '?' expression             #ConditionalAccessExpression
    | expression '?' expression ':' expression #ConditionalExpression
    | type variable_designation             #DeclarationExpression
    | DEFAULT '(' type ')'                  #DefaultExpression
    | expression bracketed_argument_list    #ElementAccessExpression
//  | element_binding_expression
//  | implicit_array_creation_expression
    | bracketed_argument_list               #ImplicitElementAccess
//  | implicit_stack_alloc_array_creation_expression
//  | initializer_expression TODO: this is probrary not valid in our grammar as we don't use 'new' keyword for anonymous object creation
    | instance_expression                   #InstanceExpression
//  | interpolated_string_expression
    | expression argument_list              #InvocationExpression
    | expression IS pattern                 #IsPatternExpression
    | literal_expression                    #LiteralExpression
    | expression ('.' | '->') simple_name   #MemberAccessExpression
    | '.' simple_name                       #MemberBindingExpression
    | '(' expression ')'                    #ParenthesizedExpression
    | expression ('++' | '--' | '!')        #PostfixUnaryExpression
    | prefix_unary_expression               #PrefixUnaryExpression
    | expression DOUBLE_DOT expression      #RangeExpression
    | REF expression                        #RefExpression
    | SIZEOF '(' type ')'                   #SizeofExpression
//  | stack_alloc_array_creation_expression
    | expression 'switch' '{' NL+ (switch_expression_arm (',' switch_expression_arm)*)? NL+ '}' #SwitchExpression
//  | throw_expression
    | '(' argument (',' argument)+ ')'?     #TupleExpression
    | type                                  #TypeExpression
    | TYPEOF '(' type ')'                   #TypeofExpression
//  | with_expression
  ;
    
instance_expression
    : base_expression
    | this_expression
    ;

base_expression
    : BASE
    ;

this_expression
    : SELF
    ;
  
literal_expression
    : DEFAULT
    | FALSE
    | TRUE
    | NULL_
    | character_literal_token
    | numeric_literal_token
    | string_literal_token
    ;

anonymous_function_expression
    : anonymous_method_expression
    | lambda_expression
    ;

anonymous_method_expression
    : modifier* DELEGATE parameter_list? block expression?
    ;

lambda_expression
    : parenthesized_lambda_expression
    | simple_lambda_expression
    ;

parenthesized_lambda_expression
    : attribute_list* modifier* type? parameter_list '=>' (block | expression)
    ;

simple_lambda_expression
    : attribute_list* modifier* parameter '=>' (block | expression)
    ;
  
equals_value_clause
    : '=' expression
    ; 

arrow_expression_clause
    : '=>' expression
    ;

collection_element
    : expression        #ExpressionElement
    | '..' expression   #SpreadElement
    ;
    
anonymous_object_member_declarator
    : name_equals? expression
    ;
    
prefix_unary_expression
    : '!' expression
    | '&' expression
    | '*' expression
    | '+' expression
    | '++' expression
    | '-' expression
    | '--' expression
    | '^' expression
    | '~' expression
    ;
    
switch_expression
    : expression 'switch' '{' NL+ (switch_expression_arm (',' switch_expression_arm)*)? NL+ '}'
    ;

switch_expression_arm
    : pattern when_clause? '=>' expression
    ;


// TODO: patterns
//is_pattern_expression
//    : expression 'is' pattern
//    ;






pattern
    : pattern (OR | AND) pattern    #BinaryPattern
    | expression                    #ConstantPattern
    | type variable_designation     #DeclarationPattern
    | DISCARD                       #DiscardPattern
    | '[' (pattern (',' pattern)*)? ']' variable_designation? #ListPattern
    | '(' pattern ')'               #ParenthesizedPattern
//  | recursive_pattern
    | relational_pattern            #RelationalPattern
    | '..' pattern?                 #SlicePattern
    | type                          #TypePattern
    | NOT pattern                   #UnaryPattern
    | VAR variable_designation      #VarPattern
  ;
  
relational_pattern
    : '!=' expression
    | '<' expression
    | '<=' expression
    | '==' expression
    | '>' expression
    | '>=' expression
    ;

variable_designation
    : DISCARD                                                       #DiscardDesignation
    | '(' (variable_designation (',' variable_designation)*)? ')'   #ParenthesizedVariableDesignation
    | identifier_token                                              #SimpleVariableDesignation
    ;

when_clause
    : WHEN expression
    ;














// =====================================================================================================================
// ================================================= TYPES =============================================================
// =====================================================================================================================
type
    : type array_rank_specifier+ #ArrayType
//    | function_pointer_type // TODO: not sure if this is possible to implement
    | name #NameType
    | type '?' #NullableType
//    | omitted_type_argument
    | type '*' #PointerType
    | predefined_type #PredefinedType
//    | 'ref' 'readonly'? type #RefType // TODO: not sure if this is possible to implement
//    | scoped_type // TODO: same
    | '(' tuple_element (',' tuple_element)+ ')' #TupleType
    ;

tuple_element
  : type identifier_token?
  ;

array_type
  : type array_rank_specifier+
  ;

array_rank_specifier
  : '[' (expression (',' expression)*)? ']'
  ;

predefined_type
    : BOOL
    | BYTE
    | CHAR
    | DOUBLE
    | FLOAT
    | INT
    | LONG
    | OBJECT
    | SBYTE
    | SHORT
    | STRING
    | USHORT
    | UINT
    | ULONG
    ;

string_literal_token
    : regular_string_literal_token
    // TODO: verbatim
    ;

regular_string_literal_token
    : REGULAR_STRING
    ;

operator_token
    : PLUS
    | MINUS
    | STAR
    | DIV
    | PERCENT
    | AMP
    | BITWISE_OR
    | CARET
    | BANG
    | TILDE
    | ASSIGNMENT
    | LT
    | GT
    | OP_COALESCING
    | OP_INC
    | OP_DEC
    | OP_AND
    | OP_OR
    | OP_EQ
    | OP_NE
    | OP_LE
    | OP_GE
    | OP_ADD_ASSIGNMENT
    | OP_SUB_ASSIGNMENT
    | OP_MULT_ASSIGNMENT
    | OP_DIV_ASSIGNMENT
    | OP_MOD_ASSIGNMENT
    | OP_AND_ASSIGNMENT
    | OP_OR_ASSIGNMENT
    | OP_XOR_ASSIGNMENT
    | OP_LEFT_SHIFT
    | OP_LEFT_SHIFT_ASSIGNMENT
    | OP_COALESCING_ASSIGNMENT
    | OP_RIGHT_SHIFT
    | OP_RIGHT_SHIFT_ASSIGNMENT
    ;

punctuation_token
    : OPEN_BRACE
    | CLOSE_BRACE
    | OPEN_BRACKET
    | CLOSE_BRACKET
    | OPEN_PARENS
    | CLOSE_PARENS
    | DOT
    | DOUBLE_DOT
    | COMMA
    | COLON
    | DOUBLE_COLON
    | SEMICOLON
    | INTERR
    | ARROW
    | LAMBDA
    ;

identifier_token
    : AT? IDENTIFIER
    ;

character_literal_token
    : CHARACTER_LITERAL
    ;
    
numeric_literal_token
    : integer_literal_token
    | real_literal_token
    ;

real_literal_token
    : REAL_LITERAL
    ;

integer_literal_token
    : INTEGER_LITERAL
    | HEX_INTEGER_LITERAL
    | BIN_INTEGER_LITERAL
    ;

// TODO: check our keywords if the need to be here
keyword
    : AS
    | BASE
    | BOOL
    | BREAK
    | BYTE
    | CASE
    | CHAR
    | CLASS
    | CONTINUE
    | DEFAULT
    | DELEGATE
    | DOUBLE
    | ELSE
    | ENUM
    | FALSE
    | FLOAT
    | FOR
    | IF
    | INT
    | IS
    | LONG
    | NEW
    | NULL_
    | OBJECT
    | SBYTE
    | SHORT
    | SIZEOF
    | STRING
    | STRUCT
    | SWITCH
    | TRUE
    | UINT
    | ULONG
    | USHORT
    | WHILE
    | modifier
    ;

modifier
    : ABSTRACT
    | CONST
    | OVERRIDE
    | PARTIAL
    | PRIVATE
    | PROTECTED
    | PUBLIC
    | READONLY
    | STATIC
    | VIRTUAL
    ;
    