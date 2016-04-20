using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : Pauseable {

    private static GameController _gameController;
    public static GameController gameController
    {
        set
        {
            _gameController = value;
        }
        get
        {
            if (!_gameController)
                new GameObject().AddComponent<GameController>();
            return _gameController;
        }
    }

    public float GameTime = 180;
    private bool paused;
    public Player[] players;
    public Text Player1, Player2, timeLeft;
    [HideInInspector]
    public int player1Score = 0, player2Score = 0;


    // Use this for initialization
    void Start () {
        players = getPlayerArray();

	}
	
	// Update is called once per frame
	void Update () {
        GameTime -= Time.deltaTime;
	}


    private void matchStart()
    {
        Invoke("endGame", GameTime);
    }

    private void endGame()
    {
        if (player1Score > player2Score)
        {
            PlayerPrefs.SetInt("Winner", 0);
            PlayerPrefs.SetInt("Score", player1Score);
        }
        else
        {
            PlayerPrefs.SetInt("Winner", 1);
            PlayerPrefs.SetInt("Score", player2Score);
        }
        

            //TODO: Put in SceneStuff.
    }

    private void UI_Update()
    {
        Player1.text = "Player 1:" + player1Score;
        Player2.text = "Player 2:" + player2Score;
        timeLeft.text = "Time :" + (int)GameTime;
    }

    private void ErasePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void addScore(int player, int score)
    {
        if(player == 0)
        {
            _gameController.player1Score += score;
        }
        else
            _gameController.player2Score += score;
    }

    private Player[] getPlayerArray()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Player");
        Player[] results = new Player[go.Length];
        for (int i = 0; i < go.Length; i++)
            results[i] = go[i].GetComponent<Player>();
        return results;
    }

}
