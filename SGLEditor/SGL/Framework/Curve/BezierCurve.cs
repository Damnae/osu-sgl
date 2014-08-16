using System;
using System.Collections;
using System.Collections.Generic;

namespace SGL.Framework.Curve {
	public class BezierCurve : Curve {
		private const int DEFAULT_PRECISION = 100;
		private List<Vector2> points;
		private int precision;

		// cache
		private List<Tuple<float, Vector2>> distancePositionTuples;
		private float length = -1.0f;

		public Vector2 StartPosition { get { return points[0]; } }
		public Vector2 EndPosition { get { return points[points.Count - 1]; } }

		public float Length {
			get {
				if (length < 0)
					InitializeCache();

				return length;
			}
		}

		public BezierCurve(String points)
			: this(toPointList(points), DEFAULT_PRECISION) {
		}

		public BezierCurve(List<Vector2> points) {
			this.points = points;
			this.precision = DEFAULT_PRECISION;
			InitializeCache();
		}

		public BezierCurve(String points, int precision)
			: this(toPointList(points), precision) {
		}

		public BezierCurve(List<Vector2> points, int precision) {
			this.points = points;
			this.precision = precision;
			InitializeCache();
		}

		public Vector2 PositionAtDistance(float distance) {
			if (distancePositionTuples == null)
				InitializeCache();

			var previousDistance = 0.0f;
			var previousPosition = StartPosition;

			var nextDistance = length;
			var nextPosition = EndPosition;

			var i = 0;
			while (i < distancePositionTuples.Count) {
				var distancePositionTuple = distancePositionTuples[i];
				if (distancePositionTuple.Item1 > distance)
					break;

				previousDistance = distancePositionTuple.Item1;
				previousPosition = distancePositionTuple.Item2;

				++i;
			}

			if (i < distancePositionTuples.Count - 1) {
				var distancePositionTuple = distancePositionTuples[i + 1];
				nextDistance = distancePositionTuple.Item1;
				nextPosition = distancePositionTuple.Item2;
			}

			var delta = (distance - previousDistance) / nextDistance;
			Vector2 previousToNext = nextPosition - previousPosition;

			return previousPosition + previousToNext * delta;
		}

		/// <summary>
		/// </summary>
		/// <param name="delta">A value in range 0 - 1, 0 being the beginning of the curve and 1 being the end</param>
		/// <returns></returns>
		public Vector2 PositionAtDelta(float delta) {
			var pointsOrder = points.Count;

			Vector2[] intermediatePoints = new Vector2[pointsOrder];
			for (int i = 0; i < pointsOrder; ++i)
				intermediatePoints[i] = points[i];

			for (int order = 1; order < pointsOrder; ++order)
				for (int i = 0; i < pointsOrder - order; ++i)
					intermediatePoints[i] =
						intermediatePoints[i] * (1 - delta) +
						intermediatePoints[i + 1] * delta;

			return intermediatePoints[0];
		}

		public static List<Vector2> toPointList(string points) {
			List<Vector2> pointsList = new List<Vector2>();
			foreach (var point in points.Split('|')) {
				var coords = point.Split(':');
				pointsList.Add(new Vector2(float.Parse(coords[0]), float.Parse(coords[1])));
			}
			return pointsList;
		}

		private void InitializeCache() {
			// Ignore precision if the curve is a line (less than 3 points)
			var precision = points.Count > 2 ? this.precision : 0;

			distancePositionTuples = new List<Tuple<float, Vector2>>(precision);
			var distance = 0.0f;

			Vector2 previousPosition = StartPosition;
			for (int i = 1; i <= precision; ++i) {
				var delta = (float)i / (precision + 1);
				var nextPosition = PositionAtDelta(delta);

				distance += Vector2.Distance(previousPosition, nextPosition);
				distancePositionTuples.Add(new Tuple<float, Vector2>(distance, nextPosition));

				previousPosition = nextPosition;
			}

			distance += Vector2.Distance(previousPosition, EndPosition);
			length = distance;
		}

		public class Tuple<T, U> {
			private T _item1;
			private U _item2;

			public Tuple(T item1, U item2) {
				_item1 = item1;
				_item2 = item2;
			}

			public T Item1 { get { return _item1; } }
			public U Item2 { get { return _item2; } }
		}
	}
}
