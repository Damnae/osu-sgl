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
using SGL.Elements;

namespace SGL.Library.Functions.Calculation
{
    internal class RandFunction : Function
    {
        private Value rand(int minimum, int maximum)
        {
            if (minimum > maximum)
                throw new CompilerException(-1, 317, "rand(min, max)",
                                            "The value for 'min' must be less or equal to the value for 'max'.");
            Random random = GlobalMemory.Instance.Random;
            return new Value(random.Next(minimum, maximum + 1), ValType.Integer);
        }

        public override Value Invoke(List<Value> param)
        {
            if (Value.TypeCompare(param, ValType.Integer, ValType.Integer))
                return rand(param[0].IntValue, param[1].IntValue);

            // TODO: Name
            throw new CompilerException(-1, 302, "rand", Value.PrintTypeList(param));
        }
    }
}