using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private const float SPEED_MODIFIER = 10;

	private Vector2 direction;
	private Impact impact;

	private Rigidbody2D rb2d;
	private ColorChanger colorChanger;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();

		colorChanger = this.transform.Find ("pad").GetComponent<ColorChanger> ();
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
		Vector2 inputMovement = new Vector2 (moveHorizontal * SPEED_MODIFIER, moveVertical * SPEED_MODIFIER);
		rb2d.velocity = inputMovement;
	}

	void ManageRotation ()
	{
		direction = new Vector2(Input.GetAxis (Strings.HORIZONTAL_ROTATION), Input.GetAxis(Strings.VERTICAL_ROTATION));
		if (direction.magnitude > .3f)
		{
			Vector2 vAux = direction - Vector2.zero;
			float angle = Mathf.Atan2(vAux.y, vAux.x)*Mathf.Rad2Deg;
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

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag (Strings.TAG_LIGHTBALL))
		{
			//Destroy (col.gameObject);
		}
	}

	public PadCollidedWithLightBallMessage GetPadInfo()
	{
		return new PadCollidedWithLightBallMessage (direction, impact);
	}
}
