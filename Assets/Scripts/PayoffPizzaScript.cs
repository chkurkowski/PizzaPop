using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public GameObject RossoWinsText;
    public GameObject VerdeWinsText;

    public TextMeshProUGUI player1ScoreDisplayFill;
    public TextMeshProUGUI player1ScoreDisplayOutline;

    public TextMeshProUGUI player2ScoreDisplayFill;
    public TextMeshProUGUI player2ScoreDisplayOutline;

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

        //private void ResetScene()
        //{
        //AudioManager.instance.Play("SplashMusic");
        //GameManager.manager.ResetScene();
        //}

    void GetPlayersScores()
    {
        boxQuotient2 = GameManager.manager.getPlayer2Score() / 200;
        boxQuotient = GameManager.manager.getPlayer1Score() / 200;
    }

    IEnumerator SpawnPlayer1Boxes()
    {

        if (boxQuotient > 19)
            boxQuotient = 19;
        else if (boxQuotient < 1)
            boxQuotient = 1;

        float secondsToWait = (float)(3.9f / boxQuotient);

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
            //winnerText.text = "Player 1 Wins!";
            RossoWinsText.SetActive(true);

      

            if (boxQuotient == boxQuotient2 && GameManager.manager.getPlayer1Score() > GameManager.manager.getPlayer2Score())
            {
                player1Pizzas[19].enabled = true;
            }
        }

        player1ScoreDisplayFill.text = GameManager.manager.getPlayer1Score().ToString();
        player1ScoreDisplayOutline.text = GameManager.manager.getPlayer1Score().ToString();
    }

    IEnumerator SpawnPlayer2Boxes()
    {
        if (boxQuotient2 > 19)
            boxQuotient2 = 19;
        else if (boxQuotient2 < 1)
            boxQuotient2 = 1;


        float secondsToWait = (float)(3.9f / boxQuotient2);

        Debug.Log("quotient 2: " + boxQuotient2);


        for (int i = 0; i < boxQuotient2; i++)
        {
            player2Pizzas[i].enabled = true;
            yield return new WaitForSeconds(secondsToWait);
        }

        if (GameManager.manager.getPlayer2Score() > GameManager.manager.getPlayer1Score())
        {
            player2WinsImage.enabled = true;
            VerdeWinsText.SetActive(true);

            if (boxQuotient == boxQuotient2 && GameManager.manager.getPlayer2Score() > GameManager.manager.getPlayer1Score())
            {
                player2Pizzas[19].enabled = true;
            }
        }

        player2ScoreDisplayFill.text = GameManager.manager.getPlayer2Score().ToString();
        player2ScoreDisplayOutline.text = GameManager.manager.getPlayer2Score().ToString();
    }


}

