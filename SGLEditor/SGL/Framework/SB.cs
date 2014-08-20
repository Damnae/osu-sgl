using System;
using System.Collections.Generic;
using System.Text;
using SGL.Storyboard.Generators.Visual;
using SGL.Elements;

namespace SGL.Framework {

	public class SB {

		// layer
		public static String Background = "Background";
		public static String Fail = "Fail";
		public static String Pass = "Pass";
		public static String Foreground = "Foreground";

		// origin
		public static String TopLeft = "TopLeft";
		public static String TopCentre = "TopCentre";
		public static String TopRight = "TopRight";
		public static String CentreLeft = "CentreLeft";
		public static String Centre = "Centre";
		public static String CentreRight = "CentreRight";
		public static String BottomLeft = "BottomLeft";
		public static String BottomCentre = "BottomCentre";
		public static String BottomRight = "BottomRight";

		// loopType
		public static String LoopForever = "LoopForever";
		public static String LoopOnce = "LoopOnce";

		// loopTrigger
		public static string HitSoundClap = "HitSoundClap";
		public static string HitSoundFinish = "HitSoundFinish";
		public static string HitSoundWhistle = "HitSoundWhistle";
		public static string Passing = "Passing";
		public static string Failing = "Failing";

		/// <summary>
		/// Creates a sprite.
		/// ex: 
		///		var sprite = SB.Sprite("s.png", SB.Foreground, SB.Centre);
		/// </summary>
		public static SpriteGenerator Sprite(String path, String layer, String origin) {
			var generator = new SpriteGenerator(layer, origin, path);
			GlobalMemory.Instance.RegisterStoryboardGenerator(generator);
			return generator;
		}

		/// <summary>
		/// Creates an animated sprite.
		/// ex: 
		///		var animatedSprite = SB.Animation("s.png", SB.Foreground, SB.Centre, 8, 16, SB.LoopForever);
		/// </summary>
		public static AnimationGenerator Animation(String path, String layer, String origin, int frameCount, int frameDelay, String loopType) {
			var generator = new AnimationGenerator(layer, origin, path, frameCount, frameDelay, loopType);
			GlobalMemory.Instance.RegisterStoryboardGenerator(generator);
			return generator;
		}

		public static String GenerateCode() {
			return GlobalMemory.Instance.StoryboardCode.ToString();
		}

		public static void Clear() {
			GlobalMemory.Clear();
		}

		/// <summary>
		/// Lets you find the background scale that osu would use. 
		/// You can then use a scale command with the value returned from this function to scale the background correctly.
		/// ex: 
		///		var background = SB.Sprite("bg.png", SB.Background, SB.Centre);
		///		background.scale(0, SB.GetBackgroundScale(1366, 768));
		/// </summary>
		/// <param name="backgroundImageWidth">The background image width in pixels</param>
		/// <param name="backgroundImageHeight">The background image height in pixels</param>
		/// <returns></returns>
		public static double GetBackgroundScale(int backgroundImageWidth, int backgroundImageHeight) {
			double bgHeightScale = 480.0 / backgroundImageWidth;
			double bgWidthScale = 640.0 * (1024.0 / 1366.0) / backgroundImageHeight;
			return bgHeightScale > bgWidthScale ? bgHeightScale : bgWidthScale;
		}
	}
}
