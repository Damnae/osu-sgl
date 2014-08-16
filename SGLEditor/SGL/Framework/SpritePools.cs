using SGL.Storyboard.Generators.Visual;
using System;
using System.Collections.Generic;

namespace SGL.Framework {
	public class SpritePools {
		private Dictionary<String, SpritePool> pools = new Dictionary<String, SpritePool>();

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin) {
			return GetPool(path, layer, origin).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, String path, String layer, String origin) {
			return GetPool(path, layer, origin).Get(startTime);
		}

		public void Release(SpriteGenerator sprite, double endTime) {
			GetPool(sprite.Filepath, sprite.Layer, sprite.Origin).Release(sprite, endTime);
		}

		public void Clear() {
			pools.Clear();
		}

		private SpritePool GetPool(String path, String layer, String origin) {
			String key = GetKey(path, layer, origin);

			SpritePool pool;
			if (!pools.TryGetValue(key, out pool)) {
				pool = new SpritePool(path, layer, origin);
				pools.Add(key, pool);
			}

			return pool;
		}

		private String GetKey(String path, String layer, String origin) {
			return path + "#" + layer + "#" + origin;
		}
	}
}
