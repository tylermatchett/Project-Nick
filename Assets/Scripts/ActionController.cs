﻿using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

    private CharacterManager characterManager;

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Player")
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
            characterManager.health -= damage * characterManager.blockReduction;
        }
        else
        {
            characterManager.health -= damage;
        }
        
    }
}
