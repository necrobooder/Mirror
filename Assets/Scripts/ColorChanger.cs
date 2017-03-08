using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	public Color A;
	public Color B;

	private float AH, AS, AV, BH, BS, BV;

	private SpriteRenderer sr;

	void Start()
	{
		sr = GetComponent<SpriteRenderer> ();
		Color.RGBToHSV (A, out AH, out AS, out AV);
		Color.RGBToHSV (B, out BH, out BS, out BV);
	}

	public void UpdateColor(float factorA, float factorB)
	{
		Color finalColor;

		if (factorA == 0 && factorB == 0) {
			finalColor = Color.clear;
		} else if (factorA > 0 && factorB == 0) {
			finalColor = new Color (A.r, A.g, A.b, factorA);
		} else if (factorA == 0 && factorB > 0) {
			finalColor = new Color (B.r, B.g, B.b, factorB);
		} else {
			float FH, FS, FV, factor = ((factorB - factorA) + 1) / 2;
			FH = AH + (BH - AH) * factor;
			FS = AS + (BS - AS) * factor;
			FV = AV + (BV - AS) * factor;
			finalColor = Color.HSVToRGB (FH, FS, FV);
		}

		sr.color = finalColor;
	}

}
