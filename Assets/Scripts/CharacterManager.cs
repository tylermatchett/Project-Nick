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
	public float moveSpeed;
	public float kickDamage;
	public float punchDamage;
	public float blockReduction;
	public CharacterState state = CharacterState.idle;
	public Player player;
	private ActionController actionController;


	//private ActionController;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var inputDevice = player.Device;
		if (inputDevice.Action1)
		{
			state = CharacterState.punching;
			//actionController
    }
		else if(inputDevice.Action2)
		{
			state = CharacterState.kicking;
		}
		else if (inputDevice.Action3)
		{
			state = CharacterState.blocking;
		}
/*
		else if (inputDevice.Action4)
		{
			//Secret fourth button
		}
//*/
		if (inputDevice.LeftStickX > 0.3)
		{
			state = CharacterState.moving;
		}
  }
}
