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

    public Image player1WinsImage;
    public Image player2WinsImage;

    public Text winnerText;

    void Awake()
    {
        if (payoff == null)
            payoff = this;
    }

    public void DetermineGameResults()
    {

        GetPlayersScores();
        StartCoroutine(SpawnPlayer1Boxes());
        StartCoroutine(SpawnPlayer2Boxes());

        Invoke("ResetScene", 10f);
    }

        private void ResetScene()
        {
        AudioManager.instance.Play("SplashMusic");
        GameManager.manager.ResetScene();
        }

    void GetPlayersScores()
    {
        boxQuotient2 = GameManager.manager.getPlayer2Score() / 200;
        boxQuotient = GameManager.manager.getPlayer1Score() / 200;
    }

    IEnumerator SpawnPlayer1Boxes()
    {

        if (boxQuotient > 20)
            boxQuotient = 20;

        float secondsToWait = (float)(4.1 / boxQuotient);

        Debug.Log("secondsToWait" + secondsToWait.ToString());

        Debug.Log("quotient 1: " + boxQuotient);


        for (int i = 0; i < boxQuotient; i++)
        {
            player1Pizzas[i].enabled = true;
            yield return new WaitForSeconds(secondsToWait);
        }

        if (GameManager.manager.getPlayer1Score() > GameManager.manager.getPlayer2Score())
        {
            player1WinsImage.enabled = true;
            winnerText.text = "Player 1 Wins!";
        }
    }

    IEnumerator SpawnPlayer2Boxes()
    {
        if (boxQuotient2 > 20)
            boxQuotient2 = 20;


        float secondsToWait = (float)(4.0 / boxQuotient2);

        Debug.Log("quotient 2: " + boxQuotient2);


        for (int i = 0; i < boxQuotient2; i++)
        {
            player2Pizzas[i].enabled = true;
            yield return new WaitForSeconds(secondsToWait);
        }

        if (GameManager.manager.getPlayer2Score() > GameManager.manager.getPlayer1Score())
        {
            player2WinsImage.enabled = true;
            winnerText.text = "Player 2 Wins!";

        }
    }
}

