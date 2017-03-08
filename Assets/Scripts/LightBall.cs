using UnityEngine;
using System.Collections;

public class LightBall : MonoBehaviour {

	// members
	private Direction direction;
	private int speed;
	private Rigidbody2D rb2d;

	// variables
	public Vector2 velocity;

	void Start () {
		// Get the rigid body component
		rb2d = GetComponent<Rigidbody2D> ();
	}

	public void SetUp(Direction direction, int speed)
	{
		this.direction = direction;
		this.speed = speed;

		switch (direction) {
		case Direction.north:
			velocity = new Vector2 (0, speed);
			break;
		case Direction.south:
			velocity = new Vector2 (0, -speed);
			break;
		case Direction.east:
			velocity = new Vector2 (speed, 0);
			break;
		case Direction.west:
			velocity = new Vector2 (-speed, 0);
			break;
		default:
			break;
		}
	}

	void FixedUpdate()
	{
			rb2d.velocity = velocity;
	}

	void OnPadImpact(PadCollidedWithLightBallMessage msg)
	{
		switch (msg.impact) {
		case Impact.weak:
			velocity = new Vector2 (msg.direction.normalized.x * -2f, msg.direction.normalized.y * -2f);
			break;
		case Impact.strong:
			velocity = new Vector2 (msg.direction.normalized.x * -35f, msg.direction.normalized.y * -35f);
			break;
		default:
			break;
		}
	}
}
