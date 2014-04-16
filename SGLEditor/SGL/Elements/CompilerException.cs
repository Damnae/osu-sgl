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
using Antlr.Runtime;

namespace SGL.Elements {
	public class CompilerException : Exception {
		private readonly int errorCode;
		private readonly RecognitionException mte;
		private readonly string syntaxExceptionDetails = "";
		private String[] args;
		private int line = -1;

		public CompilerException(int line, int errorCode, params String[] args) {
			this.line = line;
			this.errorCode = errorCode;
			this.args = args;
		}

		public CompilerException(RecognitionException mte, String syntaxExceptionDetails) {
			this.mte = mte;
			this.syntaxExceptionDetails = syntaxExceptionDetails;
		}

		public int Line {
			get { return line; }
			set {
				// Add line information only if previous nodes didn't add this information
				if (line < 1) {
					line = value;
				}
			}
		}

		public int ErrorCode {
			get { return errorCode; }
		}

		private String GetErrorMessage(int errorCode, String[] args) {
			switch (errorCode) {
				// Antlr specific
				case 101:
					return "Wrong syntax: " + args[0];

				// Operators, variable assignments, type sanity checks...
				case 201:
					return "The operator " + args[0] + " is undefined for the argument type (" + args[1] + ").";
				case 202:
					return "The operator " + args[0] + " is undefined for the argument types (" + args[1] + ", " +
						   args[2] + ").";
				case 203:
					return "The operator " + args[0] + " is undefined for the argument types (" + args[1] + ", " +
						   args[2] + ", " + args[3] + ").";
				case 204:
					return "You can't compare a value of type " + args[0] + " with a value of type " + args[1] +
						   ", they should normally be from the same type";
				case 205:
					return "You can't divide by 0 unless you are Chuck Norris";
				case 206:
					return "The conditional operator (? :) expexted the first argument type to be Boolean but was " +
						   args[0];
				case 211:
					return "'" + args[0] +
						   "' cannot be resolved to a variable. Check if the variable name is spelled correctly";
				case 212:
					return "Indexes must have a numeric value, but the " + args[0] + ". index value of '" + args[1] +
						   "' is from type " + args[2];
				case 213:
					return "The variable '" + args[0] + "' at it's " + args[1] + ". sub-level is from type " + args[2] +
						   ", but is accessed as a list";
				case 214:
					return "Indexes must have a numeric value, but this one is from type " + args[0] + ".";
				case 215:
					return "A value from type " + args[0] + " is accessed as a list.";
				case 216:
					return "A value from type " + args[0] + " was expected, but was " + args[1] + ".";
				case 217:
					return "The " + args[0] + " value in a(n) " + args[1] + "-statement must be from type " + args[2] +
						   ", but was " + args[3] + ".";
				case 218:
					return "You have to define the variable '" + args[0] +
						   "' first before you can assign a value to it.";
				case 219:
					return "'" + args[0] + "' cannot be defined, because a variable with the same name already exists.";

				// Functions and classes
				case 301:
					return "The function '" + args[0] + "' doesn't exist. Please check for typos.";
				case 302:
					return "The function '" + args[0] +
						   "' was called, but the parameters given weren't expected by this function: (" + args[1] +
						   ").";
				case 303:
					return "The function '" + args[0] + "' already exists with that number of arguments (" + args[1] +
						   "), two functions with the same number of arguments would be ambiguous.";
				case 311:
					return "The class '" + args[0] + "' doesn't exist. Please check for typos.";
				case 312:
					return "The class '" + args[0] + "' has no constructor for the following parameter types: (" +
						   args[1] + ").";
				case 313:
					return "The method '" + args[1] + "' from the class '" + args[0] +
						   "' was called, but the parameters given weren't expected by this method: (" + args[2] + ").";
				case 314:
					return "The method '" + args[1] + "' doesn't exist for class '" + args[0] + "'.";
				case 315:
					return "The value for easing must be between 0 and 2 but was " + args[0] + ".";
				case 316:
					return "Variables from type " + args[0] + " don't have any methods.";
				case 317:
					return "Error in function '" + args[0] + "': " + args[1];
				case 318:
					return "The function '" + args[0] + "' was called with an incorrect number of parameters: expected " + args[1] + ", got " + args[2] + ".";

				// other
				case 401:
					return "The resulting value is smaller than the smallest representable " + args[0] + " value.";
				case 402:
					return "The resulting value is bigger than the biggest representable " + args[0] + " value.";
				case 403:
					return "The resulting value is NaN, meaning that it's impossible to return a(n) " + args[0] +
						   " value.";

				// Should never occur
				default:
					return "Unknown exception - An error message for the error code '" + errorCode +
						   "' isn't specified.";
			}
		}

		public void SetArguments(params String[] args) {
			this.args = args;
		}

		public String GetExceptionAsString() {
			if (mte != null) {
				string errorText = "Syntax Error: ";
				errorText += "It seems that something is wrong or missing around '" + mte.Token.Text +
							 "'. Check for errors such as missing brackets/symbols, wrong written keywords and the like.";
				errorText += "\r\n\toccurred at line " + mte.Line + " at position " + mte.CharPositionInLine;
				errorText += "\r\nDetailed Parser Error Message: " + syntaxExceptionDetails;
				return errorText;
			}
			if (errorCode < 200) {
				return GetErrorMessage(errorCode, args);
			} else {
				String trace = GetStackTrace();
				if (trace != null) {
					//return "Error at line " + this.line + " (" + GlobalMemory.Instance.CurrentCall + "): " + GetErrorMessage(errorCode, args) + "\r\n" + trace;
					return "Compiler Error: " + GetErrorMessage(errorCode, args) + "\r\n\toccurred at line " + line +
						   " (" + GlobalMemory.Instance.CurrentCall + ")" + "\r\n" + trace;
				} else {
					return "Compiler Error: " + GetErrorMessage(errorCode, args) + "\r\n\toccurred at line " + line +
						   " (" + GlobalMemory.Instance.CurrentCall + ")";
				}
			}
		}


		/// <summary>
		/// Returns a String representation of the stacktrace or null.
		/// </summary>
		/// <returns></returns>
		private String GetStackTrace() {
			Stack<CallItem> callStack = GlobalMemory.Instance.CallStack;
			if (callStack.Count > 0) {
				String traceRoute = "";
				foreach (CallItem item in callStack) {
					traceRoute += "\tarising from line " + item.Line + " (" + item.Call + ")\r\n";
				}
				return traceRoute;
			} else {
				return null;
			}
		}
	}
}