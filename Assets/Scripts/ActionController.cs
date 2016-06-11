using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ActionController : MonoBehaviour
{
	public Animator anim;
	public CharacterManager characterManager;
	public System.Action OnAnimationEnd;
	public float blockAnimTimer;
	
    List<GameObject> goList;

	void Start()
    {
        goList = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();
    }

	void OnTriggerEnter2D(Collider2D target)
	{
		Debug.Log("Hit player");
		if (target.gameObject.tag == "PlayerHitBox")
		{
			// screen shake
			ActionController ac = target.gameObject.transform.root.GetComponent<ActionController>();
			
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
			}
		}
	}
	public void ApplyDamage(float damage)
	{
		anim.Play("OnHit");
		if (characterManager.state == CharacterState.blocking)
		{
			characterManager.player.Health -= damage * characterManager.blockReduction;
            SoundManager.Instance.Play(SoundType.HitBlock);
		}
		else
		{
			characterManager.player.Health -= damage;
            SoundManager.Instance.Play(SoundType.HitContact);
            if (goList[1] == gameObject)
            {
                SoundManager.Instance.Play(SoundType.HitCat);
            }
            else
            {
                SoundManager.Instance.Play(SoundType.HitDog);
            }
        }

	}
	public void Punch()
	{
        SoundManager.Instance.Play(SoundType.HitWhoosh);
        anim.speed = 1.5f;
		anim.Play("FarPunch");
		Invoke("animationend", 0.75f/anim.speed);
    }
	public void HeavyPunch()
	{
        SoundManager.Instance.Play(SoundType.HitWhoosh);
        anim.speed = 0.75f;
		anim.Play("NearPunch");
		Invoke("animationend", 0.75f/anim.speed);
    }
	public void Kick()
	{
        SoundManager.Instance.Play(SoundType.HitWhoosh);
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
