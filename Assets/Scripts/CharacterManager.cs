using UnityEngine;
using System.Collections;
using InControl;


public enum CharacterState
{
	idle,
	moving,
	ducking,
	blocking,
	punching,
	kicking,
	stunned
};

public class CharacterManager : MonoBehaviour
{
	public float moveSpeed;
	public float kickDamage;
	public float punchDamage;
	public float blockReduction;
	public Vector2 direction;
	private float stickDeadzone = 0.15f;
	public CharacterState state = CharacterState.idle;
	public Player player = null;
	public ActionController actionController;
	private float unlockTimer;
	private bool actionLock;
	private float punchTime = 0.3f, kickTime = 0.4f, stunTime = 0.2f;
	
	void Start() {
	}

	void Update ()
	{
		Debug.Log("Player is not null: " + (player != null));
		if (player != null)
			handleInput();

		if(unlockTimer < 0 && actionLock)
		{
			actionLock = false;
		}
  }

	private void handleInput()
	{
		if ( player.Device.Action1 && state <= CharacterState.blocking)
		{
			state = CharacterState.punching;
			actionController.Punch();
			unlockTimer = punchTime;
			actionLock = true;
		}
		else if ( player.Device.Action2 && state <= CharacterState.blocking)
		{
			state = CharacterState.kicking;
			actionController.Kick();
			unlockTimer = kickTime;
			actionLock = true;
		}
		else if ( player.Device.Action3 && state <= CharacterState.blocking)
		{
			state = CharacterState.blocking;
			actionController.Block();
		}
		/*
				else if (inputDevice.Action4)
				{
					//Secret fourth button
				}
		//*/
		else if (state <= CharacterState.moving)
		{
			if ( player.Device.Direction.Vector.magnitude > stickDeadzone)
			{
				state = CharacterState.moving;
				direction = player.Device.Direction.Vector.normalized;
			}
			else
			{
				state = CharacterState.idle;
			}
		}
	}

	public void onHit()
	{
		state = CharacterState.stunned;
		unlockTimer = stunTime;
		actionLock = true;
		//TODO start stun animation
	}
}
