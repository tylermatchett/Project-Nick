using UnityEngine;
using System.Collections;

public class MatchController : MonoBehaviour {

    private CharacterManager characterManager;
    private GameManager gameManager;

    private int index = 0;
    public float gameTimer = 60f;
    public string playerWinner = null;
    public int round = 1;

    public System.Action OnRoundOver;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        gameTimer -= Time.deltaTime;

        if(gameTimer <= 0)
        {
            if(gameManager.Players[0].Health > gameManager.Players[1].Health)
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
        for(int i = 0; i < gameManager.Players.Count; i++)
        {
            if (gameManager.Players[i].Health <= 0)
            {
                index = i;
                return (true);
            }
        }

        return (false);
    }
}
