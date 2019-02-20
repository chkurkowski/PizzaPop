using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayoffPizzaScript : MonoBehaviour
{

    public GameObject pizzaBox;
    int boxQuotient = 0;
    int boxQuotient2 = 0;
    public static PayoffPizzaScript payoff;
    public Image[] player1Pizzas;
    public Image[] player2Pizzas;

    void Awake()
    {
        payoff = this;
    }

    public void DetermineGameResults()
    {
        GameManager.manager.setPlayer1Score(400);
        GameManager.manager.setPlayer2Score(400);
        GetPlayersScores();
        StartCoroutine(SpawnPlayer1Boxes());
        StartCoroutine(SpawnPlayer2Boxes());
    }

    void GetPlayersScores()
    {
        boxQuotient2 = GameManager.manager.getPlayer2Score() / 20;
        boxQuotient = GameManager.manager.getPlayer1Score() / 20;
    }

    IEnumerator SpawnPlayer1Boxes()
    {
        Debug.Log("quotient 1: " + boxQuotient);
        for (int i = 0; i < boxQuotient; i++)
        {
            player1Pizzas[i].enabled = true;
            yield return new WaitForSeconds(.3f);
        }
    }

    IEnumerator SpawnPlayer2Boxes()
    {
        Debug.Log("quotient 2: " + boxQuotient2);
        for (int i = 0; i < boxQuotient2; i++)
        {
            player2Pizzas[i].enabled = true;
            yield return new WaitForSeconds(.3f);
        }
    }
}
