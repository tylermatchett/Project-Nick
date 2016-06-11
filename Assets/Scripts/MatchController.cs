using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class MatchController : MonoBehaviour {

    private CharacterManager characterManager;
    public Text winner;
    public GameObject KO;

    private float timer = 0f;
    private int index = 0;
    public float gameTimer = 60f;
    public string playerWinner = null;
    public int round = 1;
    private int player1win = 0;
    private int player2win = 0;

    public System.Action OnRoundOver;
	
	void Start ()
    {
		List<GameObject> goList = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();
		for ( int i = 0; i < goList.Count(); i++ ) {
			CharacterManager cm = goList[i].GetComponent<CharacterManager>();
			if ( GameManager.Instance.Players.Count > i && cm != null ) {
				cm.player = GameManager.Instance.Players[i];
			}
		}


	}
	
	void Update ()
    {
        gameTimer -= Time.deltaTime;
        if(KO.activeSelf)
        {
            timer += Time.deltaTime;
        }

        if(gameTimer <= 0)
        {
            if( GameManager.Instance.Players[0].Health > GameManager.Instance.Players[1].Health)
            {
                playerWinner = "Player1 Wins";
                winner.text = playerWinner;
                player1win++;
                KO.SetActive(true);
            }
            else
            {
                playerWinner = "Player2 Wins";
                winner.text = playerWinner;
                player2win++;
                KO.SetActive(true);
            }

			if (OnRoundOver != null)
				OnRoundOver();
        }

	    if(checkRoundOver())
        {
            switch(index)
            {
                case 0:

                    playerWinner = "Player1 Wins";
                    winner.text = playerWinner;
                    player1win++;
                    KO.SetActive(true);
                    break;

                case 1:

                    playerWinner = "Player2 Wins";
                    winner.text = playerWinner;
                    player2win++;
                    KO.SetActive(true);
                    break;
            }
            round++;

			if ( OnRoundOver != null )
				OnRoundOver();
		}
        if(timer>=3f)
        {
            if (player1win == 2 || player2win == 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");
            }
            timer = 0f;
            GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0;i<players.Length;i++)
            {
                players[i].GetComponent<CharacterManager>().player.Health = 100;
                if(i < 1)
                {
                    players[i].transform.position = new Vector3(-11, -5, players[i].transform.position.z);
                }
                else
                {
                    players[i].transform.position = new Vector3(10, -5, players[i].transform.position.z);
                }
            }
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
