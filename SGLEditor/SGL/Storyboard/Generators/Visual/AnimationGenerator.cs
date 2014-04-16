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

namespace SGL.Storyboard.Generators.Visual
{
    public class AnimationGenerator : AbstractVisualGenerator
    {
        private readonly int frameCount;
        private readonly int frameDelay;
        private readonly string loopType;

        public AnimationGenerator(string layer, string origin, string filepath, int frameCount, int frameDelay,
                               string loopType)
            : base(layer, origin, filepath)
        {
            this.frameCount = frameCount;
            this.frameDelay = frameDelay;
            this.loopType = loopType;
        }


        protected override String GetStoryboardInitCode()
        {
            return "Animation," + layer + "," + origin + ",\"" + filepath + "\",320,240," + frameCount + "," +
                   frameDelay + "," + loopType;
        }
    }
}