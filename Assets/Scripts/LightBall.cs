using UnityEngine;
using System.Collections;

public class LightBall : MonoBehaviour {

	public Direction direction;
	public int speed;

	public Vector2 velocity;
	private Rigidbody2D rb2d;

	void Start () {
		// Get the rigid body component
		rb2d = GetComponent<Rigidbody2D> ();
		if (!rb2d)
			print ("no rigidbody found");
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

		if (!rb2d)
			print ("none found");
		else
			rb2d.velocity = velocity;
	}

	void FixedUpdate()
	{
		if (!rb2d)
			print ("none found");
		else
			rb2d.velocity = velocity;
	}
}
