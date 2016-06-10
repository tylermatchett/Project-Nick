using UnityEngine;
using System.Collections;
using InControl;

[System.Serializable]
public class Player {
	int id;
	InputDevice device;
	bool aicontrolled = false;
	bool ready = false;

	public Player(int id, InputDevice device) {
		// Load in the weapon and abilities data
		this.id = id;
		this.device = device;
	}
	
	// Getters
	public int ID {
		get { return id; }
	}
	public InputDevice Device {
		get { return device; }
	}
	public bool AIControlled {
		get { return aicontrolled; }
	}
	public bool Ready {
		get { return ready; }
	}
	
	// Ready Selection Save
	public void IsReady() {
		ready = true;
	}
	public void NotReady() {
		ready = false;
	}
}