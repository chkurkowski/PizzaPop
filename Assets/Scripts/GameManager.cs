using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [HideInInspector]
    public bool gameStarted = false;

    public GameObject endGamePanel;

    public static GameManager manager;

    private void Awake()
    {
        manager = this;
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    public void EndGame()
    {
        gameStarted = false;
        endGamePanel.SetActive(true);
    }

    public void setPlayerScore(int whichPlayer, int score)
    {
        if (whichPlayer == 1)
            player1Score = score;
        else
            player2Score = score;
    }

    public int getPlayerScore(int whichPlayer)
    {
        if (whichPlayer == 1)
            return player1Score;
        else
            return player2Score;
    }

    //public void setPlayer1Score(int score)
    //{
    //    player1Score = score;
    //}

    //public int getPlayer1Score()
    //{
    //    return player1Score;
    //}

    //public void setPlayer2Score(int score)
    //{
    //    player2Score = score;
    //}

    //public int getPlayer2Score()
    //{
    //    return player2Score;
    //}



    // Update is called once per frame
    void Update () 
    {
        if (gameStarted)
        {
            timeLeft -= Time.deltaTime;
            timer.text = ((int)timeLeft).ToString();

            player1ScoreText.text = "Player 1: " + player1Score.ToString();
            player2ScoreText.text = "Player 2: " + player2Score.ToString();

            if (timeLeft <= 0)
                EndGame();
        }
	}
}
