using System.Collections.Generic;
using UnityEngine;

public class PizzaBehaviour : MonoBehaviour, iPoolerObject
{
    private ToppingSwitcher _toppingSwitcher;
    private GameManager _manager;
    private List<GameObject> toppings;
    private PizzaOrder _pizzaOrder;
    private ParticleSystem _particles;


    private float fallMultiplier;
    private float riseMultiplier;

    public enum PizzaSizes
    {
        Large,
        Medium,
        Small
    }

    public PizzaSizes pizzaSize;

    private void Awake()
    {
        _pizzaOrder = GetComponent<PizzaOrder>();
        _toppingSwitcher = GameObject.Find("ToppingSwitcher").GetComponent<ToppingSwitcher>();
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        toppings = new List<GameObject>();
        _particles = GetComponent<ParticleSystem>();
    }

    //private void OnMouseDown()
    //{
    //    Player1Hits();
    //}

    private void Player1Hits()
    {
        int scoreToAdd = 0;

        if (pizzaSize == PizzaSizes.Small)
        {
            scoreToAdd = 500;
        }
        else if (pizzaSize == PizzaSizes.Medium)
        {
            scoreToAdd = 200;
        }
        else if (pizzaSize == PizzaSizes.Large)
        {
            scoreToAdd = 100;
        }

        if (_toppingSwitcher.GetPlayer1Topping() == _pizzaOrder.GetToppingNeeded())
        {
            scoreToAdd *= 2;
        }

        _manager.setPlayer1Score(_manager.getPlayer1Score() + (scoreToAdd));
    }

    private void Player2Hits()
    {
        int scoreToAdd = 0;

        if (pizzaSize == PizzaSizes.Small)
        {
            scoreToAdd = 500;
        }
        else if (pizzaSize == PizzaSizes.Medium)
        {
            scoreToAdd = 200;
        }
        else if (pizzaSize == PizzaSizes.Large)
        {
            scoreToAdd = 100;
        }

        if (_toppingSwitcher.GetPlayer2Topping() == _pizzaOrder.GetToppingNeeded())
        {
            scoreToAdd *= 2;
        }

        _manager.setPlayer2Score(_manager.getPlayer2Score() + (scoreToAdd));
    }


    public void AddTopping(GameObject toppingToAdd)
    {
        //gets a string that tells us which topping to spawn onto it
        //string toppingName = _toppingSwitcher.getToppingObject((int)toppingAdded);

        //GameObject toppingToAdd = ObjectPooler.instance.SpawnFromPool(toppingName, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f), Quaternion.identity);

        if (toppingToAdd.GetComponent<ToppingScript>().playerShooter == PlayerBehaviour.Players.Player1)
        {
            Player1Hits();
        }
        else if (toppingToAdd.GetComponent<ToppingScript>().playerShooter == PlayerBehaviour.Players.Player2)
        {
            Player2Hits();
        }

        toppings.Add(toppingToAdd);
        toppingToAdd.transform.parent = transform;
    }

    public void RemoveToppings()
    {
        if(toppings != null)
        {
            foreach (GameObject topping in toppings)
            {
                topping.transform.parent = null;
                topping.SetActive(false);
            }
            toppings.Clear();
        }
    }

    public void OnSpawnedByPooler()
    {
        _pizzaOrder.AssignRandomTopping();
    }

    private void Update()
    {
        //disable a pizza when it leaves the screen
        if (transform.position.y < -7.0f)
        {
            RemoveToppings();
            gameObject.SetActive(false);
        }

        //pizza phsyics

        if (GetComponent<Rigidbody2D>().velocity.y < 0.0f)
        {
            GetComponent<Rigidbody2D>().velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (GetComponent<Rigidbody2D>().velocity.y > 0.0f)
        {
            GetComponent<Rigidbody2D>().velocity += Physics2D.gravity * (riseMultiplier - 1) * Time.deltaTime;
        }
    }
}