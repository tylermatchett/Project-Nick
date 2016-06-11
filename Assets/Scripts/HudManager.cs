using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    public RectTransform player1;
    public RectTransform player2;

    float t_scale = 1f;
    float t_scale2 = 1f;

    float targetScale = 1f;
    float targetScale2 = 2f;

	void Update () {
        targetScale = GameManager.Instance.Players[0].Health / 100f;
        targetScale2 = GameManager.Instance.Players[1].Health / 100f;
        t_scale = Mathf.Lerp(t_scale, targetScale, Time.deltaTime);
        t_scale2 = Mathf.Lerp(t_scale2, targetScale2, Time.deltaTime);

		if ( t_scale < 0f )
			t_scale = 0f;

		if ( t_scale2 < 0f )
			t_scale2 = 0f;

		player1.transform.localScale = new Vector3(t_scale, player1.transform.localScale.y, player1.transform.localScale.z);
        player2.transform.localScale = new Vector3(t_scale2, player2.transform.localScale.y, player2.transform.localScale.z);
    }
}
