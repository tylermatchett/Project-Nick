using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

    public Animator anim;
    public CharacterManager characterManager;

    void OnCollisionEnter2D(Collision2D target)
    {
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
        }
        else
        {
            anim.Play("NearPunch");
        }
   
    }
    public void Kick()
    {
        if(Random.Range(0,2)>0)
        {
            anim.Play("FarKick");
        }
        else
        {
            anim.Play("NearKick");
        }
       
    }
    public void Block()
    {
        anim.Play("Block");
    }
    public void Jump()
    {
        anim.Play("Jump");
    }

	void Update() {
		transform.position += new Vector3(characterManager.direction.x, transform.position.y, transform.position.z) * characterManager.moveSpeed;
	}
}
