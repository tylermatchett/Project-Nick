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
	heavyPunching,
	kicking,
	stunned
};

public class CharacterManager : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float kickDamage;
	public float punchDamage;
	public float heavyPunchDamage;
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
	public int debugButton;
	public bool debugMode = false;

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

		if ( state == CharacterState.blocking )
			actionController.anim.SetBool("Guarding", true);
		else
			actionController.anim.SetBool("Guarding", false);
	}

	void OnAnimationEnd() {
		state = CharacterState.idle;
	}

	private void handleInput()
	{
		if(debugButton != 0)
		{
			Debug.Log("Button not zero");
		}
		//Debug.Log("Player is null: " + (player == null));
		if ((Input.GetKeyDown(KeyCode.X) || !debugMode && player.Device.Action3) && state <= CharacterState.blocking)
		{
			state = CharacterState.punching;
			actionController.Punch();
			unlockTimer = punchTime;
			actionLock = true;
			debugButton = 0;
		}
		else if ((Input.GetKeyDown(KeyCode.S) || !debugMode && player.Device.Action4) && state <= CharacterState.blocking)
		{
			state = CharacterState.kicking;
			actionController.Kick();
			unlockTimer = kickTime;
			actionLock = true;
			debugButton = 0;
		} else if ( (Input.GetKeyDown(KeyCode.D) || !debugMode && player.Device.Action1) && state <= CharacterState.blocking ) {
			state = CharacterState.blocking;
			actionController.Block();
			debugButton = 0;
		} else if ((Input.GetKeyDown(KeyCode.C) || !debugMode && player.Device.Action2) && state <= CharacterState.blocking)
		{
			state = CharacterState.heavyPunching;
			actionController.HeavyPunch();
			unlockTimer = punchTime;
			actionLock = true;
			debugButton = 0;
    }
		//*/
		else if (state <= CharacterState.moving)
		{
			if (Mathf.Abs(player.Device.LeftStickX.Value) > 0.15f
				&& (Mathf.Abs(Vector3.Distance(transform.position, otherPlayer.transform.position)) > 2f
				|| leftPlayer && player.Device.LeftStickX.Value < 0
				|| !leftPlayer && player.Device.LeftStickX.Value > 0))
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
		if (state != CharacterState.moving) {
			direction = Vector2.zero;
		}
		if ( player.Device.Action1.WasReleased && state <= CharacterState.blocking )
			state = CharacterState.idle;
		
	}

	public void onHit()
	{
		state = CharacterState.stunned;
		unlockTimer = stunTime;
		actionLock = true;
		//TODO start stun animation
	}
}
