using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    public Text player1Score;
    public Text player2Score;

    private void OnEnable()
    {
        PayoffPizzaScript.payoff.DetermineGameResults();
        player1Score.text = "Player 1 Score: " + GameManager.manager.getPlayer1Score().ToString();
        player2Score.text = "Player 2 Score: " + GameManager.manager.getPlayer2Score().ToString();
        //Debug.Log(PayoffPizzaScript.payoff == null);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
