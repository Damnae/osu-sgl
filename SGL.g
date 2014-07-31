grammar SGL;


options {
	output = 'AST';
	language = 'CSharp2';
	// we need this to prevent the ambiguity arising from 'assignment' (assignmentList) and 'objectMethodCall'
	// it should only be used in 2 cases, so it shouldn't slow down the parsing so much.
	// It implicitly adds a syn pred in front of every production, using them only if static grammar LL* analysis fails. Syn pred code is not generated if the pred is not used in a decision.
	backtrack = true; 
	// this effectively guarantees linear parsing when backtracking at the cost of more memory.
	memoize = true;
}

tokens {
	BLOCK;
	DEF;
	GLOBALDEF;
	ASSIGN;
	STRING;
	INT_NEGATE;
	BOOL_NEGATE;
	VARINC;
	VARDEC;
	VARIABLE;
	INDEXES;
	LOOKUP;
	LIST;
	EXP_LIST;
	VAR_LIST;
	FUNC_CALL;
	OBJ_FUNC_CALL;
	BREAK;
	RETURN;
	AT;
	IF;
	EXP;
	FORDEC;
	FORCOND;
	FORITER;
	GLOBAL_ASSIGN;
	CLASS;
}	




@lexer::namespace { SGL.Antlr }
@parser::namespace { SGL.Antlr }

@lexer::header {
// -------------------------------------------------------------------------------------------------
//                This is a generated file, please don't change anything in here!
// -------------------------------------------------------------------------------------------------
}

@parser::header {
// -------------------------------------------------------------------------------------------------
//                This is a generated file, please don't change anything in here!
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using SGL;
using SGL.Elements;
using SGL.Library.Functions;
}



@members {


    	private void DefineFunction(String name, Object paramList, Object block, int defLine) {
                // 'paramList' is possibly null!  Create an empty tree in that case.  
                CommonTree paramListTree = paramList == null ? new CommonTree() : (CommonTree)paramList;
				List<String> functionParameters = ParametersToList(paramListTree);

                // 'block' is never null 
                CommonTree blockTree = (CommonTree)block;
                

                GlobalMemory.Instance.RegisterFunction(name, new UserFunction(name, functionParameters, blockTree, defLine));
        }
        
        private List<String> ParametersToList(CommonTree tree)
        {
            List<String> ids = new List<String>();
            // convert the tree to a List of Strings
            if (tree.ChildCount > 0)
            {
                for (int i = 0; i < tree.ChildCount; i++)
                {
                    CommonTree child = (CommonTree)tree.GetChild(i);
                    ids.Add(child.Text);
                }

            return ids;
        }
        
		public override String GetErrorMessage(RecognitionException e, String[] tokenNames) {
   			String message = base.GetErrorMessage(e, tokenNames);
   			throw new CompilerException(e, message); 
   		}

}


	
	
	
	
/* Parser */	
main 
    :	mainBlock
	;
	
	
// Blocks
// The rule for the top level (main) block
// only mainStatements are in the AST, methodDef's and objectMethodDef's won't be in the AST
mainBlock
	:	(mainStatement | functionDef)*
     		-> ^(BLOCK  mainStatement*) 
     	;

// A block with multiple statements, enclosed with { ... }     	
block
	:	statement*
		-> ^(BLOCK statement*)
	;
	
// The block type for single statements, e.g. for if's with only one statement	
singleBlock
	:	semicolonStatement
		-> ^(BLOCK semicolonStatement)
	;
	
// Typical block where you can either have { ... } OR statement; - e.g. for if's
commonBlock
	:	singleBlock
	|	'{'! block '}'!
	;	
	
	
//	
mainStatement
	:	statement
	|	globalVarDefList ';'!	
	;	
	
statement
	:	semicolonStatement
	|	ifStatement
	|	atStatement
	|	whileLoop
	|	forLoop
	;
	
semicolonStatement
	:	(unaryExpression // i++;
	|	objectMethodCall
	|	methodCall	
	|	varDefList // a = 1, b = 2, c; / $a = 1;
	|	assignment	 
	|	breakStat
	|	returnStat
	)	';'!
	;
	
oneLineStatement
	:	varDefList
	|	unaryExpression
	;			
	
varDefList
	:	('var'! varDef) 
		(','! varDef)*
	;
	
varDef
	:	variable ('=' expression)* -> ^(DEF variable) (^(ASSIGN variable expression))*
	;
	
globalVarDefList
	:	'global' 'var' variable ('=' expression)? (',' variable ('=' expression)?)* -> ^(GLOBALDEF variable)+ ^(ASSIGN variable expression?)*
	;
		
	
assignment
	:	variable indexes? ('=' expression)?  -> ^(ASSIGN variable indexes? expression?)
	;	
	
indexes
  	:  	('[' expression ']')+ -> ^(INDEXES expression+)
  	;	
  
objectMethodCall
	:	variable indexes? '.' Identifier '(' expressionList? ')' -> ^(OBJ_FUNC_CALL variable indexes? Identifier expressionList?)
	;	  
	
/* Extracts the $ in front of the variable */	
variable
	:	Identifier -> Identifier
	;		
	

/* Extra String rule for extracting the Quotationmarks later */
stringAtom
	:   StringAtom -> ^(STRING StringAtom)
	;


// Function definition
functionDef
	:	'function' Identifier '(' variableList? ')' commonBlock
		{ DefineFunction($Identifier.text, $variableList.tree, $commonBlock.tree, $Identifier.line); } 
	;

	
// Expression Handling
// (One Hell of a topic)
// The rules are sortet, so that the expression with the least binding power comes first
/* ----------------------------------------------------------------------------------------------------------------------------------------------- */	
	
// start rule for all sorts of expressions
// they can be class initialisations like "new Bla()" without any operators, or
// more expressions and atoms binded with operators
expression
    :	instantiateClass  
    |	conditionalExpression
    ; 
    

// condition ? if true then : if false then    
conditionalExpression
    :   conditionalOrExpression ( '?'^ conditionalExpression ':'! conditionalExpression )?
    ;        
       
    
// OR     
conditionalOrExpression
    :   conditionalAndExpression ( '||'^ conditionalAndExpression )*
    ;    
    
// AND    
conditionalAndExpression
    :   equalityExpression ( '&&'^ equalityExpression )*
    ;
    
// Is (not) equal to    
equalityExpression
    :   relationalExpression ( ('=='^ | '!='^) relationalExpression )*
    ;    
    
// Comparison <, > , <=, =>    
relationalExpression
    :   additiveExpression (('<'^|'>'^|'<='^|'>='^) additiveExpression)*
    ;            
    
// + / -    
additiveExpression
    :   multiplicativeExpression (('+'^|'-'^) multiplicativeExpression)*
    ;        
    
// * / / / %
multiplicativeExpression
    :   powExpression (('*'^|'/'^|'%'^) powExpression)*
    ;
    
powExpression
	:	negateExpression ('^'^ negateExpression)*
	;    

// - (...)
negateExpression
	:	'-' mathAtom -> ^(INT_NEGATE mathAtom)
	|	'!' mathAtom -> ^(BOOL_NEGATE mathAtom)
	|	unaryExpression
	|	mathAtom
	;
	
// Increase/Decrease a variable by 1		
unaryExpression
    :   Identifier ('++' -> ^(VARINC Identifier)
	|	'--' -> ^(VARDEC Identifier))
    ;       
	
	
// (...) / value / variable / method like rand(...)  
mathAtom
	:	'('! expression ')'!			// expression in Brackets
    |	IntAtom							// integer value
    |	FloatAtom						// float value
    |   BooleanAtom						// boolean value
	|	stringAtom						// string value    
	|	LayerAtom						// layer value
	|	OriginAtom						// origin value
	|	LoopTypeAtom    				// looptype value
	|	LoopTriggerAtom					// looptrigger value
	|	ColorAtom						// color value in format: #fff or #ffffff
	|	Null							// null...
	|	lookup
    ; 
    
lookup
	:	objectMethodCall indexes? -> ^(LOOKUP objectMethodCall indexes?)
	|	methodCall indexes? -> ^(LOOKUP methodCall indexes?)	
	|	variable indexes? -> ^(LOOKUP variable indexes?)			// any type of variable identifier
	;    
    
    
// TODO    
instantiateClass
	:	'new' 
	(	Identifier '(' expressionList? ')' -> ^(CLASS Identifier expressionList?)
	);

  
expressionList
 	:  	expression (',' expression)* -> ^(EXP_LIST expression+)
	;
  
  
variableList
 	:	variable (',' variable)* -> ^(VAR_LIST variable+)
	;
	
methodCall
	:	Identifier '(' expressionList? ')' -> ^(FUNC_CALL Identifier expressionList?)
	;
	
	
	
breakStat
	:	'break' -> BREAK
	;

returnStat
	:	'return' expression -> ^(RETURN expression)
	;	
	
	
whileLoop
	:	'while' expression commonBlock -> ^('while' expression commonBlock)
	;
	
forLoop
	:	'for' '(' dec=oneLineStatement? ';' cond=expression? ';' iter=oneLineStatement? ')' commonBlock
	->	^('for' ^(FORDEC $dec?) ^(FORCOND $cond?) ^(FORITER $iter?) commonBlock)
	;	

ifStatement
	:	ifStat elseIfStat* elseStat? -> ^(IF ifStat elseIfStat* elseStat?)
	;
ifStat
	:	'if' expression commonBlock -> ^(EXP expression commonBlock)
	;

elseIfStat
	:	'else' 'if' expression commonBlock -> ^(EXP expression commonBlock)
	;
	
elseStat
	:	'else' commonBlock -> ^(EXP commonBlock)  
	;		

atStatement
	:	'at' expression commonBlock -> ^(AT expression commonBlock)
	;	
	
	
/* Lexer */

VarStartInit
	:	'var'
	;	
	
    
BooleanAtom
    :   'true'
    |   'false'
    ;
    
Null
	:	'null'
	;    
       

/* Specific classes and values */   
Array
	:	'Array'
	;	      

LayerAtom
	:	'Background' 
	|	'Fail'
	|	'Pass'
	|	'Foreground'
	;
	
OriginAtom
	:	'TopLeft'
	|	'TopCentre'
	|	'TopRight'
	|	'CentreLeft'
	|	'Centre'
	|	'CentreRight'
	|	'BottomLeft'
	|	'BottomCentre'
	|	'BottomRight'
	;
	
LoopTypeAtom
	:	'LoopForever'
	|	'LoopOnce'
	;
	
LoopTriggerAtom
	:	'HitSoundClap'
	|	'HitSoundFinish'
	|	'HitSoundWhistle'
	|	'Passing'
	|	'Failing'
	;    	    
	
ColorAtom
	:	'#' HexDigit HexDigit HexDigit
	|	'#' HexDigit HexDigit HexDigit HexDigit HexDigit HexDigit
	;	

IntAtom
    :	'0'..'9'+
    ;

FloatAtom
    :   ('0'..'9')+ '.' ('0'..'9')*
    |   '.' ('0'..'9')+
    |   ('0'..'9')+
    ;


StringAtom
    :  '"' ( EscapeSequence | '\\' | ~('"') )* '"'
    ;	
	
// Use this as names (identifiers) for  variables, methods and so on
Identifier 
    :   UTF8Letter (UTF8Letter|UTF8Digit)*
    ;


/* Hidden */
Comment
    :   '//' ~('\n'|'\r')* '\r'? '\n' {$channel=HIDDEN;}
    |   '/*' ( options {greedy=false;} : . )* '*/' {$channel=HIDDEN;}
    ;

WhiteSpace
	:   ( ' '
        | '\t'
        | '\r'
        | '\n'
        ) {$channel=HIDDEN;}
    ;


/* I found this char range in JavaCC's grammar and modified it, but Letter and Digit overlap.
   Still works, but...
 */
fragment
UTF8Letter
    :  '\u0041'..'\u005a' |
       '\u005f' |
       '\u0061'..'\u007a' |
       '\u00c0'..'\u00d6' |
       '\u00d8'..'\u00f6' |
       '\u00f8'..'\u00ff' |
       '\u0100'..'\u1fff' |
       '\u3040'..'\u318f' |
       '\u3300'..'\u337f' |
       '\u3400'..'\u3d2d' |
       '\u4e00'..'\u9fff' |
       '\uf900'..'\ufaff'
    ;

fragment
UTF8Digit
    :  '\u0030'..'\u0039' |
       '\u0660'..'\u0669' |
       '\u06f0'..'\u06f9' |
       '\u0966'..'\u096f' |
       '\u09e6'..'\u09ef' |
       '\u0a66'..'\u0a6f' |
       '\u0ae6'..'\u0aef' |
       '\u0b66'..'\u0b6f' |
       '\u0be7'..'\u0bef' |
       '\u0c66'..'\u0c6f' |
       '\u0ce6'..'\u0cef' |
       '\u0d66'..'\u0d6f' |
       '\u0e50'..'\u0e59' |
       '\u0ed0'..'\u0ed9' |
       '\u1040'..'\u1049'
   ;

fragment
HexDigit : ('0'..'9'|'a'..'f'|'A'..'F') ;

fragment
EscapeSequence
    :   '\\' ('n'|'r'|'\"'|'\\')
    ;

