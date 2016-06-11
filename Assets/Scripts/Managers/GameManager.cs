using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class GameManager : MonoBehaviour {
	private static GameManager _instance;
	static public GameManager Instance {
		get {
			if ( _instance == null ) {
				_instance = UnityEngine.Object.FindObjectOfType(typeof(GameManager)) as GameManager;

				if ( _instance == null ) {
					Debug.LogError("[ No Instance was found for GameManager check for errors ]");
					GameObject go = new GameObject("GameManager");
					DontDestroyOnLoad(go);

					_instance = go.AddComponent<GameManager>();
					Debug.Log("[ Creating A New GameManager ]");
				}
			}
			return _instance;
		}
	}

	public List<Player> Players = new List<Player>();

	void Start() {
		Debug.Log("[GameManager] Start");
	}
	
}
