using SGL.Storyboard.Generators.Visual;
using System;
using System.Collections.Generic;

namespace SGL.Framework {
	public class SpritePool {
		private string path;
		private string layer;
		private string origin;
		private bool additive;

		private List<PooledSprite> pooledSprites = new List<PooledSprite>();

		public SpritePool(String path, String layer, String origin, bool additive) {
			this.path = path;
			this.layer = layer;
			this.origin = origin;
			this.additive = additive;
		}

		public SpriteGenerator Get(double startTime) {
			foreach (var pooledSprite in pooledSprites) {
				if (pooledSprite.endTime < startTime) {
					pooledSprites.Remove(pooledSprite);
					return pooledSprite.sprite;
				}
			}

			return SB.Sprite(path, layer, origin);
		}

		public SpriteGenerator Get(double startTime, double endTime) {
			var sprite = Get(startTime);
			Release(sprite, endTime);
			return sprite;
		}

		public void Release(SpriteGenerator sprite, double endTime) {
			pooledSprites.Add(new PooledSprite(sprite, endTime));
		}

		public void Clear() {
			if (additive) {
				foreach (var pooledSprite in pooledSprites) {
					var sprite = pooledSprite.sprite;
					sprite.additive(sprite.GetCommandsStartTime(), (int)pooledSprite.endTime);
				}
			}
			pooledSprites.Clear();
		}

		class PooledSprite {
			public SpriteGenerator sprite;
			public double endTime;

			public PooledSprite(SpriteGenerator sprite, double endTime) {
				this.sprite = sprite;
				this.endTime = endTime;
			}
		}
	}
}
