using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{

    //move character left and right
    //have them toss up pizzas 
    
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
            GameObject pizza = ObjectPooler.instance.SpawnFromPool(randomPizza(), transform.position, Quaternion.identity);
            pizza.GetComponent<PizzaBehaviour>().RemoveToppings();

            pizza.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            pizza.GetComponent<Rigidbody2D>().AddForce(randomPush(), ForceMode2D.Impulse);
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
        return new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(8.0f, 10.0f));
    }
}
