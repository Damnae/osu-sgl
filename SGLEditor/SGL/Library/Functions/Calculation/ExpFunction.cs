using System;
using System.Collections.Generic;
using SGL.Elements;

namespace SGL.Library.Functions.Calculation
{
    internal class ExpFunction : Function
    {
        private Value exp(double originalNum, double power)
        {
            Double newNum = Math.Pow(originalNum, power);
            return new Value(newNum, ValType.Double);
        }

        public override Value Invoke(List<Value> param)
        {
            if (Value.TypeCompare(param, ValType.Double, ValType.Double)) return exp(param[0].DoubleValue, param[1].DoubleValue);

            // TODO: Name
            throw new CompilerException(-1, 302, "exp", Value.PrintTypeList(param));
        }
    }
}
