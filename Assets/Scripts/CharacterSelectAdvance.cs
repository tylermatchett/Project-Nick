using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelectAdvance : MonoBehaviour {

	public Text txtReadyCount;

	void Start() {
		GameManager.Instance.Players.Clear();
        SoundManager.Instance.Play(SoundType.MenuMusic);
	}

	void Update () {
		int readyCount = 0;

		for ( int i = 0; i < GameManager.Instance.Players.Count; i++ ) {
			if ( GameManager.Instance.Players[i].Ready )
				readyCount++;
		}

		txtReadyCount.text = readyCount + " Ready";

		if (readyCount > 1) {
			Invoke("ReadyToGo", 1f);
			txtReadyCount.text = "All Players Ready";
		}
	}

	void ReadyToGo() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}
}
