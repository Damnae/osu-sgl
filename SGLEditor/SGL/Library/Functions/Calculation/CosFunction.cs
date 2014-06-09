using System;
using System.Collections.Generic;
using SGL.Elements;

namespace SGL.Library.Functions.Calculation
{
    internal class CosFunction : Function
    {
        private Value cos(double value)
        {
            return new Value(Math.Cos(value), ValType.Double);
        }

        public override Value Invoke(List<Value> param)
        {
			if (Value.TypeCompare(param, ValType.Double)) return cos(param[0].DoubleValue);

            // TODO: Name
			throw new CompilerException(-1, 302, "cos", Value.PrintTypeList(param));
        }
    }
}