
namespace SGL.Framework.Curve {
	public interface Curve {
		Vector2 StartPosition { get; }
		Vector2 EndPosition { get; }
		float Length { get; }

		Vector2 PositionAtDistance(float distance);
		Vector2 PositionAtDelta(float delta);
	}
}
