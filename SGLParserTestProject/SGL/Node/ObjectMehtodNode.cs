﻿using System;
using System.Collections.Generic;
using System.Text;
using SGL;

namespace SGL.Node
{
    class ObjectMehtodNode : SGLNode
    {
        private String variable;
        private String method;
        private List<SGLNode> arguments;
        private Scope scope;
        private int line;

        public ObjectMehtodNode(String variable, String method, List<SGLNode> arguments, Scope scope, int line)
        {
            this.variable = variable;
            this.method = method;
            this.arguments = arguments;
            this.scope = scope;
            this.line = line;
        }

        public SGLValue Evaluate()
        {
            // resolve the sprite/animation object
            SGLValue sprite = scope.Resolve(variable);
            if (sprite == null)
            {
                throw new SGLCompilerException(GetLine(), "unknown variable", "'" + sprite + "' cannot be resolved to a variable");
            }
            else if (!sprite.IsObject())
            {
                throw new SGLCompilerException(GetLine(), "type mismatch", "cannot convert from " + sprite.GetVarType() + " to Object");
            }
            SGLObject obj = sprite.AsObject();

            // resolve arguments
            List<SGLValue> values = new List<SGLValue>();
            foreach(SGLNode exprNode in arguments) {
                values.Add(exprNode.Evaluate());
            }

            // resolve current offset from scope
            int offset = scope.GetOffset();

            // find method to execute
            if (method.Equals("move"))
            {
                if (values.Count == 2)
                {
                    CheckParameters("move", values, new String[] { "int", "int" });
                    double[] startParams = new double[] { values[0].AsInteger(), values[1].AsInteger() };
                    obj.moveCmds.Add(new SbCommand(0, offset, offset, startParams, startParams));
                    obj.x = values[0].AsInteger();
                    obj.y = values[1].AsInteger();
                    //obj.AddSbCode(" M,0," + offset + "," + offset + ",," + values[0].ToString() + "," + values[1].ToString());
                }
                else if (values.Count == 3)
                {
                    CheckParameters("move", values, new String[] { "int", "int", "int" });
                    int time = offset + values[0].AsInteger();
                    double[] startParams = new double[] { values[1].AsInteger(), values[2].AsInteger() };
                    obj.moveCmds.Add(new SbCommand(0, time, time, startParams, startParams));
                    obj.x = values[1].AsInteger();
                    obj.y = values[2].AsInteger();
                    //obj.AddSbCode(" M,0," + time + "," + time + ",," + values[1].ToString() + "," + values[2].ToString());
                }
                else if (values.Count == 6)
                {
                    CheckParameters("move", values, new String[] { "int", "int", "int", "int", "int", "int" });
                    int stime = offset + values[0].AsInteger();
                    int etime = offset + values[1].AsInteger();
                    double[] startParams = new double[] { values[2].AsInteger(), values[3].AsInteger() };
                    double[] endParams = new double[] { values[4].AsInteger(), values[5].AsInteger() };
                    obj.moveCmds.Add(new SbCommand(0, stime, etime, startParams, startParams));
                    obj.x = values[4].AsInteger();
                    obj.y = values[5].AsInteger();
                    //obj.AddSbCode(" M,0," + stime + "," + etime + "," + values[2].ToString() + "," + values[3].ToString() + "," + values[4].ToString() + "," + values[5].ToString());
                }
                else if (values.Count == 7)
                {
                    CheckParameters("move", values, new String[] { "int", "int", "int", "int", "int", "int", "int" });
                    CheckRange("easing", values[0], 0, 2);
                    int easing = values[0].AsInteger();
                    int stime = offset + values[1].AsInteger();
                    int etime = offset + values[2].AsInteger();
                    double[] startParams = new double[] { values[3].AsInteger(), values[4].AsInteger() };
                    double[] endParams = new double[] { values[5].AsInteger(), values[6].AsInteger() };
                    obj.moveCmds.Add(new SbCommand(easing, stime, etime, startParams, startParams));
                    obj.x = values[5].AsInteger();
                    obj.y = values[6].AsInteger();
                    //obj.AddSbCode(" M," + values[0].ToString() + "," + stime + "," + etime + "," + values[3].ToString() + "," + values[4].ToString() + "," + values[5].ToString() + "," + values[6].ToString());
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }
            }
            else if (method.Equals("fade"))
            {
                if (values.Count == 1)
                {
                    CheckParameters("fade", values, new String[] { "number" });
                    CheckRange("opacity", values[0], 0, 1);
                    double[] startParams = new double[] { values[0].AsFloat() };
                    obj.fadeCmds.Add(new SbCommand(0, offset, offset, startParams, startParams));
                    obj.opacity = values[0].AsFloat();
                    //obj.AddSbCode(" F,0," + offset + "," + offset + ",," + values[0].ToString());
                }
                else if (values.Count == 2)
                {
                    CheckParameters("fade", values, new String[] { "int", "number" });
                    CheckRange("opacity", values[1], 0, 1);
                    int time = offset + values[0].AsInteger();
                    double[] startParams = new double[] { values[1].AsFloat() };
                    obj.fadeCmds.Add(new SbCommand(0, time, offset, startParams, startParams));
                    obj.opacity = values[1].AsFloat();
                    //obj.AddSbCode(" F,0," + time + "," + time + ",," + values[1].ToString());
                }
                else if (values.Count == 4)
                {
                    CheckParameters("fade", values, new String[] { "int", "int", "number", "number" });
                    CheckRange("startOpacity", values[2], 0, 1);
                    CheckRange("endOpacity", values[3], 0, 1);
                    int stime = offset + values[0].AsInteger();
                    int etime = offset + values[1].AsInteger();
                    double[] startParams = new double[] { values[2].AsFloat() };
                    double[] endParams = new double[] { values[3].AsFloat() };
                    obj.fadeCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.opacity = values[3].AsFloat();
                    //obj.AddSbCode(" F,0," + stime + "," + etime + "," + values[2].ToString() + "," + values[3].ToString());
                }
                else if (values.Count == 5)
                {
                    CheckParameters("fade", values, new String[] { "int", "int", "int", "number", "number" });
                    CheckRange("easing", values[0], 0, 2);
                    CheckRange("startOpacity", values[3], 0, 1);
                    CheckRange("endOpacity", values[4], 0, 1);
                    int easing = values[0].AsInteger();
                    int stime = offset + values[1].AsInteger();
                    int etime = offset + values[2].AsInteger();
                    double[] startParams = new double[] { values[3].AsFloat() };
                    double[] endParams = new double[] { values[4].AsFloat() };
                    obj.fadeCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.opacity = values[4].AsFloat();
                    //obj.AddSbCode(" F," + values[0].ToString() + "," + stime + "," + etime + "," + values[3].ToString() + "," + values[4].ToString());
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }
            }
            else if (method.Equals("scale"))
            {
                if (values.Count == 1)
                {
                    CheckParameters("scale", values, new String[] { "number" });
                    CheckRange("scalefactor", values[0], 0);
                    double[] startParams = new double[] { values[0].AsFloat() };
                    obj.scaleCmds.Add(new SbCommand(0, offset, offset, startParams, startParams));
                    obj.scaleX = values[0].AsFloat();
                    obj.scaleY = values[0].AsFloat();
                    //obj.AddSbCode(" S,0," + offset + "," + offset + ",," + values[0].ToString());
                }
                else if (values.Count == 2)
                {
                    CheckParameters("scale", values, new String[] { "int", "number" });
                    CheckRange("scalefactor", values[0], 0);
                    int time = offset + values[0].AsInteger();
                    double[] startParams = new double[] { values[1].AsFloat() };
                    obj.scaleCmds.Add(new SbCommand(0, time, offset, startParams, startParams));
                    obj.scaleX = values[1].AsFloat();
                    obj.scaleY = values[1].AsFloat();
                    //obj.AddSbCode(" S,0," + time + "," + time + ",," + values[1].ToString());
                }
                else if (values.Count == 4)
                {
                    CheckParameters("scale", values, new String[] { "int", "int", "number", "number" });
                    CheckRange("startScalefactor", values[2], 0);
                    CheckRange("endScalefactor", values[3], 0);
                    int stime = offset + values[0].AsInteger();
                    int etime = offset + values[1].AsInteger();
                    double[] startParams = new double[] { values[2].AsFloat() };
                    double[] endParams = new double[] { values[3].AsFloat() };
                    obj.scaleCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.scaleX = values[3].AsFloat();
                    obj.scaleY = values[3].AsFloat();
                    //obj.AddSbCode(" S,0," + stime + "," + etime + "," + values[2].ToString() + "," + values[3].ToString());
                }
                else if (values.Count == 5)
                {
                    CheckParameters("scale", values, new String[] { "int", "int", "int", "number", "number" });
                    CheckRange("easing", values[0], 0, 2);
                    CheckRange("startScalefactor", values[3], 0);
                    CheckRange("endScalefactor", values[4], 0);
                    int easing = values[0].AsInteger();
                    int stime = offset + values[1].AsInteger();
                    int etime = offset + values[2].AsInteger();
                    double[] startParams = new double[] { values[3].AsFloat() };
                    double[] endParams = new double[] { values[4].AsFloat() };
                    obj.scaleCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.scaleX = values[4].AsFloat();
                    obj.scaleY = values[4].AsFloat();
                    //obj.AddSbCode(" S," + values[0].ToString() + "," + stime + "," + etime + "," + values[3].ToString() + "," + values[4].ToString());
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }
            }
            else if (method.Equals("rotate"))
            {
                if (values.Count == 1)
                {
                    CheckParameters("rotate", values, new String[] { "number" });
                    double[] startParams = new double[] { values[0].AsFloat() };
                    obj.rotateCmds.Add(new SbCommand(0, offset, offset, startParams, startParams));
                    obj.rotate = values[0].AsFloat();
                    //obj.AddSbCode(" R,0," + offset + "," + offset + ",," + values[0].ToString());
                }
                else if (values.Count == 2)
                {
                    CheckParameters("rotate", values, new String[] { "int", "number" });
                    int time = offset + values[0].AsInteger();
                    double[] startParams = new double[] { values[1].AsFloat() };
                    obj.rotateCmds.Add(new SbCommand(0, time, offset, startParams, startParams));
                    obj.rotate = values[1].AsFloat();
                    //obj.AddSbCode(" R,0," + time + "," + time + ",," + values[1].ToString());
                }
                else if (values.Count == 4)
                {
                    CheckParameters("rotate", values, new String[] { "int", "int", "number", "number" });
                    int stime = offset + values[0].AsInteger();
                    int etime = offset + values[1].AsInteger();
                    double[] startParams = new double[] { values[2].AsFloat() };
                    double[] endParams = new double[] { values[3].AsFloat() };
                    obj.rotateCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.rotate = values[3].AsFloat();
                    //obj.AddSbCode(" R,0," + stime + "," + etime + "," + values[2].ToString() + "," + values[3].ToString());
                }
                else if (values.Count == 5)
                {
                    CheckParameters("rotate", values, new String[] { "int", "int", "int", "number", "number" });
                    CheckRange("easing", values[0], 0, 2);
                    int easing = values[0].AsInteger();
                    int stime = offset + values[1].AsInteger();
                    int etime = offset + values[2].AsInteger();
                    double[] startParams = new double[] { values[3].AsFloat() };
                    double[] endParams = new double[] { values[4].AsFloat() };
                    obj.rotateCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.rotate = values[4].AsFloat();
                    //obj.AddSbCode(" R," + values[0].ToString() + "," + stime + "," + etime + "," + values[3].ToString() + "," + values[4].ToString());
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }
            }
            else if (method.Equals("moveX"))
            {
                if (values.Count == 1)
                {
                    CheckParameters("moveX", values, new String[] { "int" });
                    double[] startParams = new double[] { values[0].AsInteger() };
                    obj.moveXCmds.Add(new SbCommand(0, offset, offset, startParams, startParams));
                    obj.x = values[0].AsInteger();
                    //obj.AddSbCode(" MX,0," + offset + "," + offset + ",," + values[0].ToString());
                }
                else if (values.Count == 2)
                {
                    CheckParameters("moveX", values, new String[] { "int", "int" });
                    int time = offset + values[0].AsInteger();
                    double[] startParams = new double[] { values[1].AsInteger() };
                    obj.moveXCmds.Add(new SbCommand(0, time, offset, startParams, startParams));
                    obj.x = values[1].AsInteger();
                    //obj.AddSbCode(" MX,0," + time + "," + time + ",," + values[1].ToString());
                }
                else if (values.Count == 4)
                {
                    CheckParameters("moveX", values, new String[] { "int", "int", "int", "int" });
                    int stime = offset + values[0].AsInteger();
                    int etime = offset + values[1].AsInteger();
                    double[] startParams = new double[] { values[2].AsInteger() };
                    double[] endParams = new double[] { values[3].AsInteger() };
                    obj.moveXCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.x = values[3].AsInteger();
                    //obj.AddSbCode(" MX,0," + stime + "," + etime + "," + values[2].ToString() + "," + values[3].ToString());
                }
                else if (values.Count == 5)
                {
                    CheckParameters("moveX", values, new String[] { "int", "int", "int", "int", "int" });
                    CheckRange("easing", values[0], 0, 2);
                    int easing = values[0].AsInteger();
                    int stime = offset + values[1].AsInteger();
                    int etime = offset + values[2].AsInteger();
                    double[] startParams = new double[] { values[3].AsInteger() };
                    double[] endParams = new double[] { values[4].AsInteger() };
                    obj.moveXCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.x = values[4].AsInteger();
                    //obj.AddSbCode(" MX," + values[0].ToString() + "," + stime + "," + etime + "," + values[3].ToString() + "," + values[4].ToString());
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }
            }
            else if (method.Equals("moveY"))
            {
                if (values.Count == 1)
                {
                    CheckParameters("moveY", values, new String[] { "int" });
                    double[] startParams = new double[] { values[0].AsInteger() };
                    obj.moveYCmds.Add(new SbCommand(0, offset, offset, startParams, startParams));
                    obj.y = values[0].AsInteger();
                    //obj.AddSbCode(" MY,0," + offset + "," + offset + ",," + values[0].ToString());
                }
                else if (values.Count == 2)
                {
                    CheckParameters("moveY", values, new String[] { "int", "int" });
                    int time = offset + values[0].AsInteger();
                    double[] startParams = new double[] { values[1].AsInteger() };
                    obj.moveYCmds.Add(new SbCommand(0, time, offset, startParams, startParams));
                    obj.y = values[1].AsInteger();
                    //obj.AddSbCode(" MY,0," + time + "," + time + ",," + values[1].ToString());
                }
                else if (values.Count == 4)
                {
                    CheckParameters("moveY", values, new String[] { "int", "int", "int", "int" });
                    int stime = offset + values[0].AsInteger();
                    int etime = offset + values[1].AsInteger();
                    double[] startParams = new double[] { values[2].AsInteger() };
                    double[] endParams = new double[] { values[3].AsInteger() };
                    obj.moveYCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.y = values[3].AsInteger();
                    //obj.AddSbCode(" MY,0," + stime + "," + etime + "," + values[2].ToString() + "," + values[3].ToString());
                }
                else if (values.Count == 5)
                {
                    CheckParameters("moveY", values, new String[] { "int", "int", "int", "int", "int" });
                    CheckRange("easing", values[0], 0, 2);
                    int easing = values[0].AsInteger();
                    int stime = offset + values[1].AsInteger();
                    int etime = offset + values[2].AsInteger();
                    double[] startParams = new double[] { values[3].AsInteger() };
                    double[] endParams = new double[] { values[4].AsInteger() };
                    obj.moveYCmds.Add(new SbCommand(0, stime, etime, startParams, endParams));
                    obj.y = values[4].AsInteger();
                    //obj.AddSbCode(" MY," + values[0].ToString() + "," + stime + "," + etime + "," + values[3].ToString() + "," + values[4].ToString());
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }
            }
            else if (method.Equals("scaleVec"))
            {
                if (values.Count == 2)
                {
                    CheckParameters("scaleVec", values, new String[] { "number", "number" });
                    CheckRange("scalefactorX", values[0], 0);
                    CheckRange("scalefactorY", values[1], 0);
                    double[] startParams = new double[] { values[0].AsFloat(), values[1].AsFloat() };
                    obj.scaleVecCmds.Add(new SbCommand(0, offset, offset, startParams, startParams));
                    obj.scaleX = values[0].AsFloat();
                    obj.scaleY = values[1].AsFloat();
                    //obj.AddSbCode(" V,0," + offset + "," + offset + ",," + values[0].ToString() + "," + values[1].ToString());
                }
                else if (values.Count == 3)
                {
                    CheckParameters("scaleVec", values, new String[] { "int", "number", "number" });
                    CheckRange("scalefactorX", values[1], 0);
                    CheckRange("scalefactorY", values[2], 0);
                    int time = offset + values[0].AsInteger();
                    double[] startParams = new double[] { values[1].AsFloat(), values[2].AsFloat() };
                    obj.scaleVecCmds.Add(new SbCommand(0, time, time, startParams, startParams));
                    obj.scaleX = values[1].AsFloat();
                    obj.scaleY = values[2].AsFloat();
                    //obj.AddSbCode(" V,0," + time + "," + time + ",," + values[1].ToString() + "," + values[2].ToString());
                }
                else if (values.Count == 6)
                {
                    CheckParameters("scaleVec", values, new String[] { "int", "int", "number", "number", "number", "number" });
                    CheckRange("startScalefactorX", values[2], 0);
                    CheckRange("startScalefactorY", values[3], 0);
                    CheckRange("endScalefactorX", values[4], 0);
                    CheckRange("endscalefactorY", values[5], 0);
                    int stime = offset + values[0].AsInteger();
                    int etime = offset + values[1].AsInteger();
                    double[] startParams = new double[] { values[2].AsFloat(), values[3].AsFloat() };
                    double[] endParams = new double[] { values[4].AsFloat(), values[5].AsFloat() };
                    obj.scaleVecCmds.Add(new SbCommand(0, stime, etime, startParams, startParams));
                    obj.scaleX = values[4].AsFloat();
                    obj.scaleY = values[5].AsFloat();
                    //obj.AddSbCode(" V,0," + stime + "," + etime + "," + values[2].ToString() + "," + values[3].ToString() + "," + values[4].ToString() + "," + values[5].ToString());
                }
                else if (values.Count == 7)
                {
                    CheckParameters("scaleVec", values, new String[] { "int", "int", "int", "number", "number", "number", "number" });
                    CheckRange("easing", values[0], 0, 2);
                    CheckRange("startScalefactorX", values[3], 0);
                    CheckRange("startScalefactorY", values[4], 0);
                    CheckRange("endScalefactorX", values[5], 0);
                    CheckRange("endscalefactorY", values[6], 0);
                    int easing = values[0].AsInteger();
                    int stime = offset + values[1].AsInteger();
                    int etime = offset + values[2].AsInteger();
                    double[] startParams = new double[] { values[3].AsFloat(), values[4].AsFloat() };
                    double[] endParams = new double[] { values[5].AsFloat(), values[6].AsFloat() };
                    obj.scaleVecCmds.Add(new SbCommand(easing, stime, etime, startParams, startParams));
                    obj.scaleX = values[5].AsFloat();
                    obj.scaleY = values[6].AsFloat();
                    //obj.AddSbCode(" V," + values[0].ToString() + "," + stime + "," + etime + "," + values[3].ToString() + "," + values[4].ToString() + "," + values[5].ToString() + "," + values[6].ToString());
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }
            }
            else if (method.Equals("color"))
            {
                if (values.Count == 3)
                {
                    CheckParameters("color", values, new String[] { "int", "int", "int" });
                    CheckRange("colorRed", values[0], 0, 255);
                    CheckRange("colorGreen", values[1], 0, 255);
                    CheckRange("colorBlue", values[2], 0, 255);
                    double[] startParams = new double[] { values[0].AsInteger(), values[1].AsInteger(), values[2].AsInteger() };
                    obj.colorCmds.Add(new SbCommand(0, offset, offset, startParams, startParams));
                    obj.red = values[0].AsInteger();
                    obj.green = values[1].AsInteger();
                    obj.blue = values[2].AsInteger();
                    //obj.AddSbCode(" C,0," + offset + "," + offset + ",," + values[0].ToString() + "," + values[1].ToString() + "," + values[2].ToString());
                }
                else if (values.Count == 4)
                {
                    CheckParameters("color", values, new String[] { "int", "number", "number", "number" });
                    CheckRange("colorRed", values[1], 0, 255);
                    CheckRange("colorGreen", values[2], 0, 255);
                    CheckRange("colorBlue", values[3], 0, 255);
                    int time = offset + values[0].AsInteger();
                    double[] startParams = new double[] { values[1].AsInteger(), values[2].AsInteger(), values[3].AsInteger() };
                    obj.colorCmds.Add(new SbCommand(0, time, time, startParams, startParams));
                    obj.red = values[1].AsInteger();
                    obj.green = values[2].AsInteger();
                    obj.blue = values[3].AsInteger();
                    //obj.AddSbCode(" C,0," + time + "," + time + ",," + values[1].ToString() + "," + values[2].ToString() + "," + values[3].ToString());
                }
                else if (values.Count == 8)
                {
                    CheckParameters("color", values, new String[] { "int", "int", "number", "number", "number", "number", "number", "number" });
                    CheckRange("startColorRed", values[2], 0, 255);
                    CheckRange("startColorGreen", values[3], 0, 255);
                    CheckRange("startColorBlue", values[4], 0, 255);
                    CheckRange("endColorRed", values[5], 0, 255);
                    CheckRange("endColorGreen", values[6], 0, 255);
                    CheckRange("endColorBlue", values[7], 0, 255);
                    int stime = offset + values[0].AsInteger();
                    int etime = offset + values[1].AsInteger();
                    double[] startParams = new double[] { values[2].AsInteger(), values[3].AsInteger(), values[4].AsInteger() };
                    double[] endParams = new double[] { values[5].AsInteger(), values[6].AsInteger(), values[7].AsInteger() };
                    obj.colorCmds.Add(new SbCommand(0, stime, etime, startParams, startParams));
                    obj.red = values[5].AsInteger();
                    obj.green = values[6].AsInteger();
                    obj.blue = values[7].AsInteger();
                    //obj.AddSbCode(" C,0," + stime + "," + etime + "," + values[2].ToString() + "," + values[3].ToString() + "," + values[4].ToString() + "," + values[5].ToString() + "," + values[6].ToString() + "," + values[7].ToString());
                }
                else if (values.Count == 9)
                {
                    CheckParameters("color", values, new String[] { "int", "int", "int", "number", "number", "number", "number", "number", "number" });
                    CheckRange("easing", values[0], 0, 2);
                    CheckRange("startColorRed", values[3], 0, 255);
                    CheckRange("startColorGreen", values[4], 0, 255);
                    CheckRange("startColorBlue", values[5], 0, 255);
                    CheckRange("endColorRed", values[6], 0, 255);
                    CheckRange("endColorGreen", values[7], 0, 255);
                    CheckRange("endColorBlue", values[8], 0, 255);
                    int easing = values[0].AsInteger();
                    int stime = offset + values[1].AsInteger();
                    int etime = offset + values[2].AsInteger();
                    double[] startParams = new double[] { values[3].AsInteger(), values[4].AsInteger(), values[5].AsInteger() };
                    double[] endParams = new double[] { values[6].AsInteger(), values[7].AsInteger(), values[8].AsInteger() };
                    obj.colorCmds.Add(new SbCommand(easing, stime, etime, startParams, startParams));
                    obj.red = values[6].AsInteger();
                    obj.green = values[7].AsInteger();
                    obj.blue = values[8].AsInteger();
                    //obj.AddSbCode(" C," + values[0].ToString() + "," + stime + "," + etime + "," + values[3].ToString() + "," + values[4].ToString() + "," + values[5].ToString() + "," + values[6].ToString() + "," + values[7].ToString() + "," + values[8].ToString());
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }

            }
            else if (method.Equals("getX"))
            {
                if (values.Count == 0)
                {
                    // Simply returns the last value of x for this object
                    return new SGLValue(obj.x);
                }
                else if (values.Count == 1)
                {
                    // Parameter representing a specific time, gonna look up/calc what x is at that time
                }
                else
                {
                    throw new SGLCompilerException(GetLine(), "method undefined", "The object method '" + method + "' is undefined for " + values.Count + " parameters.");
                }
            }
            else
            {
                throw new SGLCompilerException(GetLine(), "unknown method", "The object method '" + method + "' doesn't exist.");
            }



            return SGLValue.VOID;
        }


        private void CheckParameters(String ident, List<SGLValue> values, String[] expected) {
            int a = 0;
            foreach (SGLValue val in values) {
                if (!val.GetVarType().Equals(expected[a]))
                {
                    if (!expected[a].Equals("number") || (!val.GetVarType().Equals("int") && !val.GetVarType().Equals("float")))
                    {
                        throw new SGLCompilerException(GetLine(), "type mismatch", "the method '" + ident + "' is not applicable for the arguments (" + GetArgString(values) + ")");
                    }
                }

                a++;
            }
        }

        private void CheckRange(String ident, SGLValue value, int start)
        {
            if (value.AsFloat() < start)
            {
                throw new SGLCompilerException(GetLine(), "unexpected value", "the value for '" + ident + "' should not be lower than " + start);
            }
        }

        private void CheckRange(String ident, SGLValue value, int start, int end)
        {
            if (value.AsFloat() < start || value.AsFloat() > end)
            {
                throw new SGLCompilerException(GetLine(), "unexpected value", "the value for '" + ident + "' should be between " + start + " and " + end);
            }
        }

        private String GetArgString(List<SGLValue> values) {
            String argString = "";
            for (int i = 0; i < values.Count; i++)
            {
                argString += values[i].GetVarType();
                if (i < values.Count - 1)
                {
                    argString += ", ";
                }
            }
            return argString;
        }

        public int GetLine()
        {
            return line;
        }

    }
}
