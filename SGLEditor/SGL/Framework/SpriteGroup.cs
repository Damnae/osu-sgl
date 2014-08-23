using SGL.Elements;
using SGL.Storyboard.Generators;
using SGL.Storyboard.Generators.Visual;
using System;
using System.Collections.Generic;

namespace SGL.Framework {
	public class SpriteGroup {
		private List<AbstractGenerator> generators = new List<AbstractGenerator>();

		public SpriteGenerator Sprite(String path, String layer, String origin) {
			var generator = new SpriteGenerator(layer, origin, path);
			generators.Add(generator);
			return generator;
		}

		public AnimationGenerator Animation(String path, String layer, String origin, int frameCount, int frameDelay, String loopType) {
			var generator = new AnimationGenerator(layer, origin, path, frameCount, frameDelay, loopType);
			generators.Add(generator);
			return generator;
		}

		public void InsertSprites() {
			foreach (var generator in generators)
				GlobalMemory.Instance.RegisterStoryboardGenerator(generator);
			generators.Clear();
		}
	}
}
