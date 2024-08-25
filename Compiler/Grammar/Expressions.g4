parser grammar Expressions;


expression
    : EOF
//  : anonymous_function_expression
//  | anonymous_object_creation_expression
//  | array_creation_expression
//  | assignment_expression
//  | await_expression
//  | base_object_creation_expression
//  | binary_expression
//  | cast_expression
//  | checked_expression
//  | collection_expression
//  | conditional_access_expression
//  | conditional_expression
//  | declaration_expression
//  | default_expression
//  | element_access_expression
//  | element_binding_expression
//  | implicit_array_creation_expression
//  | implicit_element_access
//  | implicit_stack_alloc_array_creation_expression
//  | initializer_expression
  | instance_expression
//  | interpolated_string_expression
//  | invocation_expression
//  | is_pattern_expression
//  | literal_expression
//  | make_ref_expression
//  | member_access_expression
//  | member_binding_expression
//  | omitted_array_size_expression
//  | parenthesized_expression
//  | postfix_unary_expression
//  | prefix_unary_expression
//  | query_expression
//  | range_expression
//  | ref_expression
//  | ref_type_expression
//  | ref_value_expression
//  | size_of_expression
//  | stack_alloc_array_creation_expression
//  | switch_expression
//  | throw_expression
//  | tuple_expression
  | type
//  | type_of_expression
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
