using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private static float speed = 12f;

	private Vector2 direction;
	private Impact impact;

	private Rigidbody2D rb2d;
	private ColorChanger colorChanger;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		colorChanger = this.transform.Find ("pad").GetComponent<ColorChanger> ();
		direction = Vector2.right;
	}

	void FixedUpdate()
	{
		ManageMovement ();
		ManageRotation ();
		ManageImpact ();
	}

	void ManageMovement()
	{
		float moveHorizontal = Input.GetAxis (Strings.HORIZONTAL_MOVEMENT);
		float moveVertical = Input.GetAxis (Strings.VERTICAL_MOVEMENT);
		Vector2 inputMovement = new Vector2 (moveHorizontal * speed, moveVertical * speed);
		rb2d.velocity = inputMovement;
	}

	void ManageRotation ()
	{
		Vector2 rotationAxisValue = new Vector2(Input.GetAxis (Strings.HORIZONTAL_ROTATION), Input.GetAxis(Strings.VERTICAL_ROTATION));
		if (rotationAxisValue.magnitude > .5f)
		{
			direction = rotationAxisValue;
			float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
			rb2d.MoveRotation (angle);
		}
	}

	void ManageImpact()
	{		
		float weak = Input.GetAxis (Strings.WEAK_HIT);
		float strong = Input.GetAxis (Strings.STRONG_HIT);

		colorChanger.UpdateColor (weak, strong);

		if (weak - strong > 0.6f)
			impact = Impact.weak;
		else if (strong - weak > 0.6f)
			impact = Impact.strong;
		else if (strong > 0.6f && weak > 0.6f)
			impact = Impact.polarized;
		else
			impact = Impact.standar;
	}

	public PadImpactMessage GetPadInfo()
	{
		return new PadImpactMessage (direction, impact);
	}

	public static float Speed {
		get{ return speed; }
	}
}
