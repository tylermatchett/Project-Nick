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
	List<InputDevice> BoundDevices = new List<InputDevice>();
	bool[] PlayerSlots = new bool[]{ false, false, false, false };

	void Start() {
		Debug.Log("[GameManager] Start");

		InputManager.OnDeviceAttached += inputDevice => DeviceAttached(inputDevice);
		InputManager.OnDeviceDetached += inputDevice => DeviceDetached(inputDevice);
	}

	void ProcessInput() {
		InputDevice device = InputManager.ActiveDevice;

		if ( device.Action1.WasPressed || device.MenuWasPressed ) {
			if ( !IsDeviceBound(device) ) {
				if ( AvailablePlayerSlot() ) {
					BindDevice(device);

					Player NewPlayer = new Player(GetNextPlayerIndex(), device);
					// Set the new player to the UI stuff and game manager
					Players.Add(NewPlayer);

					Debug.Log("New player with an id of " + NewPlayer.ID + " created with " + device + " device.");
				} else {
					Debug.Log("All player slots full");
				}
			} else {
				Debug.Log("Device already bound.");
			}
		} else if ( device.Action3.WasPressed ) {
		}
	}

	bool IsDeviceBound(InputDevice device) {
		return BoundDevices.Contains(device);
	}
	void BindDevice(InputDevice device) {
		BoundDevices.Add(device);
	}
	public void UnbindDevice(InputDevice device) {
		BoundDevices.Remove(device);
		Player temp = GetPlayerIdWithDevice(device);
		if ( temp != null ) {
			PlayerSlots[temp.ID] = false;
		}
	}

	bool AvailablePlayerSlot() {
		for ( int i = 0; i < PlayerSlots.Length; i++ ) {
			if ( !PlayerSlots[i] ) {
				return true;
			}
		}
		return false;
	}
	int GetNextPlayerIndex() {
		for ( int i = 0; i < PlayerSlots.Length; i++ ) {
			if ( !PlayerSlots[i] ) {
				PlayerSlots[i] = true;
				return i;
			}
		}

		return -1;
	}


	void DeviceAttached(InputDevice device) {
		Debug.Log("Attached: " + device.Name);
	}
	void DeviceDetached(InputDevice device) {
		Debug.Log("Detached: " + device.Name);

		// UI Exit condition - a player is backing out
		Player temp = GetPlayerIdWithDevice(device);
		if ( temp != null ) {
			PlayerSlots[temp.ID] = false;
		}
	}

	Player GetPlayerIdWithDevice(InputDevice device) {
		for ( int i = 0; i < GameManager.Instance.Players.Count; i++ ) {
			if ( GameManager.Instance.Players[i].Device == device ) {
				return GameManager.Instance.Players[i];
			}
		}

		return null;
	}

}
