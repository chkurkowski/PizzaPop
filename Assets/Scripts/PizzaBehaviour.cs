using System.Collections.Generic;
using UnityEngine;

public class PizzaBehaviour : MonoBehaviour, iPoolerObject
{
    private ToppingSwitcher _toppingSwitcher;
    private GameManager _manager;
    private List<GameObject> toppings;
    private PizzaOrder _pizzaOrder;

    private void Awake()
    {
        _pizzaOrder = GetComponent<PizzaOrder>();
        _toppingSwitcher = GameObject.Find("ToppingSwitcher").GetComponent<ToppingSwitcher>();
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        toppings = new List<GameObject>();
    }

    private void Start()
    {


    }

    private void OnMouseDown()
    {
        Player1Hits();
    }

    private void Player1Hits()
    {
        AddTopping(_toppingSwitcher.GetPlayer1Topping());

        if (_toppingSwitcher.GetPlayer1Topping() == _pizzaOrder.GetToppingNeeded())
        {
            _manager.setPlayer1Score(_manager.getPlayer1Score() + 300);
        }
        else
        {
            _manager.setPlayer1Score(_manager.getPlayer1Score() + 100);
        }
    }

    public void AddTopping(ToppingSwitcher.Toppings toppingAdded)
    {
        //gets a string that tells us which topping to spawn onto it
        string toppingName = _toppingSwitcher.getToppingObject((int)toppingAdded);

        GameObject toppingToAdd = ObjectPooler.instance.SpawnFromPool(toppingName, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f), Quaternion.identity);

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
}