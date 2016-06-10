using UnityEngine;
using System.Collections;
using InControl;

public enum CharacterState
{
	idle,
	moving,
	blocking,
	punching,
	kicking
};

public class CharacterManager : MonoBehaviour
{
	public float health;
	public static float moveSpeed;
	public static float kickDamage;
	public static float punchDamage;
	public static float blockReduction;
	public CharacterState state = CharacterState.idle;

	


	//private ActionController;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.Action1)
		{
			state = CharacterState.punching;
    }
		else if(inputDevice.Action1)
		{
			state = CharacterState.blocking;
		}
		else if (inputDevice.Action1)
		{
			state = CharacterState.kicking;
		}
		else if (inputDevice.Action1)
		{
			//Secret fourth button
		}
		if (inputDevice.LeftStickX > 0.3)
		{
			state = CharacterState.moving;
		}
  }
}
