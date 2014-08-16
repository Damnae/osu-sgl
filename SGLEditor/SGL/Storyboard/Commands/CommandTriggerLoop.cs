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
using System.Collections.Generic;
using System.Text;

namespace SGL.Storyboard.Commands
{
    class CommandTriggerLoop : CommandLoop
    {
        private String triggerName;
        private int endTime;

        public CommandTriggerLoop(String triggerName, int startTime, int endTime) : base(startTime)
        {
            this.triggerName = triggerName;
            this.endTime = endTime;

			if (endTime < startTime)
				throw new CompilerException(-1, 404, "trigger", startTime.ToString(), endTime.ToString());
        }

        public override void AddStoryboardCode(StringBuilder storyboardCode)
        {
            storyboardCode.Append(" T," + triggerName + "," + startTime + "," + endTime + "\r\n");
            foreach (Animation currentAnimation in nestedAnimations)
            {
                storyboardCode.Append(" ");
                currentAnimation.AddStoryboardCode(storyboardCode);
            }
        }
    }
}
