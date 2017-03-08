using UnityEngine;

// Physics 
public struct PadCollidedWithLightBallMessage
{
	public Vector2 direction;
	public Impact impact;

	public PadCollidedWithLightBallMessage(Vector2 direction, Impact impact)
	{
		this.direction = direction;
		this.impact = impact;
	}
}
