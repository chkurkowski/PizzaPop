using System.Collections.Generic;
using UnityEngine;

public class PizzaBehaviour : MonoBehaviour, iPoolerObject
{
    private ToppingSwitcher _toppingSwitcher;
    private GameManager _manager;
    private List<GameObject> toppings;
    private PizzaOrder _pizzaOrder;

<<<<<<< Updated upstream
    [SerializeField]
    private int pizzaLife;

=======
>>>>>>> Stashed changes

    [SerializeField]
    private float riseMultiplier = 1.0f;

    [SerializeField]
    private float fallMultiplier = 1.0f;

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
    }

    //private void OnMouseDown()
    //{
    //    Player1Hits();
    //}

    private void PlayerHits(int whichPlayer)
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

        if (_toppingSwitcher.GetPlayerTopping(whichPlayer) == _pizzaOrder.GetToppingNeeded())
        {
            scoreToAdd *= 2;

            pizzaLife--;

            if (pizzaLife <= 0)
            {
                Pop();
            }
        }

        int currentplayerScore = _manager.getPlayerScore(whichPlayer);

        _manager.setPlayerScore(whichPlayer, currentplayerScore + scoreToAdd);
                                           
    }


    public void AddTopping(GameObject toppingToAdd)
    {
        if (toppingToAdd.GetComponent<ToppingScript>().playerShooter == PlayerBehaviour.Players.Player1)
        {
            PlayerHits(1);
        }
        else if (toppingToAdd.GetComponent<ToppingScript>().playerShooter == PlayerBehaviour.Players.Player2)
        {
            PlayerHits(2);
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

<<<<<<< Updated upstream
    }

    void Pop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 8.0f), ForceMode2D.Impulse);
=======
>>>>>>> Stashed changes
    }
}