//  Storyboard Generation Language
//  Copyright (C) 2013 Dominik Halfkann
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using Antlr.Runtime.Tree;
using SGL.Antlr;
using SGL.Elements;

namespace SGL.Library.Functions
{
    /// <summary>
    /// A method defined by "method [name]() { ... } in the actuall SGL code."
    /// </summary>
    internal class UserFunction : Function
    {
        private readonly CommonTree functionBody;
        private readonly List<String> parameterNames;
        private int definedLine;
        private String name;

        // method xyt(param1, param2) {
        public UserFunction(String name, List<String> parameterNames, CommonTree functionBody, int definedLine)
        {
            this.parameterNames = parameterNames;
            this.functionBody = functionBody;
            this.name = name;
            this.definedLine = definedLine;
        }

        public override Value Invoke(List<Value> parameters)
        {
            if (parameters.Count == parameterNames.Count)
            {
                // create new scope for the method call
                var methodScope = new Scope();
                for (int i = 0; i < parameterNames.Count; i++)
                {
                    methodScope.Define(parameterNames[i]);
                    methodScope.Assign(parameterNames[i], parameters[i]);
                }

                var nodeStream = new CommonTreeNodeStream(functionBody);
                // Create a tree walker to evaluate this method's code block  
                var walker = new SGLTreeWalker(nodeStream, methodScope);

                // Ok, executing the function then
                Value returnValue = walker.main().Evaluate();
                if (returnValue.Type == ValType.Return)
                {
                    return returnValue.ReturnValue;
                }
                else
                {
                    return returnValue;
                }

                // we shouldn't check the return type
                /*if (!returnValue.GetVarType().Equals(this.returnType))
                {
                    throw new Exception("The method doesn't return the expected return type (" + returnValue.ToString()  + " is not from type " + this.returnType + ")");
                }*/
                
            }
            // TODO: Handle exception properly
            throw new Exception("A method specified an incorrect number of parameters (expected " + parameters.Count + ", got " + parameterNames.Count + ")");
        }

        public Value Invoke(Value objectVar, List<Value> parameters)
        {
            if (parameters.Count == parameterNames.Count)
            {
                // create new scope for the method call
                var methodScope = new Scope();
                methodScope.Define("this");
                methodScope.Assign("this", objectVar);
                for (int i = 0; i < parameterNames.Count; i++)
                {
                    methodScope.Define(parameterNames[i]);
                    methodScope.Assign(parameterNames[i], parameters[i]);
                }


                var nodeStream = new CommonTreeNodeStream(functionBody);
                // Create a tree walker to evaluate this method's code block  
                var walker = new SGLTreeWalker(nodeStream, methodScope);

                Value returnValue = null;

                // Ok, executing the function then
                returnValue = walker.main().Evaluate();


                // we shouldn't check the return type
                /*if (!returnValue.GetVarType().Equals(this.returnType))
                {
                    throw new Exception("The method doesn't return the expected return type (" + returnValue.ToString()  + " is not from type " + this.returnType + ")");
                }*/
                return returnValue;
            }
            // TODO: Handle exception properly
            throw new Exception("A method specified an incorrect number of parameters (expected " + parameters.Count + ", got " + parameterNames.Count + ")");
        }
    }
}