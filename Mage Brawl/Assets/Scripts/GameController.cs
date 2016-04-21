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
    public AudioClip getReady, gameEnding;


    // Use this for initialization
    void Start () {
        players = getPlayerArray();
        players[0].playerNumber = 0;
        players[1].playerNumber = 1;
        matchStart();

	}
	
	// Update is called once per frame
	void Update () {
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                pauseGame(false);
            //TODO: pause hud
        }
        else {
            GameTime -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
                pauseGame(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneController.Quit();
        
	}


    IEnumerator matchStart()
    {
        AudioSource src = this.gameObject.AddComponent<AudioSource>();
        src.clip = getReady;
        src.Play();
        Pauseable.pauseGame(true);
        yield return new WaitForSeconds(src.clip.length);
        Invoke("endGame", GameTime);
    }

    IEnumerator endGame()
    {
        AudioSource src = this.gameObject.AddComponent<AudioSource>();
        src.clip = gameEnding;
        src.Play();
        GameTime += src.clip.length;
        yield return new WaitForSeconds(src.clip.length);
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

        SceneController.targetScene("WinScene");
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
