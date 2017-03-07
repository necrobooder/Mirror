using UnityEngine;

// Physics 
public struct PadCollidedWithLightBallMessage
{
	Vector2 direction;
	Impact impact;

	PadCollidedWithLightBallMessage(Vector2 direction, Impact impact)
	{
		this.direction = direction;
		this.impact = impact;
	}
}
