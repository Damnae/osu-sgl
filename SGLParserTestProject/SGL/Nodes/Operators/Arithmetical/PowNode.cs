﻿using System;
using System.Collections.Generic;
using System.Text;
using SGL.Elements;

namespace SGL.Nodes.Operators.Arithmetical
{
    class PowNode : AbstractBinaryOperatorNode
    {
        private AbstractNode lhs;
        private AbstractNode rhs;

        public PowNode(AbstractNode node1, AbstractNode node2) : base(node1, node2) { }

        public override Value Operate(Value value1, Value value2)
        {

            // number + number
            if (value1.Type == ValType.Integer && value2.Type == ValType.Integer)
            {
                double x = Math.Pow(value1.DoubleValue, value1.DoubleValue);
                //Console.WriteLine(x);
                if (x == Double.PositiveInfinity)
                {
                    throw new CompilerException(Line, 402, "Double");
                }
                else if (x == Double.NegativeInfinity)
                {
                    throw new CompilerException(Line, 401, "Double");
                }
                else if (x == Double.NaN)
                {
                    throw new CompilerException(Line, 403, "Double");
                }

                int Int;
                bool isInt = Int32.TryParse(x.ToString(), out Int);
                if (isInt)
                    return new Value(Int, ValType.Integer);
                else
                    return new Value(x, ValType.Double);
            }
            // float + number / number + float  
            else if (value1.TypeEquals(ValType.Double) && value2.TypeEquals(ValType.Double))
            {
                double x = Math.Pow(value1.DoubleValue, value1.DoubleValue);
                //Console.WriteLine(x);
                if (x == Double.PositiveInfinity)
                {
                    throw new CompilerException(Line, 402, "Double");
                }
                else if (x == Double.NegativeInfinity)
                {
                    throw new CompilerException(Line, 401, "Double");
                }
                else if (x == Double.NaN)
                {
                    throw new CompilerException(Line, 403, "Double");
                }
                return new Value(x, ValType.Double);
            }
            else
            {
                throw new CompilerException(Line, 202, "^", value1.TypeString, value2.TypeString);
            }
        }
    }
}