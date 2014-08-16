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
using System.Text;

namespace SGL.Storyboard.Commands
{
    internal class Flag : Command
    {
        private readonly FlagType flagType;
        private readonly int endTime;

        /// <summary>
        /// Cunstructor for normal storyboard commands
        /// </summary>
        /// <param name="easing"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="startParams"></param>
        /// <param name="endParams"></param>
        public Flag(FlagType flagType, int startTime, int endTime) : base(startTime)
        {
            this.flagType = flagType;
            this.endTime = endTime;
        }

        public override void AddStoryboardCode(StringBuilder storyboardCode)
        {
			switch (flagType)
            {
				case FlagType.Additive:
                    storyboardCode.Append(" P,0," + startTime + "," + endTime + ",A");
                    break;
				case FlagType.FlipH:
                    storyboardCode.Append(" P,0," + startTime + "," + endTime + ",H");
                    break;
				case FlagType.FlipV:
                    storyboardCode.Append(" P,0," + startTime + "," + endTime + ",V");
                    break;
                default:
                    throw new Exception("Unimplemented flag type - Error should never occur");
            }
            storyboardCode.Append("\r\n");
        }


        public override String ToString()
        {
			return "SbFlag (" + flagType + "): " + startTime + "," + endTime;
        }
    }
}