using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private const float SPEED_MODIFIER = 10;

	public Vector2 rotation;
	public float angle;

	private Rigidbody2D rb2d;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		ManageMovement ();
		ManageRotation ();
	}

	void ManageMovement()
	{
		float moveHorizontal = Input.GetAxis ("H_Movement");
		float moveVertical = Input.GetAxis ("V_Movement");
		Vector2 inputMovement = new Vector2 (moveHorizontal * SPEED_MODIFIER, moveVertical * SPEED_MODIFIER);
		rb2d.velocity = inputMovement;
	}

	void ManageRotation ()
	{
		rotation = new Vector2(Input.GetAxis ("H_Rotation"), Input.GetAxis("V_Rotation"));
		if (rotation.magnitude > .3f)
		{
			Vector2 vAux = rotation - Vector2.zero;
			angle = Mathf.Atan2(vAux.y, vAux.x)*Mathf.Rad2Deg;
			rb2d.MoveRotation (angle);
		}
	}
}
