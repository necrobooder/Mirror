using UnityEngine;
using System.Collections;

public class Pad : MonoBehaviour {

	public LayerMask mask;

	private PlayerController characterInstance;

	private GameObject previousHit;

	void Start()
	{
		characterInstance = GetComponentInParent<PlayerController> ();
		previousHit = null;
	}

	void Update(){
		BoxCollider2D collider = GetComponent<BoxCollider2D> ();
		RaycastHit2D hit = Physics2D.BoxCast (collider.transform.position, new Vector2 (3,3), 0f, new Vector2(transform.right.x, transform.right.y), 7f, mask, -Mathf.Infinity, Mathf.Infinity);
		if (hit) {
			if (previousHit != null && hit.transform.gameObject != previousHit) {
				previousHit.SendMessage ("OnPadLostFocus");
			}				
			hit.transform.gameObject.SendMessage ("OnPadFocus", new PadFocusMessage (transform.right));
			previousHit = hit.transform.gameObject;
		} else {
			if (previousHit != null) {
				previousHit.SendMessage ("OnPadLostFocus");
			}
		}

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		col.gameObject.SendMessage ("OnPadImpact", characterInstance.GetPadInfo());
	}
}
