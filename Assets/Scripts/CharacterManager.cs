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
	public float moveSpeed = 5f;
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
	GameObject otherPlayer;
	public bool leftPlayer;
	
	void Start() {
		actionController.OnAnimationEnd += OnAnimationEnd;

		//This breaks with more than two players
		otherPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
		if(otherPlayer == gameObject)
		{
			otherPlayer = GameObject.FindGameObjectsWithTag("Player")[1];
		}

		if(transform.position.x > otherPlayer.transform.position.x)
		{
			leftPlayer = false;
		}
		else
		{
			leftPlayer = true;
		}
	}

	void Update ()
	{
		if (player != null)
			handleInput();

		if(unlockTimer < 0 && actionLock)
		{
			actionLock = false;
		}
  }

	void OnAnimationEnd() {
		state = CharacterState.idle;
	}

	private void handleInput()
	{
		Debug.Log("Player is null: " + (player == null));
		if ( player.Device.Action3 && state <= CharacterState.blocking)
		{
			state = CharacterState.punching;
			actionController.Punch();
			unlockTimer = punchTime;
			actionLock = true;
		}
		else if ( player.Device.Action4 && state <= CharacterState.blocking)
		{
			state = CharacterState.kicking;
			actionController.Kick();
			unlockTimer = kickTime;
			actionLock = true;
		}
		else if ( player.Device.Action1 && state <= CharacterState.blocking)
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
			float moveVal = 0f;
			if (leftPlayer)
			{
				moveVal = 1f;
			}
			if (Mathf.Abs(player.Device.LeftStickX.Value) > 0.15f
				&& (Vector3.Distance(transform.position, otherPlayer.transform.position) > 6f
				|| leftPlayer && player.Device.LeftStickX.Value < 0
				|| player.Device.LeftStickX.Value > 0))
			{
				state = CharacterState.moving;
				direction = new Vector2(player.Device.LeftStickX.Value, 0f);
			}
			else
			{
				direction = Vector2.zero;
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
