﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class CharacterSelect : MonoBehaviour {

	List<InputDevice> BoundDevices = new List<InputDevice>();
	bool[] PlayerSlots = new bool[]{ false, false };

	public Image player1;
	public Image player2;

	public Color notSelectedColor;
	public Color selectedColor;

	public Text p1Text;
	public Text p2Text;

	void Start () {
		InputManager.OnDeviceAttached += inputDevice => DeviceAttached(inputDevice);
		InputManager.OnDeviceDetached += inputDevice => DeviceDetached(inputDevice);
	}
	
	void Update () {
		ProcessInput();
	}

	void ProcessInput() {
		InputDevice device = InputManager.ActiveDevice;

		if ( device.Action1.WasPressed || device.MenuWasPressed ) {
			if ( !IsDeviceBound(device) ) {
				if ( AvailablePlayerSlot() ) {
					BindDevice(device);

					Player NewPlayer = new Player(GetNextPlayerIndex(), device);
					// Set the new player to the UI stuff and game manager
					//NewPlayer.IsReady();
					GameManager.Instance.Players.Add(NewPlayer);

					if ( NewPlayer.ID == 0 ) {
						p1Text.text = "Not Ready";
					} else {
						p2Text.text = "Not Ready";
					}

				} else {
					Debug.Log("All player slots full");
				}
			} else {
				Debug.Log("Device already bound.");
				GetPlayerIdWithDevice(device).IsReady();
				if ( GetPlayerIdWithDevice(device).ID == 0 ) {
					player1.color = selectedColor;
					p1Text.text = "Ready!";
				} else {
					player2.color = selectedColor;
					p2Text.text = "Ready!";
				}
			}
		} else if ( device.Action2.WasPressed ) {
			if ( GetPlayerIdWithDevice(device).Ready ) {
				GetPlayerIdWithDevice(device).NotReady();

				if ( GetPlayerIdWithDevice(device).ID == 0 ) {
					p1Text.text = "Not Ready";
				} else {
					p2Text.text = "Not Ready";
				}
			} else {
				// Remove player from that location
				Player plr = null;
				foreach ( Player p in GameManager.Instance.Players ) {
					if ( p.Device == device ) {
						plr = p;
					}
				}
				GameManager.Instance.Players.Remove(plr);


				if ( GetPlayerIdWithDevice(device).ID == 0 ) {
					player1.color = notSelectedColor;
					p1Text.text = "Press Start";
				} else {
					player2.color = notSelectedColor;
					p2Text.text = "Press Start";
				}

				// Unbind the device
				UnbindDevice(device);
			}
			Debug.Log("Action 2");
		} else if ( device.Action3.WasPressed ) {
			GetPlayerIdWithDevice(device).isCat = !GetPlayerIdWithDevice(device).isCat;
			Debug.Log("I'm changing my race!");
			Debug.Log("Action 3");
		} else if ( device.Action4.WasPressed ) {
			Debug.Log("Action 4");
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
		Player plr = null;
		foreach ( Player p in GameManager.Instance.Players ) {
			if ( p.Device == device ) {
				plr = p;
			}
		}
		GameManager.Instance.Players.Remove(plr);


		UnbindDevice(device);
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
