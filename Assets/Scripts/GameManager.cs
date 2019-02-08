using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    private int player1Score;
    private int player2Score;

    [SerializeField]
    private float timeLeft;

    [SerializeField]
    private Text player1ScoreText;

    [SerializeField]
    private Text player2ScoreText;

    [SerializeField]
    private Text timer;

    public bool gameStarted = false;

    private bool secondsLeft15;

    private bool secondsLeft5;

    private bool lowThresholdReached;

    private bool mediumThresholdReached;

    private bool highThresholdReached;

    private float timeWithoutHit;

    private int abundantToppings;

    public GameObject endGamePanel;

    public static GameManager manager;

    private void Awake()
    {
        manager = this;
    }

    public void StartGame()
    {
        timeLeft = 30f;

        gameStarted = true;
        secondsLeft15 = false;
        secondsLeft5 = false;
        lowThresholdReached = false;
        mediumThresholdReached = false;
        highThresholdReached = false;
        timeWithoutHit = 0f;
        
        AudioManager.instance.Play("Click");
        AudioManager.instance.Stop("Title Music");
        AudioManager.instance.Play("Game Music");
        AudioManager.instance.Play("Beginning of Game");

        Cursor.visible = false; //makes mouse cursor invisible on start of game

        foreach (PlayerBehaviour p in FindObjectsOfType<PlayerBehaviour>())
        {
            p.resetPosition();
        }

        foreach (CharacterBehaviour c in FindObjectsOfType<CharacterBehaviour>())
        {
            c.resetCharacter();
        }
    }

    public void EndGame() 
    {
        Cursor.visible = true;
        gameStarted = false;
        endGamePanel.SetActive(true);
    }

    public void ResetGame()
    {
        AudioManager.instance.Stop("Results Music");
        player1Score = 0;
        player2Score = 0;

        endGamePanel.SetActive(false);
        StartGame();
    }


    public void setPlayer1Score(int score)
    {
        player1Score = score;
        timeWithoutHit = 0f;
    }

    public int getPlayer1Score()
    {
        return player1Score;
    }

    public void setPlayer2Score(int score)
    {
        player2Score = score;
        timeWithoutHit = 0f;
    }

    public int getPlayer2Score()
    {
        return player2Score;
    }

    // Update is called once per frame
    void Update () 
    {
        if (gameStarted)
        {
            timeLeft -= Time.deltaTime;
            timer.text = ((int)timeLeft).ToString();

            player1ScoreText.text = "Player 1: " + player1Score.ToString();
            player2ScoreText.text = "Player 2: " + player2Score.ToString();

            if (timeLeft <= 15f && !secondsLeft15)
            {
                AudioManager.instance.Play("15 Seconds Left");
                secondsLeft15 = true;
            }

            if (timeLeft <= 5f && !secondsLeft5)
            {
                AudioManager.instance.Play("5 Seconds Left");
                secondsLeft5 = true;
            }

            if ((player1Score > 1000f || player2Score > 1000f) && !lowThresholdReached)
            {
                AudioManager.instance.Play("Score Threshold Low");
                lowThresholdReached = true;
            }

            if ((player1Score > 2000f || player2Score > 2000f) && !mediumThresholdReached)
            {
                AudioManager.instance.Play("Score Threshold Medium");
                mediumThresholdReached = true;
            }

            if ((player1Score > 5000f || player2Score > 5000f) && !highThresholdReached)
            {
                AudioManager.instance.Play("Score Threshold High");
                highThresholdReached = true;
            }

            timeWithoutHit += Time.deltaTime;
            if (timeWithoutHit > 5f)
            {
                AudioManager.instance.Play("No Pizzas Hit");
                timeWithoutHit = 0f;
            }

            if (timeLeft <= 0)
            {
                AudioManager.instance.Stop("Game Music");
                AudioManager.instance.Play("Results Music");
                AudioManager.instance.Play("End of Game");
                EndGame();
            }
        }
	}

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float GetSecondsLeft()
    {
        return timeLeft;
    }
}
