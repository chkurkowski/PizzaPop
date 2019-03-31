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
    private const float baseFontSize = 7.0f;


    [SerializeField]
    private int pizzaLife;

    private int scoreToAdd = 0;


    private float fallMultiplier = 0.8f;

    private float riseMultiplier = 1.0f;

    private float player1ComboPrev;

    private float player2ComboPrev;

    private int uniqueToppingCount = 0;

    public bool pepperoni, greenPepper, mushroom, onion, olive = false;

    [SerializeField]
    private const float LARGE_MULTIPLIER = 1f;
    [SerializeField]
    private const float MEDIUM_MULTIPLIER = 1.2f;
    [SerializeField]
    private const float SMALL_MULTIPLIER = 1.5f;

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
            if(player1ComboPrev != _manager.GetPlayer1Combo() && _manager.GetPlayer1Combo() != 1)
            {
                _manager.setPlayer1Score(_manager.getPlayer1Score() + (scoreToAdd));
                DisplayScore(_manager.GetPlayer1Combo(), transform.position, Color.red);
                player1ComboPrev = _manager.GetPlayer1Combo();
            }
        }
        else
        {
            if(player2ComboPrev != _manager.GetPlayer2Combo() && _manager.GetPlayer2Combo() != 1)
            {
                _manager.setPlayer2Score(_manager.getPlayer2Score() + (scoreToAdd));
                DisplayScore(_manager.GetPlayer2Combo(), transform.position, Color.blue);
                player2ComboPrev = _manager.GetPlayer2Combo();
            }
        }

    }

    private void CheckSizeAndTopping(string topping, string player)
    {
        print(topping);
        switch(pizzaSize)
        {
            case PizzaSizes.Large:
                scoreToAdd = (int)Mathf.Floor(scoreToAdd * LARGE_MULTIPLIER);
                break;
            case PizzaSizes.Medium:
                scoreToAdd = (int)Mathf.Floor(scoreToAdd * MEDIUM_MULTIPLIER);
                break;
            case PizzaSizes.Small:
                scoreToAdd = (int)Mathf.Floor(scoreToAdd * SMALL_MULTIPLIER);
                break;
        }

        switch(topping)
        {
            case "Pepporoni": //I am aware that this is not how you spell Pepporoni - Chase
                if(!pepperoni)
                {
                    pepperoni = true;
                    uniqueToppingCount++;
                    AddToPlayerCombo(player);
                }
                break;
            case "GreenPepper":
                if(!greenPepper)
                {
                    greenPepper = true;
                    uniqueToppingCount++;
                    AddToPlayerCombo(player);
                }
                break;
            case "Onion":
                if(!onion)
                {
                    onion = true;
                    uniqueToppingCount++;
                    AddToPlayerCombo(player);
                }
                break;
            case "Mushroom":
                if(!mushroom)
                {
                    mushroom = true;
                    uniqueToppingCount++;
                    AddToPlayerCombo(player);
                }
                break;
            case "Olive":
                if(!olive)
                {
                    olive = true;
                    uniqueToppingCount++;
                    AddToPlayerCombo(player);
                }
                break;
        }

        if(player == "one")
        {
            scoreToAdd = (int)Mathf.Floor(scoreToAdd * _manager.GetPlayer1Combo());
        }
        else if(player == "two")
        {
            scoreToAdd = (int)Mathf.Floor(scoreToAdd * _manager.GetPlayer2Combo());
        }
    }

    private void AddToPlayerCombo(string player)
    {
        // print(uniqueToppingCount);
        if(player == "one")
        {
            if(_manager.GetPlayer1Combo() > 0 && uniqueToppingCount > 0)
            {
                _manager.SetPlayer1Combo(.2f);
            }
            else if(uniqueToppingCount > 1)
            {
                _manager.SetPlayer1Combo(.2f);
            }
        }
        else if(player == "two")
        {
            if(_manager.GetPlayer2Combo() > 0 && uniqueToppingCount > 0)
            {
                _manager.SetPlayer2Combo(.2f);
            }
            else if(uniqueToppingCount > 1)
            {
                _manager.SetPlayer2Combo(.2f);
            }
        }
    }

    public void DisplayScore(float combo, Vector2 position, Color fontColor)
    {
        if (combo <= 1.0f)
            return;

        float temp = (int)(combo * 100);

        combo = temp / 100;

        TextMeshPro playerTextPopup = ObjectPooler.instance.SpawnFromPool("Text", position, Quaternion.identity).GetComponent<TextMeshPro>();

        playerTextPopup.text = "x" + combo.ToString();
        playerTextPopup.fontSize = baseFontSize * combo;

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
            CheckSizeAndTopping(toppingToAdd.tag, "one");
            PlayerHits("one");
        }
        else if (toppingToAdd.GetComponent<ToppingScript>().playerShooter == PlayerBehaviour.Players.Player2)
        {
            CheckSizeAndTopping(toppingToAdd.tag, "two");
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
        if (transform.position.y < -20.0f)
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