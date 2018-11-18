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
    private Text timer;



    public void setPlayer1Score(int score)
    {
        player1Score = score;
    }

    public int getPlayer1Score()
    {
        return player1Score;
    }

	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        timeLeft -= Time.deltaTime;
        Debug.Log((int)timeLeft);
	}
}
