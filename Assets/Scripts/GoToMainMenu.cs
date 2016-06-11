using UnityEngine;
using System.Collections;

public class GoToMainMenu : MonoBehaviour {
	void Start () {
		UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");
	}
}
