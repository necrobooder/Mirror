using UnityEngine;
using System.Collections;

public class Pad : MonoBehaviour {

	private PlayerController characterInstance;

	void Start()
	{
		characterInstance = GetComponentInParent<PlayerController> ();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		col.gameObject.SendMessage ("OnPadImpact", characterInstance.GetPadInfo());
	}
}
