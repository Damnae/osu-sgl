tree grammar SGLTreeWalker;

options {
	tokenVocab = SGL;
	language = 'CSharp2';
	ASTLabelType=CommonTree;
}

@namespace { SGL.Antlr }

@header {
// -------------------------------------------------------------------------------------------------
//                This is a generated file, please don't change anything in here!
// -------------------------------------------------------------------------------------------------

using System.Text;
using System.Collections.Generic;
using SGL.Elements;
using SGL.Nodes;
using SGL.Nodes.Actions;
using SGL.Nodes.ControlFlow;
using SGL.Nodes.Operators;
using SGL.Nodes.Operators.Arithmetical;
using SGL.Nodes.Operators.Logical;
using SGL.Nodes.Operators.Misc;
using SGL.Nodes.Values;
}

@members{

	Scope currentScope;
	
	private Boolean isMain = false;
      
	public SGLTreeWalker(CommonTreeNodeStream nodes, Boolean isMain) : this(nodes) {
    	this.currentScope = new Scope();
    	this.isMain = isMain;
    }
    
    internal SGLTreeWalker(CommonTreeNodeStream nodes, Scope sc) : this(nodes) {
    	this.currentScope = sc;
    }
    
   	internal override String GetErrorMessage(RecognitionException e, String[] tokenNames) {
  		String message = base.GetErrorMessage(e, tokenNames);
  		throw new CompilerException(e, message); 
  	}
  
}




main returns [AbstractNode node]
	:	block { node = $block.node; }
	;	
	
	
block returns [AbstractNode node]
@init { 
  Scope scope = new Scope(currentScope); 
  currentScope = scope;
  BlockNode bn = new BlockNode(currentScope); 
  node = bn;  
}  
@after { 
  currentScope = currentScope.Parent; 
} 
	:	^(BLOCK   
        	(statement  {bn.AddStatement($statement.node);})*    
     	)  
  	; 
 
  	
statement returns [AbstractNode node]
	//:  	variableDefinitionList
	:	definition { node = $definition.node; }
	|	assignment { node = $assignment.node; } // a = 1;
	|	unaryExpression { node = $unaryExpression.node; }
	|	functionCall { node = $functionCall.node; } // move(100,200)
	|	objectFunctionCall { node = $objectFunctionCall.node; } // move(100,200)
	|	ifStatement { node = $ifStatement.node; }
	|	atStatement { node = $atStatement.node; } 
	|	whileLoop { node = $whileLoop.node; }
	|	forLoop { node = $forLoop.node; }
	|	breakStat { node = $breakStat.node; }
	|	returnStat { node = $returnStat.node; }
	;  		
	
	
atStatement returns [AbstractNode node]
	:	^(AT expression block) { node = new AtNode($expression.node, $block.node); }
	;
	
returnStat returns [AbstractNode node]
	:	^(RETURN expression?) { node = new ReturnNode($expression.node); }
	;	
		
functionCall returns [AbstractNode node]
	:	^(FUNC_CALL Identifier expressionList?) { node = new InvokeFunctionNode($Identifier.text, $expressionList.list, $Identifier.Line); }
	;	
	
breakStat returns [AbstractNode node]
	:	BREAK { node = new BreakNode($BREAK.Line); }
	;
	
objectFunctionCall returns [AbstractNode node]
	:	^(OBJ_FUNC_CALL variable indexes? Identifier expressionList?)
	{ node = new InvokeFunctionNode($variable.txt, $indexes.list, $Identifier.text, $expressionList.list, currentScope, $Identifier.Line); }
	;	
	
whileLoop returns [AbstractNode node]
	:	^('while' expression block)
	{ node = new WhileNode($expression.node, $block.node); }
	;
	
ifStatement returns [AbstractNode node] 
@init  { 
  IfNode ifNode = new IfNode(); 
  node = ifNode; 
}   
  :  ^(IF   
       (^(EXP expression b1=block) { ifNode.AddChoice($expression.node,$b1.node); } )+   
       (^(EXP b2=block) { ifNode.AddChoice(new AtomNode(true, ValType.Boolean),$b2.node); } )?  
     )  
  ;	
	
forLoop returns [AbstractNode node]
@init {
  // We have to use 2 Blocks for the for-Node to work correctly
  // Create new block for the beginning of the for-Loop
  Scope scope = new Scope(currentScope); 
  currentScope = scope;
  BlockNode bn = new BlockNode(currentScope); 
  
  //Create the For-Node and add it to the block
  ForNode forNode = new ForNode();
  bn.AddStatement(forNode);
  
  // Return the block node
  node = bn;
}  
@after { 
  currentScope = currentScope.Parent; 
} 
	:	^('for' ^(FORDEC (dec=statement { forNode.Init = $dec.node; } )*) ^(FORCOND (cond=expression { forNode.Condition = $cond.node; })?) ^(FORITER (iter=statement { forNode.Iteration = $iter.node; })?) block)
	{ forNode.Block = $block.node; }
	;	
	
	
expressionList returns [List<AbstractNode> list]
@init { list = new List<AbstractNode>(); }
    :	^(EXP_LIST   
    		(expression {list.Add($expression.node);})+
    	)	
    ;

indexes returns [List<AbstractNode> list]
@init { list = new List<AbstractNode>(); }
  :  ^(INDEXES 
  		(expression {list.Add($expression.node);})+
  	)
  ;    	
	
variable returns [String txt]
	:	Identifier { txt = $Identifier.text; }
	;
	
assignment returns [AbstractNode node]
	:	^(ASSIGN Identifier indexes? expression)  	
	        { node = new AssignVariableNode($Identifier.text,$indexes.list,$expression.node,currentScope,$Identifier.Line); }
	;
	
definition returns [AbstractNode node]
	:	^(DEF Identifier)  	
	        { node = new DefineVariableNode($Identifier.text,currentScope,$Identifier.Line); }
	|	^(GLOBALDEF Identifier)  	
	        { node = new DefineVariableNode($Identifier.text,$Identifier.Line); }	
	;	
		
	
// start rule for all sorts of expressions
expression returns [AbstractNode node]
	:	^('+' a=expression b=expression) { node = new AddNode($a.node, $b.node); }
	|	^('-' a=expression b=expression) { node = new SubNode($a.node, $b.node); }
	|	^('*' a=expression b=expression) { node = new MultNode($a.node, $b.node); }
	|	^('/' a=expression b=expression) { node = new DivNode($a.node, $b.node); }
	|	^('%' a=expression b=expression) { node = new ModNode($a.node, $b.node); }
	|	^('^' a=expression b=expression) { node = new PowNode($a.node, $b.node); }
	|	^(INT_NEGATE a=expression) { node = new NegateIntNode($a.node); }
	|	^(BOOL_NEGATE a=expression) { node = new NegateBoolNode($a.node); }
	|	^('<' a=expression b=expression) { node = new LowerThanNode($a.node, $b.node); }
	|	^('<=' a=expression b=expression) { node = new LowerThanEqualsNode($a.node, $b.node); }
	|	^('>' a=expression b=expression) { node = new GreaterThanNode($a.node, $b.node); }
	|	^('>=' a=expression b=expression) { node = new GreaterThanEqualsNode($a.node, $b.node); }
	|	^('!=' a=expression b=expression) { node = new NotEqualsNode($a.node, $b.node); }
	|	^('==' a=expression b=expression) { node = new EqualsNode($a.node, $b.node); }
	|	^('&&' a=expression b=expression) { node = new AndNode($a.node, $b.node); }
	|	^('||' a=expression b=expression) { node = new OrNode($a.node, $b.node); }
	|	^('?' a=expression b=expression c=expression) { node = new ConditionalNode($a.node, $b.node, $c.node); }
	|  	IntAtom { node = new AtomNode(int.Parse($IntAtom.text, System.Globalization.CultureInfo.InvariantCulture), ValType.Integer, $IntAtom.Line); }
	|	FloatAtom { node = new AtomNode(Double.Parse($FloatAtom.text, System.Globalization.CultureInfo.InvariantCulture), ValType.Double,  $FloatAtom.Line); }
	|  	BooleanAtom { node = new AtomNode(Boolean.Parse($BooleanAtom.text), ValType.Boolean, $BooleanAtom.Line); }
	|	^(STRING StringAtom) { node = new AtomNode(($StringAtom.text).Substring(1, ($StringAtom.text).Length-2), ValType.String, $StringAtom.Line); }
	|	LayerAtom { node = new AtomNode($LayerAtom.text, ValType.Layer, $LayerAtom.Line); }
	|	OriginAtom { node = new AtomNode($OriginAtom.text, ValType.Origin, $OriginAtom.Line); }
	|	LoopTypeAtom { node = new AtomNode($LoopTypeAtom.text, ValType.LoopType, $LoopTypeAtom.Line); }
	|	LoopTriggerAtom { node = new AtomNode($LoopTriggerAtom.text, ValType.LoopTrigger, $LoopTriggerAtom.Line); }
	|	Null { node = new AtomNode($Null.text, ValType.Null,  $Null.Line); }
	|	instantiateClass { node = $instantiateClass.node; }
	|	lookup { node = $lookup.node; }
	|	unaryExpression { node = $unaryExpression.node; }
    ;     	
	
unaryExpression returns [AbstractNode node]
    :   ^(VARINC Identifier) { node = new VarIncNode($Identifier.text, currentScope, $Identifier.Line); }
	|	^(VARDEC Identifier) { node = new VarDecNode($Identifier.text, currentScope, $Identifier.Line); }
	;
	
	
instantiateClass returns [AbstractNode node]
	:	^(CLASS Identifier expressionList?) {  node = new InstanciateClassNode($Identifier.text, $expressionList.list, $Identifier.Line); }
	;  		
	
lookup returns [AbstractNode node]
  :  ^(LOOKUP objectFunctionCall i=indexes?)	{node = $i.list != null ? new LookupNode($objectFunctionCall.node, $indexes.list) : $objectFunctionCall.node;}
  |  ^(LOOKUP Identifier i=indexes?)        
  	{AbstractNode identNode = new IdentifierNode($Identifier.text, currentScope, $Identifier.Line);
  	node = $i.list != null ? new LookupNode(identNode, $indexes.list) : identNode;}
  |  ^(LOOKUP functionCall i=indexes?)   		{node = $i.list != null ? new LookupNode($functionCall.node, $indexes.list) : node = $functionCall.node;}
  ;
	
	
	