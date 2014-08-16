using SGL.Elements;
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
using System.Globalization;
using System.Text;

namespace SGL.Storyboard.Commands
{
	public class Animation : Command
    {
        private readonly AnimationType animationType;
        private readonly int easing;
        private readonly double[] endParams;
        private readonly int endTime;
        private readonly double[] startParams;

        /// <summary>
        /// Cunstructor for normal storyboard commands
        /// </summary>
        /// <param name="easing"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="startParams"></param>
        /// <param name="endParams"></param>
        public Animation(AnimationType animationType, int easing, int startTime, int endTime, double[] startParams,
                         double[] endParams) : base(startTime)
        {
            this.animationType = animationType;
            this.easing = easing;
            this.endTime = endTime;
            this.startParams = startParams;
			this.endParams = endParams;

			if (endTime < startTime)
				throw new CompilerException(-1, 404, animationType.ToString(), startTime.ToString(), endTime.ToString());
        }

        public Animation(AnimationType animationType, int startTime, int endTime, double[] startParams,
                         double[] endParams)
            : this(animationType, 0, startTime, endTime, startParams, endParams)
        {
        }

        public Animation(AnimationType animationType, int startTime, double[] startParams)
            : this(animationType, 0, startTime, startTime, startParams, startParams)
        {
        }

        public Animation(AnimationType animationType, double[] startParams)
            : this(animationType, 0, 0, 0, startParams, startParams)
        {
        }

        public override void AddStoryboardCode(StringBuilder storyboardCode)
        {
            switch (animationType)
            {
                case AnimationType.Move:
                    storyboardCode.Append(" M," + easing + "," + startTime + "," + endTime + "," +
                                          ((int) startParams[0]).ToString(CultureInfo.InvariantCulture) + "," +
                                          ((int) startParams[1]).ToString(CultureInfo.InvariantCulture) + "," +
                                          ((int) endParams[0]).ToString(CultureInfo.InvariantCulture) + "," +
                                          ((int) endParams[1]).ToString(CultureInfo.InvariantCulture));
                    break;
                case AnimationType.MoveX:
                    storyboardCode.Append(" MX," + easing + "," + startTime + "," + endTime + "," +
                                          ((int) startParams[0]).ToString(CultureInfo.InvariantCulture) + "," +
                                          ((int) endParams[0]).ToString(CultureInfo.InvariantCulture));
                    break;
                case AnimationType.MoveY:
                    storyboardCode.Append(" MY," + easing + "," + startTime + "," + endTime + "," +
                                          ((int) startParams[0]).ToString(CultureInfo.InvariantCulture) + "," +
                                          ((int) endParams[0]).ToString(CultureInfo.InvariantCulture));
                    break;
                case AnimationType.Fade:
                    storyboardCode.Append(" F," + easing + "," + startTime + "," + endTime + "," +
                                          startParams[0].ToString(CultureInfo.InvariantCulture) + "," +
                                          endParams[0].ToString(CultureInfo.InvariantCulture));
                    break;
                case AnimationType.Scale:
                    storyboardCode.Append(" S," + easing + "," + startTime + "," + endTime + "," +
                                          startParams[0].ToString(CultureInfo.InvariantCulture) + "," +
                                          endParams[0].ToString(CultureInfo.InvariantCulture));
                    break;
                case AnimationType.ScaleVec:
                    storyboardCode.Append(" V," + easing + "," + startTime + "," + endTime + "," +
                                          startParams[0].ToString(CultureInfo.InvariantCulture) + "," +
                                          startParams[1].ToString(CultureInfo.InvariantCulture) + "," +
                                          endParams[0].ToString(CultureInfo.InvariantCulture) + "," +
                                          endParams[1].ToString(CultureInfo.InvariantCulture));
                    break;
                case AnimationType.Rotate:
                    storyboardCode.Append(" R," + easing + "," + startTime + "," + endTime + "," +
                                          startParams[0].ToString(CultureInfo.InvariantCulture) + "," +
                                          endParams[0].ToString(CultureInfo.InvariantCulture));
                    break;
                case AnimationType.Color:
                    storyboardCode.Append(" C," + easing + "," + startTime + "," + endTime + "," +
                                          startParams[0].ToString(CultureInfo.InvariantCulture) + "," +
                                          startParams[1].ToString(CultureInfo.InvariantCulture) + "," +
                                          startParams[2].ToString(CultureInfo.InvariantCulture) + "," +
                                          endParams[0].ToString(CultureInfo.InvariantCulture) + "," +
                                          endParams[1].ToString(CultureInfo.InvariantCulture) + "," +
                                          endParams[2].ToString(CultureInfo.InvariantCulture));
                    break;
                default:
                    throw new Exception("Unimplemented animation type - Error should never occur");
            }
            storyboardCode.Append("\r\n");
        }


        public override String ToString()
        {
            return "SbAnimation (" + animationType + "): " + easing + "," + startTime + "," + endTime + "," +
                   startParams + "," + endParams;
        }
    }
}