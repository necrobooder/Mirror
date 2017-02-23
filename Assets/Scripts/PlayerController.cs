using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 velocityIndicator;

	private Rigidbody2D rb2d;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		// update velocity
		velocityIndicator = rb2d.velocity;

		// apply input force
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 inputMovement = new Vector2 (moveHorizontal, moveVertical);
		rb2d.AddForce (inputMovement*5);
	}
}
