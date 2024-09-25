parser grammar RavenParser2;

options { tokenVocab=RavenLexer2; superClass=RavenParserBase2; }


compilation_unit
    : package_declaration import_directive* member_declaration* EOF
    ;
    
package_declaration
    : PACKAGE name NL+
    ;
    
import_directive
    : GLOBAL? IMPORT STATIC? name NL+
    ;





attribute_list
    : '[' attribute_target_specifier? attribute (',' attribute)* ']' NL+
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
    : attribute_list* modifier* identifier_token (':' type)? equals_value_clause?
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
//    | global_statement // TODO: is this valid?? Probably for Program.cs usage (whitout class)
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
  : ':' init=(BASE | SELF) argument_list
  ;
  
destructor_declaration
    : attribute_list* modifier* '~' INIT parameter_list (block | (arrow_expression_clause NL))
    ;
    
method_declaration
  : attribute_list* modifier* FUNC explicit_interface_specifier? identifier_token type_parameter_list? parameter_list type_parameter_constraint_clause* (':' type)? (block | (arrow_expression_clause NL))
  ;
  
explicit_interface_specifier
    : name '.'
    ;
  
property_declaration
    : attribute_list* modifier* VAR explicit_interface_specifier? identifier_token (':' type)? (accessor_list | ((arrow_expression_clause | equals_value_clause) NL))
    ;
    
accessor_list
    : '{' NL* accessor_declaration* NL* '}'
    ;

accessor_declaration
    : attribute_list* modifier* op=(GET | SET | WILL_SET | DID_SET) (block | (arrow_expression_clause NL)) NL*
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
  
conversion_operator_declaration
    : attribute_list* modifier* ct=(IMPLICIT | EXPLICIT) explicit_interface_specifier? OPERATOR type parameter_list (block | (arrow_expression_clause NL))
    ; 
    
operator_declaration
    : attribute_list* modifier* type explicit_interface_specifier? OPERATOR op=('+' | '-' | '!' | '~' | '++' | '--' | '*' | '/' | '%' | '<<' | '>>' | '>>>' | '|' | '&' | '^' | '==' | '!=' | '<' | '<=' | '>' | '>=' | 'false' | 'true' | 'is') parameter_list (block | (arrow_expression_clause NL))
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
    
class_declaration
     : attribute_list* modifier* CLASS identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* ('{' NL+ member_declaration* NL+ '}')? NL
     ; 
     
shader_declaration
     : attribute_list* modifier* SHADER identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* ('{' NL+ member_declaration* NL+ '}')? NL
     ; 

protocol_declaration
    : attribute_list* modifier* PROTOCOL identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* ('{' NL+ member_declaration* NL+ '}')? NL
    ;

record_declaration
    : attribute_list* modifier* RECORD (CLASS | STRUCT)? identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* ('{' NL+ member_declaration* NL+ '}')? NL
    ;

struct_declaration
    : attribute_list* modifier* STRUCT identifier_token type_parameter_list? parameter_list? base_list? type_parameter_constraint_clause* ('{' NL+ member_declaration* NL+ '}')? NL
    ;

enum_declaration
    : attribute_list* modifier* ENUM identifier_token base_list? '{' NL+ (enum_member_declaration (',' enum_member_declaration)*)? '}' NL
    ;
    
enum_member_declaration
    : attribute_list* modifier* identifier_token equals_value_clause?
    ;

type_parameter_list
    : '<' type_parameter (',' type_parameter)* '>'
    ;

type_parameter
    : attribute_list* dir=(IN | OUT)? identifier_token
    ;
    
type_parameter_constraint_clause
    : WHERE identifier_name ':' type_parameter_constraint (',' type_parameter_constraint)*
    ;
    
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

variable_declaration
    : (VAR | VAL) identifier_token (':' type)? equals_value_clause?
    ;
    
argument_list
    : '(' (argument (',' argument)*)? ')'
    ;

argument
    : name_colon? kind=(REF | OUT | IN)? expression
    ;

bracketed_argument_list
    : '[' argument (',' argument)* ']'
    ;


// =====================================================================================================================
// ================================================= Statements ========================================================
// =====================================================================================================================
block
    : '{' NL* statement* NL* '}'
    ;

statement
    : block
    | break_statement
    | continue_statement
    | repeat_statement
    | empty_statement
    | expression_statement
    | for_statement
    | if_statement
    | local_declaration_statement
    | local_function_statement
    | return_statement
    | switch_statement
    | using_statement
    | while_statement
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
    : attribute_list* RETURN expression? NL
    ;

local_function_statement
    : attribute_list* modifier* FUNC identifier_token type_parameter_list? parameter_list type_parameter_constraint_clause* (':' type)? (block | (arrow_expression_clause NL))
    ;
    
local_declaration_statement
    : attribute_list* USING? modifier* variable_declaration NL
    ;
    
while_statement
    : attribute_list* WHILE '(' expression ')' statement
    ;

using_statement
    : attribute_list* USING '(' (variable_declaration | expression) ')' statement
    ;

switch_statement
  : attribute_list* SWITCH '('? expression ')'? '{' switch_section* '}'
  ;

switch_section
  : switch_label+ statement+
  ;

switch_label
  : case_pattern_switch_label
  | case_switch_label
  | default_switch_label
  ;

case_pattern_switch_label
  : CASE pattern when_clause? ':'
  ;
  
case_switch_label
    : CASE expression ':'
    ;

default_switch_label
    : DEFAULT ':'
    ;



// =====================================================================================================================
// ================================================= Expressions =======================================================
// =====================================================================================================================
expression
    : anonymous_function_expression         #AnonymousFunctionExpression
    | '{' NL* (anonymous_object_member_declarator (',' NL* anonymous_object_member_declarator)*)? NL* '}' #AnonymousObjectCreationExpression
//  | array_creation_expression
    | expression op=('=' | '+=' | '-=' | '*=' | '/=' | '%=' | '&=' | '^=' | '|=' | '<<=' | '>>=' | '>>>=' | '??=') expression #AssignmentExpression
//  | await_expression
//  | base_object_creation_expression
    | expression op=('+' | '-' | '*' | '/' | '%' | '<<' | '>>' | '>>>' | '||' | '&&' | '|' | '&' | '^' | '==' | '!=' | '<' | '<=' | '>' | '>=' | 'is' | 'as' | '??') expression #BinaryExpression
    | '(' type ')' expression               #CastExpression
    | '[' NL* (collection_element (',' NL* collection_element)*)? NL* ']' #CollectionExpression
    | expression '?' expression             #ConditionalAccessExpression
    | expression '?' expression ':' expression #ConditionalExpression
    | type variable_designation             #DeclarationExpression
    | DEFAULT '(' type ')'                  #DefaultExpression
    | expression bracketed_argument_list    #ElementAccessExpression
//  | implicit_array_creation_expression
    | bracketed_argument_list               #ImplicitElementAccess
//  | implicit_stack_alloc_array_creation_expression
//  | initializer_expression TODO: this is probably not valid in our grammar as we don't use 'new' keyword for anonymous object creation
    | instance_expression                   #InstanceExpression
//  | interpolated_string_expression
    | expression argument_list              #InvocationExpression
    | expression IS pattern                 #IsPatternExpression
    | literal_expression                    #LiteralExpression
    | expression op=('.' | '->') simple_name   #MemberAccessExpression
    | '.' simple_name                       #MemberBindingExpression
    | '(' expression ')'                    #ParenthesizedExpression
    | expression op=('++' | '--' | '!')        #PostfixUnaryExpression
    | op=('!' | '&' | '*' | '+' | '++' | '-' | '--' | '^' | '~') expression #PrefixUnaryExpression
    | expression DOUBLE_DOT expression      #RangeExpression
    | REF expression                        #RefExpression
    | SIZEOF '(' type ')'                   #SizeofExpression
    | expression 'switch' '{' NL+ (switch_expression_arm (',' switch_expression_arm)*)? NL+ '}' #SwitchExpression
    | '(' argument (',' argument)+ ')'?     #TupleExpression
    | type                                  #TypeExpression
    | TYPEOF '(' type ')'                   #TypeofExpression
//  | with_expression
  ;
    
instance_expression
    : base_expression
    | self_expression
    ;

base_expression
    : BASE
    ;

self_expression
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
    : attribute_list* modifier* parameter_list (':' type)? '=>' (block | expression)
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

switch_expression_arm
    : pattern when_clause? '=>' expression
    ;
    
pattern
    : pattern op=(OR | AND) pattern                             #BinaryPattern
    | expression                                                #ConstantPattern
    | type variable_designation                                 #DeclarationPattern
    | DISCARD                                                   #DiscardPattern
    | '[' (pattern (',' pattern)*)? ']' variable_designation?   #ListPattern
    | '(' pattern ')'                                           #ParenthesizedPattern
    | op=('!=' | '<' | '<=' | '==' | '>' | '>=') expression     #RelationalPattern
    | '..' pattern?                                             #SlicePattern
    | type                                                      #TypePattern
    | NOT pattern                                               #UnaryPattern
    | VAR variable_designation                                  #VarPattern
  ;

variable_designation
    : DISCARD                                                       #DiscardDesignation
    | '(' (variable_designation (',' variable_designation)*)? ')'   #ParenthesizedVariableDesignation
    | identifier_token                                              #SimpleVariableDesignation
    ;

when_clause
    : WHEN expression
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
    
    
//interpolated_string_expression
//    : '$"' interpolated_string_content* '"'
//    | '$@"' interpolated_string_content* '"'
//    ;
//    
//interpolated_string_content
//    : interpolated_string_text
//    | interpolation
//    ;
//    
//interpolated_string_text
//    : interpolated_string_text_token
//    ;
//
//interpolation
//    : '{' expression interpolation_alignment_clause? interpolation_format_clause? '}'
//    ;
//    
//interpolation_alignment_clause
//    : ',' expression
//    ;
//
//interpolation_format_clause
//    : ':' interpolated_string_text_token
//    ;
//    
//// TODO: not sure about this
//interpolated_string_text_token
//    : CommonCharacter*
//    ;


// =====================================================================================================================
// ================================================= TYPES =============================================================
// =====================================================================================================================
type
    : type array_rank_specifier+                    #ArrayType
    | name                                          #NameType
    | type '?'                                      #NullableType
    | type '*'                                      #PointerType
    | pType=(BOOL | BYTE | SBYTE | INT | UINT | SHORT | USHORT | LONG | ULONG | FLOAT | DOUBLE | STRING | CHAR | OBJECT) #PredefinedType
    | '(' tuple_element (',' tuple_element)+ ')'    #TupleType
    ;
    
tuple_element
  : type identifier_token?
  ;

array_rank_specifier
  : '[' (expression (',' expression)*)? ']'
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
    :  CHARACTER_LITERAL
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
    