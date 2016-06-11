using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeCharacter : MonoBehaviour {
	
	public SpriteRenderer head;
	public SpriteRenderer body;
	public SpriteRenderer farFist;
	public SpriteRenderer nearFist;
	public SpriteRenderer farLeg;
	public SpriteRenderer nearLeg;

	public Sprite doghead;
	public Sprite dogbody;
	public Sprite dogfarFist;
	public Sprite dognearFist;
	public Sprite dogfarLeg;
	public Sprite dognearLeg;

	void Start () {
		if ( !GetComponent<CharacterManager>().player.isCat ) {
			head.sprite = doghead;
			body.sprite = dogbody;
			farFist.sprite = dogfarFist;
			nearFist.sprite = dognearFist;
			farLeg.sprite = dogfarLeg;
			nearLeg.sprite = dognearLeg;
		}
	}
}
