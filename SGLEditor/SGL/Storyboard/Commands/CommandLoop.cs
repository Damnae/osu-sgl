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
    public class CommandLoop : Command
    {

        private int loopCount;
        protected List<Animation> nestedAnimations = new List<Animation>();

        protected CommandLoop(int startTime) : base(startTime) { }

        public CommandLoop(int startTime, int loopCount) : base(startTime)
        {
            this.loopCount = loopCount;
        }

        public void AddAnimation(Animation animation)
        {
            this.nestedAnimations.Add(animation);
        }

        public override void AddSoryboardCode(StringBuilder storyboardCode)
        {
            storyboardCode.Append(" L," + startTime + "," + loopCount + "\r\n");
            foreach (Animation currentAnimation in nestedAnimations)
            {
                storyboardCode.Append(" ");
                currentAnimation.AddSoryboardCode(storyboardCode);
            }
        }
    }
}
