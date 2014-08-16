
using System;

namespace SGL.Framework.Curve {
	public class TransformedCurve : Curve {
		private Curve curve;
		private Vector2 offset;
		private float size;
		private float rotation;
		private Vector2 rotationCenter;
		private bool reversed;

		public TransformedCurve(Curve curve, Vector2 offset, float size, float rotation, Vector2 rotationCenter, bool reversed = false) {
			this.curve = curve;
			this.offset = offset;
			this.size = size;
			this.rotation = rotation;
			this.rotationCenter = rotationCenter;
			this.reversed = reversed;
		}

		public float Length {
			get { return curve.Length * size; }
		}

		public Vector2 StartPosition {
			get { return Rotate((reversed ? curve.EndPosition : curve.StartPosition) * size + offset); }
		}

		public Vector2 EndPosition {
			get { return Rotate((reversed ? curve.StartPosition : curve.EndPosition) * size + offset); }
		}

		public Vector2 PositionAtDistance(float distance) {
			return Rotate(curve.PositionAtDistance(reversed ? curve.Length - distance : distance) * size + offset);
		}

		public Vector2 PositionAtDelta(float delta) {
			return Rotate(curve.PositionAtDelta(reversed ? 1.0f - delta : delta) * size + offset);
		}

		private Vector2 Rotate(Vector2 point) {
			var v = point - rotationCenter;
			var distance = Math.Sqrt(v.X * v.X + v.Y * v.Y);
			var angle = Math.Atan2(v.Y, v.X) + rotation;

			return rotationCenter + new Vector2(
				(float)(Math.Cos(angle) * distance),
				(float)(Math.Sin(angle) * distance));
		}
	}
}
