using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadyUpBro : MonoBehaviour {

	public Image img;

	bool fadeIn = true;
	float speed = 2f;
	float alpha = 1f;

	void Update () {
		if ( fadeIn ) {
			alpha = Mathf.Lerp(alpha, 1f, Time.deltaTime * speed);
		} else {
			alpha = Mathf.Lerp(alpha, 0f, Time.deltaTime * speed);
		}

		if (alpha > 0.9f || alpha < 0.1f) {
			fadeIn = !fadeIn;
		}

		img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
	}
}
