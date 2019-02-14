using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaBehaviour : MonoBehaviour, iPoolerObject
{
    private ToppingSwitcher _toppingSwitcher;
    private GameManager _manager;
    private List<GameObject> toppings;
    private PizzaOrder _pizzaOrder;
    private ParticleSystem _particles;
    private ToppingScoreHandler _scoreHandler;

    [SerializeField]
    private int pizzaLife;

    private int scoreToAdd = 0;


    private float fallMultiplier = 1.0f;

    private float riseMultiplier = 1.0f;

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
        _scoreHandler = ToppingScoreHandler.instance;
    }

    //private void OnMouseDown()
    //{
    //    Player1Hits();
    //}

    private void PlayerHits(string player)
    {
        AudioManager.instance.Play("Hit");

        pizzaLife--;

        if (pizzaLife <= 0)
            Pop();

        if(player == "one")
        {
            _manager.setPlayer1Score(_manager.getPlayer1Score() + (scoreToAdd));
            DisplayScore(scoreToAdd, transform.position, Color.red);
        }
        else
        {
            _manager.setPlayer2Score(_manager.getPlayer2Score() + (scoreToAdd));
            DisplayScore(scoreToAdd, transform.position, Color.blue);
        }

    }

    public void DisplayScore(int score, Vector2 position, Color fontColor)
    {
        TextMeshPro playerTextPopup = ObjectPooler.instance.SpawnFromPool("Text", position, Quaternion.identity).GetComponent<TextMeshPro>();
        playerTextPopup.text = "+" + score.ToString();
        playerTextPopup.color = fontColor;
    }


    public void AddTopping(GameObject toppingToAdd)
    {
        //gets a string that tells us which topping to spawn onto it
        //string toppingName = _toppingSwitcher.getToppingObject((int)toppingAdded);

        //GameObject toppingToAdd = ObjectPooler.instance.SpawnFromPool(toppingName, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f), Quaternion.identity);

        scoreToAdd = CheckToppingToAdd(toppingToAdd);

        if (toppingToAdd.GetComponent<ToppingScript>().playerShooter == PlayerBehaviour.Players.Player1)
        {
            PlayerHits("one");
        }
        else if (toppingToAdd.GetComponent<ToppingScript>().playerShooter == PlayerBehaviour.Players.Player2)
        {
            PlayerHits("two");
        }

        toppings.Add(toppingToAdd);
        toppingToAdd.transform.parent = transform;
    }

    private int CheckToppingToAdd(GameObject toppingToAdd)
    {
        return _scoreHandler.GetScore(toppingToAdd.tag);
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

    private void Pop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5.0f), ForceMode2D.Impulse);
        //pizzaLife = 3;
    }

}