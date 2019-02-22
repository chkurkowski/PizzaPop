﻿using System.Collections;
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

    [HideInInspector]
    public bool gameStarted = false;

    public GameObject endGamePanel;

    public static GameManager manager;

    private UIManager UIManagerScript;

    private void Awake()
    {
        manager = this;
    }

    private void Start()
    {
        UIManagerScript = GameObject.Find("GameCanvas").GetComponent<UIManager>();
    }

    public void StartGame()
    {
        timeLeft = 30f; // CHANGE THIS BACK TO 30

        gameStarted = true;
        
        AudioManager.instance.Play("Click");
        AudioManager.instance.Stop("SplashMusic");
        AudioManager.instance.Play("Gameplay Music");

       // Cursor.visible = false; //makes mouse cursor invisible on start of game

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
        UIManagerScript.onPayoffScreen = true;
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
    }

    public int getPlayer1Score()
    {
        return player1Score;
    }

    public void setPlayer2Score(int score)
    {
        player2Score = score;
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

            if (timeLeft <= 0)
            {
                AudioManager.instance.Stop("Game Music");
                AudioManager.instance.Play("End Game Chime");
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
