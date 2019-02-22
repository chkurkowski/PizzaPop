using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayoffPizzaScript : MonoBehaviour {

    public GameObject pizzaBox;
    int boxQuotient = 0;
    public static PayoffPizzaScript payoff;


    void Awake()
    {
        payoff = this;
    }

    public void DetermineGameResults()
    {
        GetPlayersScores();
        StartCoroutine(SpawnPizzaBoxes());
    }

    void GetPlayersScores()
    {
        if (GameManager.manager.getPlayer2Score() > GameManager.manager.getPlayer1Score())
        {
            boxQuotient = GameManager.manager.getPlayer2Score() / 20;
        }

        if (GameManager.manager.getPlayer2Score() < GameManager.manager.getPlayer1Score())
        {
            boxQuotient = GameManager.manager.getPlayer1Score() / 20;
        }
    }

    IEnumerator SpawnPizzaBoxes()
    {
        Debug.Log(boxQuotient);
        for (int i = 1; i < boxQuotient; i++)
        {
            Vector2 newPizza = new Vector2(Random.Range(-2, 3), transform.position.y + 10);
            ObjectPooler.instance.SpawnFromPool("PizzaBox", newPizza, Quaternion.identity);
            yield return new WaitForSeconds(.3f);
        }
    }

    
    /* player 1 score is 10000
     player 2 socre is 5999

    compare players' scores
    highest one / 20 = quotient

    in a coroutine:
        for how many times the quotient is
            spawn new pizza at random x offset, y + small amount, same rotation


    */
}
