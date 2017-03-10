using UnityEngine;

// Physics 
public struct PadImpactMessage
{
	public Vector2 direction;
	public Impact impact;

	public PadImpactMessage(Vector2 direction, Impact impact)
	{
		this.direction = direction;
		this.impact = impact;
	}
}

public struct PadFocusMessage
{
	public Vector3 direction;

	public PadFocusMessage(Vector3 direction)
	{
		this.direction = direction;
	}
}
