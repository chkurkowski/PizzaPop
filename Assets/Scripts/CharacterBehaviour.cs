using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{

    public ScriptedPizzaEvent[] events;
    
    public float rightLimitX;
    public float leftLimitX;
    public float pizzaSpawnRate;

    public float characterSpeed;

    private float direction = 1.0f;

    private bool spawningStarted = false;

    private void changeDirection()
    {
        direction *= -1.0f;
    }

    public void resetCharacter()
    {
        spawningStarted = false;
    }

	// Update is called once per frame
	void Update () 
    {
        if (GameManager.manager.gameStarted)
        {
            MoveLeftAndRight();

            if (!spawningStarted)
            {
                spawningStarted = true;
                StartCoroutine(spawnPizza());
            }
        }
	}

    private void MoveLeftAndRight()
    {
        transform.Translate(new Vector3(characterSpeed * Time.deltaTime * direction, 0f, 0f));

        if (transform.position.x > rightLimitX)
        {
            transform.position = new Vector3(rightLimitX, transform.position.y, transform.position.z);
            changeDirection();
        }
        else if (transform.position.x < leftLimitX)
        {
            transform.position = new Vector3(leftLimitX, transform.position.y, transform.position.z);
            changeDirection();
        }
    }

    private IEnumerator spawnPizza()
    {
        while (GameManager.manager.gameStarted)
        {
            yield return new WaitForSeconds(pizzaSpawnRate);
            //events[3].launch();

            float percentage = Random.Range(0f, 1f);
            //every 1 in 10 

            if (percentage <= 0.2f)
            {
                //event triggered every 1 in 10
                int randomNum = Random.Range(0, 3);
                events[randomNum].launch();

            }
            else
            {

                GameObject pizza = ObjectPooler.instance.SpawnFromPool(randomPizza(), transform.position, Quaternion.identity);
                pizza.GetComponent<PizzaBehaviour>().RemoveToppings();
                pizza.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                pizza.GetComponent<Rigidbody2D>().AddForce(randomPush(), ForceMode2D.Impulse);
            }

            if (GameManager.manager.GetSecondsLeft() <= 30.0f)
            {
               // AudioManager.instance.Play("Make Pizza");
                pizzaSpawnRate = 1.5f;
            }
             if (GameManager.manager.GetSecondsLeft() <= 20.0f)
            {
                //AudioManager.instance.Play("stopSittingOnHands");
                pizzaSpawnRate = 1.0f;
            }
             if (GameManager.manager.GetSecondsLeft() <= 10.0f)
            {
                //AudioManager.instance.Play("GetToppingsReady");
                pizzaSpawnRate = 0.5f;
            }
        }
    }

    private string randomPizza()
    {
        float randomNum = Random.Range(0f, 1.0f);

        if (randomNum <= 0.5f)
            return "LargePizza";
        else if (randomNum <= 0.8f)
            return "MediumPizza";
        else
            return "SmallPizza";
    }

    Vector2 randomPush()
    {
        float distanceFromCenter = Mathf.Abs(transform.position.x);
        float xforce = 0.0f;

        int randomNum = Random.Range(0, 3);

        if (randomNum == 0)
        {
            //push straight up
            xforce = 0.0f;
        }
        else if (distanceFromCenter >= 5.0f)
        {
            //either push mildly or harshly towards the center
            if (randomNum == 2)
            {
                xforce = 2.5f;
            }
            else
            {
                xforce = 5.0f;
            }

            xforce *= Mathf.Sign(transform.position.x) * -1.0f; 
        }
        else
        {
            //Push randomly left or right
            if (randomNum == 2)
            {
                xforce = Random.Range(-2.5f, -5.0f);
            }
            else
            {
                xforce = Random.Range(2.5f, 5.0f);
            }
        }

        return new Vector2(xforce, Random.Range(8.0f, 10.0f));
    }
}
