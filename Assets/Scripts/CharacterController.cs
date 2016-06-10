using UnityEngine;
using System.Collections;
//using incontroll;

public class CharacterController : MonoBehaviour {
	public float health;
	public float moveSpeed;
	public float kickDamage;
	public float punchDamage;
	public float blockReduction;

	public enum characterState
	{
		idle,
		moving,
    blocking,
    punching,
    kicking
	};

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
