using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private const float SPEED_MODIFIER = 10;

	public Vector2 rotation;
	public float angle;

	private Rigidbody2D rb2d;
	private SpriteRenderer strongSpriteRenderer;
	private SpriteRenderer weakSpriteRenderer;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();

		weakSpriteRenderer = this.transform.Find ("pad/blue").GetComponent<SpriteRenderer> ();
		strongSpriteRenderer = this.transform.Find ("pad/orange").GetComponent<SpriteRenderer> ();
	}

	void FixedUpdate()
	{
		ManageMovement ();
		ManageRotation ();
		ManageImpact ();
	}

	void ManageMovement()
	{
		float moveHorizontal = Input.GetAxis (InputTypes.HORIZONTAL_MOVEMENT);
		float moveVertical = Input.GetAxis (InputTypes.VERTICAL_MOVEMENT);
		Vector2 inputMovement = new Vector2 (moveHorizontal * SPEED_MODIFIER, moveVertical * SPEED_MODIFIER);
		rb2d.velocity = inputMovement;
	}

	void ManageRotation ()
	{
		rotation = new Vector2(Input.GetAxis (InputTypes.HORIZONTAL_ROTATION), Input.GetAxis(InputTypes.VERTICAL_ROTATION));
		if (rotation.magnitude > .3f)
		{
			Vector2 vAux = rotation - Vector2.zero;
			angle = Mathf.Atan2(vAux.y, vAux.x)*Mathf.Rad2Deg;
			rb2d.MoveRotation (angle);
		}
	}

	void ManageImpact()
	{
		float weak = Input.GetAxis (InputTypes.WEAK_HIT);
		float strong = Input.GetAxis (InputTypes.STRONG_HIT);
		Color auxiliarColor;

		auxiliarColor = weakSpriteRenderer.color;
		auxiliarColor.a = weak;
		weakSpriteRenderer.color = auxiliarColor;

		auxiliarColor = strongSpriteRenderer.color;
		auxiliarColor.a = strong;
		strongSpriteRenderer.color = auxiliarColor;
	}
}
