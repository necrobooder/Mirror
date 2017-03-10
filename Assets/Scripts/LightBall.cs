using UnityEngine;
using System.Collections;

enum LightBallStatus{
	standar, weak, strong, bounce
}

public class LightBall : MonoBehaviour {

	private const float STRONG_SPEED = 35f;
	private const float WEAK_SPEED = 2f;
	private float BOUNCE_SPEED;
	private const float BOUNCE_DECAY_TIME = .2f;

	// members
	private LightBallStatus status;
	private float speed;
	private bool isInsidePlayfield;


	private float originalSpeed;
	private Rigidbody2D rb2d;
	private LineRenderer lineRenderer;
	private SpriteRenderer spriteRenderer;

	// variables
	private Vector2 velocity;
	private LightBallStatus nextStatus;

	void Start () {
		BOUNCE_SPEED = PlayerController.Speed + 10;

		// Get the rigid body component
		rb2d = GetComponent<Rigidbody2D> ();
		lineRenderer = GetComponent<LineRenderer> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		isInsidePlayfield = false;
		status = LightBallStatus.standar;
	}

	public void SetUp(Direction direction, int speed, Color color)
	{
		this.speed = originalSpeed = speed;

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
		if (spriteRenderer == null) {
			spriteRenderer = GetComponent<SpriteRenderer> ();
		}
		spriteRenderer.color = color;
	}

	void FixedUpdate()
	{
		if (status == LightBallStatus.bounce) {			
			switch (nextStatus) {
			case LightBallStatus.weak:
				speed -= Time.deltaTime * (BOUNCE_SPEED - WEAK_SPEED) / BOUNCE_DECAY_TIME;
				if (speed < WEAK_SPEED) {
					speed = WEAK_SPEED;
					status = LightBallStatus.weak;
				}
				break;
			case LightBallStatus.standar:
				speed -= Time.deltaTime * (BOUNCE_SPEED - originalSpeed) / BOUNCE_DECAY_TIME;
				if (speed < originalSpeed) {
					speed = originalSpeed;
					status = LightBallStatus.standar;
				}
				break;
			}
			velocity.Set(velocity.normalized.x * speed, velocity.normalized.y * speed);
		}
			rb2d.velocity = velocity;
	}

	void OnPadImpact(PadImpactMessage msg)
	{
		switch (msg.impact) {
		case Impact.weak:
			speed = BOUNCE_SPEED;
			status = LightBallStatus.bounce;
			nextStatus = LightBallStatus.weak;
			break;
		case Impact.strong:
			speed = STRONG_SPEED;
			status = LightBallStatus.strong;
			break;
		case Impact.standar:
			speed = BOUNCE_SPEED;
			status = LightBallStatus.bounce;
			nextStatus = LightBallStatus.standar;
			break;
		default:
			break;
		}
		velocity.Set(msg.direction.normalized.x * speed, msg.direction.normalized.y * speed);
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.tag == Strings.TAG_WALL)
		{
			if (isInsidePlayfield)
				Destroy (gameObject);
			else
				isInsidePlayfield = true;
		}
			
	}

	void OnPadFocus (PadFocusMessage msg)
	{
		RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y), msg.direction);
		if (hit) {
			Color semitransparentColor1 = new Color (spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
			Color semitransparentColor2 = new Color (spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.01f);
			lineRenderer.SetColors (semitransparentColor1, semitransparentColor2);
			lineRenderer.SetVertexCount (2);
			lineRenderer.SetPositions(new [] { transform.localPosition, new Vector3(hit.point.x, hit.point.y, 1) });		
		}
	}

	void OnPadLostFocus()
	{
		lineRenderer.SetVertexCount (0);
	}
}
