using System;
using System.Collections.Generic;
using System.Text;

namespace SGL.Util {
	static class ColorUtil {

		public static void HsbToRgb(double hue, double saturation, double brightness, out int red, out int green, out int blue) {
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			double b = brightness * 255;
			int v = Convert.ToInt32(b);
			int p = Convert.ToInt32(b * (1 - saturation));
			int q = Convert.ToInt32(b * (1 - f * saturation));
			int t = Convert.ToInt32(b * (1 - (1 - f) * saturation));

			if (hi == 0) {
				red = v;
				green = t;
				blue = p;

			} else if (hi == 1) {
				red = q;
				green = v;
				blue = p;

			} else if (hi == 2) {
				red = p;
				green = v;
				blue = t;

			} else if (hi == 3) {
				red = p;
				green = q;
				blue = v;

			} else if (hi == 4) {
				red = t;
				green = p;
				blue = v;

			} else {
				red = v;
				green = p;
				blue = q;
			}
		}
	}
}
