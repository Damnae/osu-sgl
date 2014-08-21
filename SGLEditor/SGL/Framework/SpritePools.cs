using SGL.Storyboard.Generators.Visual;
using System;
using System.Collections.Generic;

namespace SGL.Framework {
	public class SpritePools {
		private Dictionary<String, SpritePool> pools = new Dictionary<String, SpritePool>();

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin, bool additive, int group) {
			return GetPool(path, layer, origin, additive, group).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin, bool additive) {
			return GetPool(path, layer, origin, additive, 0).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin, int group) {
			return GetPool(path, layer, origin, false, group).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin) {
			return GetPool(path, layer, origin, false, 0).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, String path, String layer, String origin) {
			return GetPool(path, layer, origin, false, 0).Get(startTime);
		}

		public void Release(SpriteGenerator sprite, double endTime) {
			GetPool(sprite.Filepath, sprite.Layer, sprite.Origin, false, 0).Release(sprite, endTime);
		}

		public void Clear() {
			foreach (var pool in pools)
				pool.Value.Clear();
			pools.Clear();
		}

		private SpritePool GetPool(String path, String layer, String origin, bool additive, int group) {
			String key = GetKey(path, layer, origin, additive, group);

			SpritePool pool;
			if (!pools.TryGetValue(key, out pool)) {
				pool = new SpritePool(path, layer, origin, additive);
				pools.Add(key, pool);
			}

			return pool;
		}

		private String GetKey(String path, String layer, String origin, bool additive, int group) {
			return path + "#" + layer + "#" + origin + "#" + (additive ? "1" : "0") + "#" + group;
		}
	}
}
