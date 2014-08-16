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
using System.Globalization;
using System.Text;
using SGL.Library.Classes;

namespace SGL.Elements
{
    internal class Value : IComparable<Value>
    {
        // return null;
        public static readonly Value NULL = new Value(ValType.Null);
        // no return
        public static readonly Value VOID = new Value(ValType.Void);
        // break;
        public static readonly Value BREAK = new Value(ValType.Break);
        // for error stacktracing
        public static readonly Value INVALID = new Value(ValType.Invalid);

        private readonly ValType type;
        private readonly object value;

        private Value(ValType type)
        {
            // private constructor: only used for NULL, VOID, BREAK, ...
            this.type = type;
        }


        public Value(object value, ValType type)
        {
            if (value == null)
            {
                throw new Exception("v == null");
            }

            this.value = value;
            this.type = type;
            /*
            if (type == ValType.String)
            {
                // escape sequences
                String newString = AsString();
                newString = newString.Replace("\\\\", "\\");
                newString = newString.Replace("\\\"", "\"");
                value = newString;
            }
            */
        }

        public object RawValue
        {
            get { return value; }
        }

        public ValType Type
        {
            get { return type; }
        }

        // object with the value information
        public int IntValue
        {
            get
            {
                // TODO: Exception
                switch (type)
                {
                    case ValType.Integer:
                        return (int) value;
                    case ValType.Double:
                        return Convert.ToInt32((double) value);
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public double DoubleValue
        {
            get
            {
                // TODO: Exception
                if (type == ValType.Double) return (double) value;
                else if (type == ValType.Integer) return Convert.ToDouble((int) value);
                else throw new InvalidOperationException();
            }
        }

        public bool BoolValue
        {
            get
            {
                // TODO: Exception
                if (type == ValType.Boolean) return (bool) value;
                else throw new InvalidOperationException();
            }
        }

        public string StringValue
        {
            get
            {
                // TODO: Exception
                if (type == ValType.String || type == ValType.Layer || type == ValType.Origin ||
                    type == ValType.LoopType || type == ValType.LoopTrigger) return (string)value;
                else throw new InvalidOperationException();
            }
        }

        public AbstractClass ObjectValue
        {
            get
            {
                // TODO: Exception
                if (type == ValType.Object) return (AbstractClass) value;
                else throw new InvalidOperationException();
            }
        }

        public List<Value> ListValue
        {
            get
            {
                // TODO: Exception
                if (type == ValType.List) return (List<Value>) value;
                else throw new InvalidOperationException();
            }
        }

        public Value ReturnValue
        {
            get
            {
                // TODO: Exception
                if (type == ValType.Return) return (Value) value;
                else throw new InvalidOperationException();
            }
        }

        public String TypeString
        {
            get
            {
                switch (type)
                {
                    case ValType.Boolean:
                        return "Boolean";
                    case ValType.Break:
                        return "Break";
                    case ValType.Double:
                        return "Double";
                    case ValType.Integer:
                        return "Integer";
                    case ValType.Layer:
                        return "Layer";
                    case ValType.List:
                        return "List";
                    case ValType.LoopType:
                        return "LoopType";
                    case ValType.Null:
                        return "Null";
                    case ValType.Object:
                        return "Object";
                    case ValType.Origin:
                        return "Origin";
                    case ValType.Return:
                        return "Return";
                    case ValType.String:
                        return "String";
                    case ValType.LoopTrigger:
                        return "LoopTrigger";
                    case ValType.Void:
                        return "Void";
                    default:
                        return "unknown";
                }
            }
        }


        // Compare one value to another

        #region IComparable<Value> Members

        public int CompareTo(Value that)
        {
            try
            {
                // this comparison can fail, but it may be covered by the next comparisons
                if (RawValue is IComparable && that.RawValue is IComparable)
                {
                    // if both can be compared, use that to compare both
                    return ((IComparable) RawValue).CompareTo(that.RawValue);
                }
            }
            finally
            {
                throw new Exception("illegal expression: can't compare `" +
                                    this + "` to `" + that + "`");
            }
        }

        #endregion

        // Checks if one value is the same as another value
        public override Boolean Equals(Object o)
        {
            /*
            ValType thisType = this.GetType();
            
            ValType thatType = that.GetType();
            */
            var that = (Value) o;

            if (TypeEquals(ValType.Double, true) || that.TypeEquals(ValType.Double, true))
            {
                // Float Comparison
                double diff = Math.Abs(DoubleValue - that.DoubleValue);
                return diff < 0.00000000001;
            }
            else if (TypeEquals(ValType.Integer, true) && that.TypeEquals(ValType.Integer, true))
            {
                // Integer Comparison
                return IntValue == that.IntValue;
            }
            else if (Type == ValType.Boolean && that.Type == ValType.Boolean)
            {
                // Boolean Comparison
                return BoolValue == that.BoolValue;
            }
            else if (Type == ValType.String && that.Type == ValType.String)
            {
                // String Comparison
                return StringValue.Equals(that.StringValue, StringComparison.Ordinal);
            }
            else if (Type == that.Type && (Type == ValType.Layer || Type == ValType.LoopType || Type == ValType.Origin || Type == ValType.LoopTrigger))
            {
                // Comparison of other string-like types
                return StringValue.Equals(that.StringValue, StringComparison.Ordinal);
            }
            else
            {
                // everything else shouldn't be compared
                //throw new CompilerException(-1, 204, new String[] {Value.TypeToString(thisType), Value.TypeToString(thatType)});
                // TODO: Exception
                //throw new Exception();
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override String ToString()
        {
            switch (type)
            {
                case ValType.Boolean:
                    return BoolValue.ToString(CultureInfo.InvariantCulture);
                case ValType.Break:
                    return "break";
                case ValType.Double:
                    return DoubleValue.ToString(CultureInfo.InvariantCulture);
                case ValType.Integer:
                    return IntValue.ToString(CultureInfo.InvariantCulture);
                case ValType.Layer:
                    return StringValue;
                case ValType.List:
                    var builder = new StringBuilder();
                    builder.Append("{");
                    for (int i = 0; i < ListValue.Count; i++) // Loop through all strings
                    {
                        builder.Append(ListValue[i]); // Append string to StringBuilder
                        if (i < ListValue.Count - 1)
                            builder.Append(",");
                    }
                    builder.Append("}");
                    string result = builder.ToString(); // Get string from StringBuilder
                    return ListValue.ToString();

                case ValType.LoopType:
                    return StringValue;
                case ValType.Null:
                    return "null";
                case ValType.Object:
                    return "class";
                case ValType.Origin:
                    return StringValue;
                case ValType.LoopTrigger:
                    return StringValue;
                case ValType.Return:
                    return "return";
                case ValType.String:
                    return StringValue;
                case ValType.Void:
                    return "void";
                default:
                    return "unknown";
            }
        }


        /*
        public static String TypeToString(ValType type) {
            switch (type)
            {
                case ValType.Boolean: return "Boolean";
                case ValType.Break: return "Break";
                case ValType.Double: return "Double";
                case ValType.Integer: return "Integer";
                case ValType.Layer: return "Layer";
                case ValType.List: return "List";
                case ValType.LoopType: return "LoopType";
                case ValType.Null: return "Null";
                case ValType.Object: return "Object";
                case ValType.Origin: return "Origin";
                case ValType.Return: return "Return";
                case ValType.String: return "String";
                case ValType.Void: return "Void";
                default: return "unknown";
            }
        }*/

        /// <summary>
        /// TypeCompare method to ensure implizit convertion from int to float and vice versa.
        /// </summary>
        /// <param name="acceptedType">Type to compare with</param>
        /// <returns></returns>
        public bool TypeEquals(ValType acceptedType, bool exactType)
        {
            if (exactType) return type == acceptedType;
            else return TypeEquals(acceptedType);
        }

        public bool TypeEquals(ValType acceptedType)
        {
            switch (acceptedType)
            {
                case ValType.Integer:
                    return type == ValType.Integer || type == ValType.Double;
                case ValType.Double:
                    return type == ValType.Integer || type == ValType.Double;
                case ValType.StrictInteger:
                    return type == ValType.Integer;
                case ValType.StrictDouble:
                    return type == ValType.Double;
                default:
                    return type == acceptedType;
            }
        }


        public static bool TypeCompare(List<Value> valueList, params ValType[] acceptedTypes)
        {
            if (valueList.Count != acceptedTypes.Length) return false;
            for (int i = 0; i < acceptedTypes.Length; i++)
            {
                ValType acceptedType = acceptedTypes[i];
                if (!valueList[i].TypeEquals(acceptedType)) return false;
            }
            return true;
        }

        public static string PrintTypeList(List<Value> valueList)
        {
            String output = "";
            for (int i = 0; i < valueList.Count; i++)
            {
                output += valueList[i].TypeString;
                if (i < valueList.Count - 1)
                {
                    output += ", ";
                }
            }
            return output;
        }
    }
}