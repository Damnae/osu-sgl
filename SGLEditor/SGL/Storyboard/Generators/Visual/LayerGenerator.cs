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
using SGL.Library.Classes;
using System;
using System.Collections.Generic;

namespace SGL.Storyboard.Generators.Visual
{
    internal class LayerGenerator : AbstractVisualGenerator
    {
        private List<AbstractVisualGenerator> LayerGenerators;

        public LayerGenerator(List<AbstractVisualClass> layerClasses)
        {
            List<AbstractVisualGenerator> LayerGeneratorsTemp = new List<AbstractVisualGenerator>();
            foreach (AbstractVisualClass obj in layerClasses)
            {
                LayerGeneratorsTemp.Add(obj.Object);
            }
            this.LayerGenerators = LayerGeneratorsTemp;
        }

        protected override void GenerateStoryboardCode(System.Text.StringBuilder storyboardCode)
        {
        }

        protected override String GetStoryboardInitCode()
        {
            return null;
        }

		public override void move(int easing, int startTime, int endTime, double startX, double startY, double endX, double endY)
        {
            foreach (AbstractVisualGenerator obj in LayerGenerators)
            {
                obj.move(easing, startTime, endTime, obj.X + startX, obj.Y + startY, obj.X + endX, obj.Y + endY);
            }
        }


        public override void scale(int easing, int startTime, int endTime, double startScale, double endScale)
        {
            foreach (AbstractVisualGenerator obj in LayerGenerators)
            {
                obj.scale(easing, startTime, endTime, obj.Scale * startScale, obj.Scale * endScale);
            }
        }

        public override void fade(int easing, int startTime, int endTime, double startOpacity, double endOpacity)
        {
            foreach (AbstractVisualGenerator obj in LayerGenerators)
            {
                obj.scale(easing, startTime, endTime, obj.Opacity * startOpacity, obj.Opacity * endOpacity);
            }
        }

        public override void color(int easing, int startTime, int endTime, int startRed, int startGreen, int startBlue,
                          int endRed, int endGreen, int endBlue)
        {
            foreach (AbstractVisualGenerator obj in LayerGenerators)
            {
                obj.color(easing, startTime, endTime, Convert.ToInt32(obj.Red * (startRed / 255.0)), Convert.ToInt32(obj.Green * (startGreen / 255.0)), Convert.ToInt32(obj.Blue * (startBlue / 255.0)),
                    Convert.ToInt32(obj.Red * (endRed / 255.0)), Convert.ToInt32(obj.Green * (endGreen / 255.0)), Convert.ToInt32(obj.Blue * (endBlue / 255.0)));
            }
        }

    }
}