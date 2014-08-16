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
using System.Text;
using SGL.Elements;
using SGL.Storyboard.Commands;
using SGL.Util;

namespace SGL.Storyboard.Generators.Visual {
	public abstract class AbstractVisualGenerator : AbstractGenerator, IComparable {
		protected string filepath;
		protected string layer;
		protected string origin;
		protected int priority = 0;
		protected double x = 320;
		protected double y = 240;
		protected double scaleFactor = 1;
		protected double opacity = 1;
		protected double rotation = 0;
		protected int red = 255;
		protected int green = 255;
		protected int blue = 255;
		protected bool insideLoop = false;
		protected CommandLoop currentLoop;

		private readonly List<Command> storyboardCommands = new List<Command>();

		public AbstractVisualGenerator() {
		}

		public AbstractVisualGenerator(string layer, string origin, string filepath) {
			this.layer = layer;
			this.origin = origin;
			this.filepath = filepath;
		}

		#region Properties

		public string Filepath {
			get {
				return filepath;
			}
		}

		public string Origin {
			get {
				return origin;
			}
		}

		public string Layer {
			get {
				return layer;
			}
		}

		public double X {
			get {
				return x;
			}
		}

		public double Y {
			get {
				return y;
			}
		}

		public double Scale {
			get {
				return scaleFactor;
			}
		}

		public double Opacity {
			get {
				return opacity;
			}
		}

		public double Rotation {
			get {
				return rotation;
			}
		}

		public double Red {
			get {
				return red;
			}
		}

		public double Green {
			get {
				return green;
			}
		}

		public double Blue {
			get {
				return blue;
			}
		}

		public int Priority {
			get { return priority; }
		}

		#endregion

		#region Mehtods for general storyboard generation

		public override void GenerateBackgroundSection(StringBuilder storyboardCode) {
			if (layer.Equals("Background")) {
				GenerateStoryboardCode(storyboardCode);
			}
		}

		public override void GenerateFailSection(StringBuilder storyboardCode) {
			if (layer.Equals("Fail")) {
				GenerateStoryboardCode(storyboardCode);
			}
		}

		public override void GeneratePassSection(StringBuilder storyboardCode) {
			if (layer.Equals("Pass")) {
				GenerateStoryboardCode(storyboardCode);
			}
		}

		public override void GenerateForegroundSection(StringBuilder storyboardCode) {
			if (layer.Equals("Foreground")) {
				GenerateStoryboardCode(storyboardCode);
			}
		}

		/// <summary>
		/// Generates the storyboard code for one VisualObject
		/// </summary>
		/// <param name="storyboardCode"></param>
		protected virtual void GenerateStoryboardCode(StringBuilder storyboardCode) {
			// Start Sprite with this code
			String initCode = GetStoryboardInitCode();
			if (initCode != null) {
				storyboardCode.AppendLine(initCode);
			}

			foreach (Command command in storyboardCommands) {
				command.AddStoryboardCode(storyboardCode);
			}
		}

		#endregion

		protected abstract String GetStoryboardInitCode();

		#region IComparable Members

		public int CompareTo(object obj) {
			if (obj == null)
				return 1;

			// sort order: 1. layer, 2. priority
			var that = obj as AbstractVisualGenerator;
			if (that != null) {
				int layerCompare = LayerToInt().CompareTo(that.LayerToInt());
				if (layerCompare != 0)
					return layerCompare;
				else {
					return Priority.CompareTo(that.Priority);
				}
			} else
				throw new ArgumentException("Object is not a VisualObject");
		}

		public int LayerToInt() {
			switch (layer) {
				case "Background":
					return 0;
				case "Fail":
					return 1;
				case "Pass":
					return 2;
				case "Foreground":
					return 3;
				default:
					throw new Exception("unknown layer type");
			}
		}

		#endregion

		protected void AddCommand(Command command) {
			if (!insideLoop) {
				storyboardCommands.Add(command);
			} else {
				if (command is Animation) {
					currentLoop.AddAnimation((Animation)command);
				} else {
					throw new Exception("tried to add a loop inside a loop but should never occur");
				}
			}
		}

		#region Methods for generating code for storyboard commands

		#region move

		public virtual void move(int easing, int startTime, int endTime, double startX, double startY, double endX, double endY) {
			if (easing < 0 || easing > 2)
				throw new CompilerException(-1, 315, easing.ToString());
			var startParams = new double[] { startX, startY };
			var endParams = new double[] { endX, endY };
			AddCommand(new Animation(AnimationType.Move, easing, startTime, endTime, startParams, endParams));
			this.x = endX;
			this.y = endY;
		}

		public void move(int startTime, int endTime, double startX, double startY, double endX, double endY) {
			move(0, startTime, endTime, startX, startY, endX, endY);
		}

		public void move(int startTime, double startX, double startY) {
			move(0, startTime, startTime, startX, startY, startX, startY);
		}

		public void move(int startX, double startY) {
			move(0, 0, 0, startX, startY, startX, startY);
		}

		#endregion

		#region fade

		public virtual void fade(int easing, int startTime, int endTime, double startOpacity, double endOpacity) {
			if (easing < 0 || easing > 2)
				throw new CompilerException(-1, 315, easing.ToString());
			var startParams = new[] { startOpacity };
			var endParams = new[] { endOpacity };
			AddCommand(new Animation(AnimationType.Fade, easing, startTime, endTime, startParams, endParams));
			opacity = endOpacity;
		}

		public void fade(int startTime, int endTime, double startOpacity, double endOpacity) {
			fade(0, startTime, endTime, startOpacity, endOpacity);
		}

		public void fade(int startTime, double startOpacity) {
			fade(0, startTime, startTime, startOpacity, startOpacity);
		}

		public void fade(double startOpacity) {
			fade(0, 0, 0, startOpacity, startOpacity);
		}

		#endregion

		#region scale

		public virtual void scale(int easing, int startTime, int endTime, double startScale, double endScale) {
			if (easing < 0 || easing > 2)
				throw new CompilerException(-1, 315, easing.ToString());
			var startParams = new[] { startScale };
			var endParams = new[] { endScale };
			AddCommand(new Animation(AnimationType.Scale, easing, startTime, endTime, startParams, endParams));
			this.scaleFactor = endScale;
		}

		public void scale(int startTime, int endTime, double startScale, double endScale) {
			scale(0, startTime, endTime, startScale, endScale);
		}

		public void scale(int startTime, double startScale) {
			scale(0, startTime, startTime, startScale, startScale);
		}

		public void scale(double startScale) {
			scale(0, 0, 0, startScale, startScale);
		}

		#endregion

		#region vector scaling

		public virtual void scaleVec(int easing, int startTime, int endTime, double startX, double startY, double endX,
							 double endY) {
			if (easing < 0 || easing > 2)
				throw new CompilerException(-1, 315, easing.ToString());
			var startParams = new[] { startX, startY };
			var endParams = new[] { endX, endY };
			AddCommand(new Animation(AnimationType.ScaleVec, easing, startTime, endTime, startParams, endParams));
			this.scaleFactor = (endX + endY) / 2;
		}

		public void scaleVec(int startTime, int endTime, double startX, double startY, double endX, double endY) {
			scaleVec(0, startTime, endTime, startX, startY, endX, endY);
		}

		public void scaleVec(int startTime, double startX, double startY) {
			scaleVec(0, startTime, startTime, startX, startY, startX, startY);
		}

		public void scaleVec(double startX, double startY) {
			scaleVec(0, 0, 0, startX, startY, startX, startY);
		}

		#endregion

		#region rotate

		public virtual void rotate(int easing, int startTime, int endTime, double startAngle, double endAngle) {
			if (easing < 0 || easing > 2)
				throw new CompilerException(-1, 315, easing.ToString());
			var startParams = new[] { startAngle };
			var endParams = new[] { endAngle };
			AddCommand(new Animation(AnimationType.Rotate, easing, startTime, endTime, startParams, endParams));
			rotation = endAngle;
		}

		public void rotate(int startTime, int endTime, double startAngle, double endAngle) {
			rotate(0, startTime, endTime, startAngle, endAngle);
		}

		public void rotate(int startTime, double startAngle) {
			rotate(0, startTime, startTime, startAngle, startAngle);
		}

		public void rotate(double startAngle) {
			rotate(0, 0, 0, startAngle, startAngle);
		}

		#endregion

		#region color

		public virtual void color(int easing, int startTime, int endTime, int startRed, int startGreen, int startBlue,
						  int endRed, int endGreen, int endBlue) {
			if (easing < 0 || easing > 2)
				throw new CompilerException(-1, 315, easing.ToString());
			var startParams = new double[] { startRed, startGreen, startBlue };
			var endParams = new double[] { endRed, endGreen, endBlue };
			AddCommand(new Animation(AnimationType.Color, easing, startTime, endTime, startParams, endParams));
			red = endRed;
			green = endGreen;
			blue = endBlue;
		}

		public void color(int startTime, int endTime, int startRed, int startGreen, int startBlue, int endRed,
						  int endGreen, int endBlue) {
			color(0, startTime, endTime, startRed, startGreen, startBlue, endRed, endGreen, endBlue);
		}

		public void color(int startTime, int startRed, int startGreen, int startBlue) {
			color(0, startTime, startTime, startRed, startGreen, startBlue, startRed, startGreen, startBlue);
		}

		public void color(int startRed, int startGreen, int startBlue) {
			color(0, 0, 0, startRed, startGreen, startBlue, startRed, startGreen, startBlue);
		}

		#endregion

		#region colorHsb

		public virtual void colorHsb(int easing, int startTime, int endTime, double startHue, double startSaturation, double startBrightness, double endHue, double endSaturation, double endBrightness) {
			int startRed, startGreen, startBlue, endRed, endGreen, endBlue;
			ColorUtil.HsbToRgb(startHue, startSaturation, startBrightness, out startRed, out startGreen, out startBlue);
			ColorUtil.HsbToRgb(endHue, endSaturation, endBrightness, out endRed, out endGreen, out endBlue);
			color(easing, startTime, endTime, startRed, startGreen, startBlue,
						   endRed, endGreen, endBlue);
		}

		public void colorHsb(int startTime, int endTime, double startHue, double startSaturation, double startBrightness, double endHue, double endSaturation, double endBrightness) {
			colorHsb(0, startTime, endTime, startHue, startSaturation, startBrightness, endHue, endSaturation, endBrightness);
		}

		public void colorHsb(int startTime, double startHue, double startSaturation, double startBrightness) {
			colorHsb(0, startTime, startTime, startHue, startSaturation, startBrightness, startHue, startSaturation, startBrightness);
		}

		public void colorHsb(double startHue, double startSaturation, double startBrightness) {
			colorHsb(0, 0, 0, startHue, startSaturation, startBrightness, startHue, startSaturation, startBrightness);
		}

		#endregion

		#region moveX

		public virtual void moveX(int easing, int startTime, int endTime, double startX, double endX) {
			if (easing < 0 || easing > 2)
				throw new CompilerException(-1, 315, easing.ToString());
			var startParams = new[] { startX };
			var endParams = new[] { endX };
			AddCommand(new Animation(AnimationType.MoveX, easing, startTime, endTime, startParams, endParams));
			this.x = (int)endX;
		}

		public void moveX(int startTime, int endTime, double startX, double endX) {
			moveX(0, startTime, endTime, startX, endX);
		}

		public void moveX(int startTime, double startX) {
			moveX(0, startTime, startTime, startX, startX);
		}

		public void moveX(double startX) {
			moveX(0, 0, 0, startX, startX);
		}

		#endregion

		#region moveY

		public virtual void moveY(int easing, int startTime, int endTime, double startY, double endY) {
			if (easing < 0 || easing > 2)
				throw new CompilerException(-1, 315, easing.ToString());
			var startParams = new[] { startY };
			var endParams = new[] { endY };
			AddCommand(new Animation(AnimationType.MoveY, easing, startTime, endTime, startParams, endParams));
			this.y = (int)endY;
		}

		public void moveY(int startTime, int endTime, double startY, double endY) {
			moveY(0, startTime, endTime, startY, endY);
		}

		public void moveY(int startTime, double startY) {
			moveY(0, startTime, startTime, startY, startY);
		}

		public void moveY(double startY) {
			moveY(0, 0, 0, startY, startY);
		}

		#endregion

		#region additive

		public virtual void additive(int startTime, int endTime) {
			AddCommand(new Flag(FlagType.Additive, startTime, endTime));
		}

		#endregion

		#region flipX

		public virtual void flipH(int startTime, int endTime) {
			AddCommand(new Flag(FlagType.FlipH, startTime, endTime));
		}

		#endregion

		#region flipY

		public virtual void flipV(int startTime, int endTime) {
			AddCommand(new Flag(FlagType.FlipV, startTime, endTime));
		}

		#endregion

		#endregion

		#region Methods for Looping

		public virtual void startLoop(int startTime, int loopCount) {
			this.currentLoop = new CommandLoop(startTime, loopCount);
			AddCommand(currentLoop);
			this.insideLoop = true;
		}

		public virtual void startTriggerLoop(string loopTrigger, int startTime, int endTime) {
			this.currentLoop = new CommandTriggerLoop(loopTrigger, startTime, endTime);
			AddCommand(currentLoop);
			this.insideLoop = true;
		}

		public void endLoop() {
			this.currentLoop = null;
			this.insideLoop = false;
		}

		#endregion



	}
}