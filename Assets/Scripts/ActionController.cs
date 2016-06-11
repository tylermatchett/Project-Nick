using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour
{
	public Animator anim;
	public CharacterManager characterManager;
	public System.Action OnAnimationEnd;
	public float blockAnimTimer;

	void OnTriggerEnter2D(Collider2D target)
	{
		Debug.Log("Hit player");
		if (target.gameObject.tag == "PlayerHitBox")
		{
			ActionController ac = target.gameObject.transform.root.GetComponent<ActionController>();

			Debug.Log("Found hit area | ac:  " + ac);
			if (ac != null)
			{
				if (characterManager.state == CharacterState.punching)
				{
					ac.ApplyDamage(characterManager.punchDamage);
				}
				else if (characterManager.state == CharacterState.kicking)
				{
					ac.ApplyDamage(characterManager.kickDamage);
				}
				else if (characterManager.state == CharacterState.heavyPunching)
				{
					ac.ApplyDamage(characterManager.heavyPunchDamage);
				}
				Debug.Log("found AC");
			}
		}
	}
	public void ApplyDamage(float damage)
	{
		Debug.Log("I'm hit broski");
		if (characterManager.state == CharacterState.blocking)
		{
			characterManager.player.Health -= damage * characterManager.blockReduction;
		}
		else
		{
			characterManager.player.Health -= damage;
		}

	}
	public void Punch()
	{
		anim.speed = 1.5f;
		anim.Play("FarPunch");
		Invoke("animationend", 0.75f/anim.speed);
	}
	public void HeavyPunch()
	{
		anim.speed = 0.75f;
		anim.Play("NearPunch");
		Invoke("animationend", 0.75f/anim.speed);
	}
	public void Kick()
	{
		if (Random.Range(0, 2) > 0)
		{
			anim.Play("FarKick");
		}
		else
		{
			anim.Play("NearKick");
		}
		Invoke("animationend", 0.75f / anim.speed);

	}
	public void Block()
	{
		//anim.speed = 2.5f;
		//anim.Play("Block");
		//blockAnimTimer = 0;
		//Invoke("animationend", 0.75f / anim.speed);
	}
	public void Jump()
	{
		anim.Play("Jump");
		Invoke("animationend", 0.75f / anim.speed);
	}

	void Update()
	{
		transform.position += new Vector3(characterManager.direction.x, 0f, 0f) * characterManager.moveSpeed * Time.deltaTime;

		if(characterManager.state == CharacterState.blocking)
		{
			blockAnimTimer += Time.deltaTime;

			if (blockAnimTimer >= 0.5)
			{
//				anim.Play("Blocking");
			}
		}
	}
	void animationend()
	{
		anim.speed = 1f;
		OnAnimationEnd();
	}
}
