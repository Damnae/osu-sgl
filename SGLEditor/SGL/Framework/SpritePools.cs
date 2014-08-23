using SGL.Storyboard.Generators.Visual;
using System;
using System.Collections.Generic;

namespace SGL.Framework {
	public class SpritePools {
		private Dictionary<String, SpritePool> pools = new Dictionary<String, SpritePool>();
		private List<SpriteGroup> spriteGroups = new List<SpriteGroup>();

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin, bool additive, SpriteGroup spriteGroup, int poolGroup) {
			return GetPool(path, layer, origin, additive, spriteGroup, poolGroup).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin, bool additive, SpriteGroup spriteGroup) {
			return GetPool(path, layer, origin, additive, spriteGroup, 0).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin, bool additive, int poolGroup) {
			return GetPool(path, layer, origin, additive, null, poolGroup).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin, bool additive) {
			return GetPool(path, layer, origin, additive, null, 0).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin, int poolGroup) {
			return GetPool(path, layer, origin, false, null, poolGroup).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, double endTime, String path, String layer, String origin) {
			return GetPool(path, layer, origin, false, null, 0).Get(startTime, endTime);
		}

		public SpriteGenerator Get(double startTime, String path, String layer, String origin) {
			return GetPool(path, layer, origin, false, null, 0).Get(startTime);
		}

		public void Release(SpriteGenerator sprite, double endTime) {
			GetPool(sprite.Filepath, sprite.Layer, sprite.Origin, false, null, 0).Release(sprite, endTime);
		}

		public void Clear() {
			foreach (var pool in pools)
				pool.Value.Clear();
			pools.Clear();
		}

		private SpritePool GetPool(String path, String layer, String origin, bool additive, SpriteGroup spriteGroup, int poolGroup) {
			String key = GetKey(path, layer, origin, additive, spriteGroup, poolGroup);

			SpritePool pool;
			if (!pools.TryGetValue(key, out pool)) {
				pool = new SpritePool(path, layer, origin, additive, spriteGroup);
				pools.Add(key, pool);
			}

			return pool;
		}

		private String GetKey(String path, String layer, String origin, bool additive, SpriteGroup spriteGroup, int poolGroup) {
			return path + "#" + layer + "#" + origin + "#" + (additive ? "1" : "0") + "#" + GetSpriteGroupId(spriteGroup) + "#" + poolGroup;
		}

		private int GetSpriteGroupId(SpriteGroup spriteGroup) {
			if (spriteGroup == null)
				return -1;

			var index = spriteGroups.IndexOf(spriteGroup);
			if (index < 0) {
				spriteGroups.Add(spriteGroup);
				return spriteGroups.Count - 1;
			}

			return index;
		}
	}
}
