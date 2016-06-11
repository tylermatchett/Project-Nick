using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class MatchController : MonoBehaviour {

    private CharacterManager characterManager;
    public Image firstIcon1;
    public Image firstIcon2;
    public Image secondIcon1;
    public Image secondIcon2;
    public Sprite swapIcon;
    public Text winner;
    public GameObject KO;

    bool win = false;
    private float timer = 0f;
    private int index = 0;
    public float gameTimer = 60f;
    public string playerWinner = null;
    public int round = 1;
    private int player1win = 0;
    private int player2win = 0;
    bool roundOver = false;

    public System.Action OnRoundOver;
	
	void Start ()
    {
		List<GameObject> goList = GameObject.FindGameObjectsWithTag("Player").ToList<GameObject>();
		for ( int i = 0; i < goList.Count(); i++ ) {
			CharacterManager cm = goList[i].GetComponent<CharacterManager>();
			if ( GameManager.Instance.Players.Count > i && cm != null ) {
				if( i == 0 )
					cm.player = GameManager.Instance.Players[1];
				else
					cm.player = GameManager.Instance.Players[0];
			}
		}

        SoundManager.Instance.Play(SoundType.GameMusic);
	}
	
	void Update ()
    {
        gameTimer -= Time.deltaTime;
        if(KO.activeSelf)
        {
            timer += Time.deltaTime;
        }

	    if(checkRoundOver() &&!roundOver)
        {
            switch(index)
            {
                case 0:

                    playerWinner = "Player2 Wins";
                    winner.text = playerWinner;
                    player2win++;
                    KO.SetActive(true);
                    round++;
                    roundOver = true;
                    break;

                case 1:

                    playerWinner = "Player1 Wins";
                    winner.text = playerWinner;
                    player1win++;
                    KO.SetActive(true);
                    round++;
                    roundOver = true;
                    break;
            }
            round++;
            if(player1win>0)
            {
                firstIcon1.sprite = swapIcon;
            }
            if(player1win>1)
            {
                secondIcon1.sprite = swapIcon;
            }
            if(player2win>0)
            {
                firstIcon2.sprite = swapIcon;
            }
            if(player2win>1)
            {
                secondIcon2.sprite = swapIcon;
            }
			if ( OnRoundOver != null )
				OnRoundOver();
		}
        if(timer>=3f)
        {
            Debug.Log(player1win + "," + player2win);
            if (player1win > 1 || player2win > 1)
            {
                Debug.Log("I Win");
                UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");
            }
            timer = 0f;
            GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0;i<players.Length;i++)
            {
                players[i].GetComponent<CharacterManager>().player.Health = 100;
                if(i < 1)
                {
                    players[1].transform.position = new Vector3(-11, -5, players[i].transform.position.z);
                }
                else
                {
                    players[0].transform.position = new Vector3(10, -5, players[i].transform.position.z);
                }
            }
			KO.SetActive(false);
            roundOver = false;
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
        if(gameTimer<=0)
        {
            return true;
        }
        return (false);
    }
}
