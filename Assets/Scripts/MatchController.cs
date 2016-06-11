using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MatchController : MonoBehaviour {

    private CharacterManager characterManager;

    private int index = 0;
    public float gameTimer = 60f;
    public string playerWinner = null;
    public int round = 1;

    public System.Action OnRoundOver;
	
	void Start ()
    {
		List<GameObject> goList = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();
		for ( int i = 0; i < goList.Count(); i++ ) {
			goList[i].GetComponent<CharacterManager>().player = GameManager.Instance.Players[i];
		}
	}
	
	void Update ()
    {
        gameTimer -= Time.deltaTime;

        if(gameTimer <= 0)
        {
            if( GameManager.Instance.Players[0].Health > GameManager.Instance.Players[1].Health)
            {
                playerWinner = "Player1";
            }
            else
            {
                playerWinner = "Player2";
            }

            OnRoundOver();
        }

	    if(checkRoundOver())
        {
            switch(index)
            {
                case 0:

                    playerWinner = "Player1";
                    break;

                case 1:

                    playerWinner = "Player2";
                    break;
            }
            round++;
            OnRoundOver();
        }
	}

    bool checkRoundOver()
    {
        for(int i = 0; i < GameManager.Instance.Players.Count; i++)
        {
            if ( GameManager.Instance.Players[i].Health <= 0)
            {
                index = i;
                return (true);
            }
        }

        return (false);
    }
}
