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
		
	}
}
