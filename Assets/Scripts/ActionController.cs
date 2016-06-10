using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

    private CharacterManager characterManager;

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            if(characterManager.state == characterState.Attack)
            {
             target.gameObject.GetComponent<ActionController>().ApplyDamage(characterManager.damage);
            }
        }
    }
    public void ApplyDamage(float damage)
    {
        if(characterManager.state == characterState.Block)
        {
            characterManager.hp -= damage * characterManager.damageMod;
        }
        else
        {
            characterManager.hp -= damage;
        }
        
    }
}
