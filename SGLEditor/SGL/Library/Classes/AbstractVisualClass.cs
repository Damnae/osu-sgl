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
using SGL.Storyboard.Generators.Visual;

namespace SGL.Library.Classes
{
    internal abstract class AbstractVisualClass : AbstractClass
    {
        protected AbstractVisualGenerator visualObject;

        public AbstractVisualGenerator Object
        {
            get
            {
                return visualObject;
            }
        }

        public override Value InvokeMethod(String name, List<Value> param)
        {
            switch (name)
            {
                case "fade":
                    if (Value.TypeCompare(param, ValType.Double))
                    {
                        visualObject.fade(param[0].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Double))
                    {
                        visualObject.fade(param[0].IntValue, param[1].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Double, ValType.Double))
                    {
                        visualObject.fade(param[0].IntValue, param[1].IntValue, param[2].DoubleValue,
                                          param[3].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer, ValType.Double,
                                               ValType.Double))
                    {
                        visualObject.fade(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                          param[3].DoubleValue, param[4].DoubleValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

                case "scale":
                    if (Value.TypeCompare(param, ValType.Double))
                    {
                        visualObject.scale(param[0].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Double))
                    {
                        visualObject.scale(param[0].IntValue, param[1].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Double, ValType.Double))
                    {
                        visualObject.scale(param[0].IntValue, param[1].IntValue, param[2].DoubleValue,
                                           param[3].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer, ValType.Double,
                                               ValType.Double))
                    {
                        visualObject.scale(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                           param[3].DoubleValue, param[4].DoubleValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

                case "rotate":
                    if (Value.TypeCompare(param, ValType.Double))
                    {
                        visualObject.rotate(param[0].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Double))
                    {
                        visualObject.rotate(param[0].IntValue, param[1].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Double, ValType.Double))
                    {
                        visualObject.rotate(param[0].IntValue, param[1].IntValue, param[2].DoubleValue,
                                            param[3].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer, ValType.Double,
                                               ValType.Double))
                    {
                        visualObject.rotate(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                            param[3].DoubleValue, param[4].DoubleValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

                case "moveX":
                    if (Value.TypeCompare(param, ValType.Double))
                    {
                        visualObject.moveX(param[0].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Double))
                    {
                        visualObject.moveX(param[0].IntValue, param[1].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Double, ValType.Double))
                    {
                        visualObject.moveX(param[0].IntValue, param[1].IntValue, param[2].DoubleValue,
                                           param[3].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer, ValType.Double,
                                               ValType.Double))
                    {
                        visualObject.moveX(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                           param[3].DoubleValue, param[4].DoubleValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

                case "moveY":
                    if (Value.TypeCompare(param, ValType.Double))
                    {
                        visualObject.moveY(param[0].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Double))
                    {
                        visualObject.moveY(param[0].IntValue, param[1].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Double, ValType.Double))
                    {
                        visualObject.moveY(param[0].IntValue, param[1].IntValue, param[2].DoubleValue,
                                           param[3].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer, ValType.Double,
                                               ValType.Double))
                    {
                        visualObject.moveY(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                           param[3].DoubleValue, param[4].DoubleValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

                case "move":
                    if (Value.TypeCompare(param, ValType.Integer, ValType.Integer))
                    {
                        visualObject.move(param[0].IntValue, param[1].IntValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer))
                    {
                        visualObject.move(param[0].IntValue, param[1].IntValue, param[2].IntValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer, ValType.Integer,
                                               ValType.Integer, ValType.Integer))
                    {
                        visualObject.move(param[0].IntValue, param[1].IntValue, param[2].IntValue, param[3].IntValue,
                                          param[4].IntValue, param[5].IntValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer,
                                               ValType.Integer, ValType.Integer, ValType.Integer, ValType.Integer))
                    {
                        visualObject.move(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                          param[3].IntValue, param[4].IntValue, param[5].IntValue,
                                          param[6].IntValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

                case "scaleVec":
                    if (Value.TypeCompare(param, ValType.Double, ValType.Double))
                    {
                        visualObject.scaleVec(param[0].DoubleValue, param[1].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Double, ValType.Double))
                    {
                        visualObject.scaleVec(param[0].IntValue, param[1].DoubleValue, param[2].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Double, ValType.Double,
                                               ValType.Double, ValType.Double))
                    {
                        visualObject.scaleVec(param[0].IntValue, param[1].IntValue, param[2].DoubleValue,
                                              param[3].DoubleValue, param[4].DoubleValue, param[5].DoubleValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer,
                                               ValType.Double, ValType.Double, ValType.Double, ValType.Double))
                    {
                        visualObject.scaleVec(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                              param[3].DoubleValue, param[4].DoubleValue, param[5].DoubleValue,
                                              param[6].DoubleValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

                case "color":
                    if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer))
                    {
                        visualObject.color(param[0].IntValue, param[1].IntValue, param[2].IntValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer, ValType.Integer))
                    {
                        visualObject.color(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                           param[3].IntValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer,
                                               ValType.Integer, ValType.Integer, ValType.Integer, ValType.Integer,
                                               ValType.Integer))
                    {
                        visualObject.color(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                           param[3].IntValue, param[4].IntValue, param[5].IntValue,
                                           param[6].IntValue, param[7].IntValue);
                        return Value.VOID;
                    }
                    else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer,
                                               ValType.Integer, ValType.Integer, ValType.Integer, ValType.Integer,
                                               ValType.Integer, ValType.Integer))
                    {
                        visualObject.color(param[0].IntValue, param[1].IntValue, param[2].IntValue,
                                           param[3].IntValue, param[4].IntValue, param[5].IntValue,
                                           param[6].IntValue, param[7].IntValue, param[8].IntValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				case "colorHsv":
				case "colorHsb":
					if (Value.TypeCompare(param, ValType.Integer, ValType.Double, ValType.Double)) {
						visualObject.colorHsb(param[0].DoubleValue, param[1].DoubleValue, param[2].DoubleValue);
						return Value.VOID;
					} else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Double, ValType.Double)) {
						visualObject.colorHsb(param[0].IntValue, param[1].DoubleValue, param[2].DoubleValue,
										   param[3].DoubleValue);
						return Value.VOID;
					} else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer,
												 ValType.Double, ValType.Double, ValType.Integer, ValType.Double,
												 ValType.Double)) {
						visualObject.colorHsb(param[0].IntValue, param[1].IntValue, param[2].DoubleValue,
										   param[3].DoubleValue, param[4].DoubleValue, param[5].DoubleValue,
										   param[6].DoubleValue, param[7].DoubleValue);
						return Value.VOID;
					} else if (Value.TypeCompare(param, ValType.Integer, ValType.Integer, ValType.Integer,
												 ValType.Double, ValType.Double, ValType.Double, ValType.Double,
												 ValType.Double, ValType.Double)) {
						visualObject.colorHsb(param[0].IntValue, param[1].IntValue, param[2].IntValue,
										   param[3].DoubleValue, param[4].DoubleValue, param[5].DoubleValue,
										   param[6].DoubleValue, param[7].DoubleValue, param[8].DoubleValue);
						return Value.VOID;
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				case "additive":
					if (Value.TypeCompare(param, ValType.Integer, ValType.Integer)) {
						visualObject.additive(param[0].IntValue, param[1].IntValue);
						return Value.VOID;
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));
				
				case "flipH":
					if (Value.TypeCompare(param, ValType.Integer, ValType.Integer)) {
						visualObject.flipH(param[0].IntValue, param[1].IntValue);
						return Value.VOID;
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

				case "flipV":
					if (Value.TypeCompare(param, ValType.Integer, ValType.Integer)) {
						visualObject.flipV(param[0].IntValue, param[1].IntValue);
						return Value.VOID;
					} else
						throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));

                case "startLoop":
                    if (Value.TypeCompare(param, ValType.Integer, ValType.Integer))
                    {
                        visualObject.startLoop(param[0].IntValue, param[1].IntValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));
                case "startTriggerLoop":
                    if (Value.TypeCompare(param, ValType.LoopTrigger, ValType.Integer, ValType.Integer))
                    {
                        visualObject.startTriggerLoop(param[0].StringValue, param[1].IntValue, param[2].IntValue);
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));
                case "endLoop":
                    if (param.Count == 0)
                    {
                        visualObject.endLoop();
                        return Value.VOID;
                    }
                    else
                        throw new CompilerException(-1, 313, Name, name, Value.PrintTypeList(param));
                default:
                    throw new CompilerException(-1, 314, Name, name);
            }
            //TODO: Exception
            throw new Exception();
        }
    }
}