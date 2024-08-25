parser grammar RavenParser2;

import Types, Expressions;

options { tokenVocab=RavenLexer2; superClass=RavenParserBase2; }


compilation_unit
    : package_declaration import_directive*
//     member_declaration* EOF
    ;
    
package_declaration
    : PACKAGE name NL+
    ;
    
import_directive
    : GLOBAL? IMPORT STATIC? type NL+
    ;


// Names
name
    : alias_qualified_name
    | name '.' simple_name // qualified_name
    | simple_name
    ;

alias_qualified_name
    : identifier_name '::' simple_name
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

//qualified_name
//    : name '.' simple_name
//    ;



name_equals
    : identifier_name ASSIGNMENT
    ;

identifier_name
    : GLOBAL
    | identifier_token;


// Types

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





// TODO: statements, expressions, declarations



