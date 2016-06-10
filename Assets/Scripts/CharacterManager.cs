using UnityEngine;
using System.Collections;
using InControl;

public enum CharacterState
{
	idle,
	moving = 1,
	blocking = 2,
	punching = 4,
	kicking = 8
};

public class CharacterManager : MonoBehaviour
{
	public float health;
	public float moveSpeed;
	public float kickDamage;
	public float punchDamage;
	public float blockReduction;
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
		}else if(inputDevice.LeftStickX > 0.3 && state == (CharacterState.idle | CharacterState.moving))
		{
			state = CharacterState.moving;
		}
  }
}
