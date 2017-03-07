using UnityEngine;
using System.Collections;

public class Pad : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col)
	{
		col.gameObject.SendMessage("OnPadImpact", new PadCollidedWithLightBallMessage());
	}
}
