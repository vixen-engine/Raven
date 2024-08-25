parser grammar Types;

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
    