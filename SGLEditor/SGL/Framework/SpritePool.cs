using SGL.Storyboard.Generators.Visual;
using System;
using System.Collections.Generic;

namespace SGL.Framework {
	public class SpritePool {
		private string path;
		private string layer;
		private string origin;

		private List<PooledSprite> pooledSprites = new List<PooledSprite>();

		public SpritePool(String path, String layer, String origin) {
			this.path = path;
			this.layer = layer;
			this.origin = origin;
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
