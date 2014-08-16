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
using SGL.Library.Classes;
using SGL.Library.Functions;
using SGL.Library.Functions.Calculation;
using SGL.Library.Functions.IO;

namespace SGL.Elements
{
    /// <summary>
    /// Singleton containing classes which can be used in SGL
    /// </summary>
    internal class LibraryManager
    {
        private static readonly LibraryManager instance = new LibraryManager();
        private readonly Dictionary<string, AbstractClass> classes = new Dictionary<string, AbstractClass>();

        // memory for methods
        private readonly Dictionary<string, Function> functions = new Dictionary<string, Function>();

        private LibraryManager()
        {
            RegisterClasses();
            RegisterFunctions();
        }

        public static LibraryManager Instance
        {
            get { return instance; }
        }

        private void RegisterClasses()
        {
            RegisterClass("List", new ListClass());
            RegisterClass("Sprite", new SpriteClass());
            RegisterClass("Animation", new AnimationClass());
            RegisterClass("Layer", new LayerClass());
			RegisterClass("Sample", new SampleClass());
			RegisterClass("Mesh", new MeshClass());
        }

        private void RegisterFunctions()
        {
            RegisterFunction("exp", new ExpFunction());
			RegisterFunction("sin", new SinFunction());
			RegisterFunction("cos", new CosFunction());
            RegisterFunction("rand", new RandFunction());
            RegisterFunction("println", new PrintFunction());
        }

        // memory for classes

        //public Function GetFunction(string name, List<Value> parameters);

        /// <summary>
        /// Tests if a class is known by the Library and was registered before.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public bool IsClassKnown(String className)
        {
            if (classes.ContainsKey(className))
            {
                return true;
            }
            return false;
        }

        public bool IsFunctionKnown(String funcName)
        {
            if (functions.ContainsKey(funcName))
            {
                return true;
            }
            return false;
        }

        public void RegisterClass(string name, AbstractClass cls)
        {
            classes.Add(name, cls);
        }

        public void RegisterFunction(string name, Function func)
        {
            functions.Add(name, func);
        }


        /// <summary>
        /// Returns the class 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object GetClass(string name, List<Value> parameters)
        {
            // look if the class exists
            if (classes.ContainsKey(name))
            {
                try
                {
                    return classes[name].CreateInstance(parameters);
                }
                catch (CompilerException ce)
                {
                    if (ce.ErrorCode == 312)
                    {
                        ce.SetArguments(name, Value.PrintTypeList(parameters));
                    }
                    throw ce;
                }
            }
            else
            {
                throw new CompilerException(-1, 311, name, Value.PrintTypeList(parameters));
            }
        }


        public Function GetFunction(string name, List<Value> parameters)
        {
            // look if the function exists
            if (functions.ContainsKey(name))
            {
                try
                {
                    return functions[name];
                }
                catch (CompilerException ce)
                {
                    if (ce.ErrorCode == 312)
                    {
                        ce.SetArguments(name, Value.PrintTypeList(parameters));
                    }
                    throw ce;
                }
            }
            else
            {
                throw new CompilerException(-1, 311, name, Value.PrintTypeList(parameters));
            }
        }
    }
}