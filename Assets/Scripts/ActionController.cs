using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

    public Animator anim;
    public CharacterManager characterManager;
	public System.Action OnAnimationEnd;

	void OnCollisionEnter2D(Collision2D target)
    {
		Debug.Log("Hit player");
        if(target.gameObject.tag == "PlayerHitBox")
        {
            if(characterManager.state == CharacterState.punching)
            {
				target.gameObject.GetComponent<ActionController>().ApplyDamage(characterManager.punchDamage);
            }
            else if(characterManager.state == CharacterState.kicking)
            {
                target.gameObject.GetComponent<ActionController>().ApplyDamage(characterManager.kickDamage);
            }
        }
    }
    public void ApplyDamage(float damage)
    {
		Debug.Log("I'm hit broski");
        if(characterManager.state == CharacterState.blocking)
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
        if(Random.Range(0,2)>0)
        {
            anim.Play("FarPunch");
			Invoke("animationend", 0.75f);
		}
        else
        {
            anim.Play("NearPunch");
			Invoke("animationend", 0.75f);
		}
   
    }
    public void Kick()
    {
        if(Random.Range(0,2)>0)
        {
            anim.Play("FarKick");
			Invoke("animationend", 0.75f);
		}
        else
        {
            anim.Play("NearKick");
			Invoke("animationend", 0.75f);
		}
       
    }
    public void Block()
    {
        anim.Play("Block");
		Invoke("animationend", 0.75f);
	}
    public void Jump()
    {
        anim.Play("Jump");
		Invoke("animationend", 0.75f);
    }

	void Update() {
		transform.position += new Vector3(characterManager.direction.x, 0f, 0f) * characterManager.moveSpeed * Time.deltaTime;
	}
	void animationend() {
		OnAnimationEnd();
	}
}
