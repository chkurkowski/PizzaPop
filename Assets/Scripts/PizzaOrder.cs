using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaOrder : MonoBehaviour 
{
    private ToppingSwitcher.Toppings toppingNeeded = ToppingSwitcher.Toppings.Pepperoni;
    private ToppingSwitcher _toppingSwitcher;

    
    private void Awake()
    {
        _toppingSwitcher = FindObjectOfType<ToppingSwitcher>();
    }

    public void AssignRandomTopping()
    {
        toppingNeeded = (ToppingSwitcher.Toppings)Random.Range(0, 5);
        assignToppingNeededSprite();
    }

    public ToppingSwitcher.Toppings GetToppingNeeded()
    {
        return toppingNeeded;
    }

    void assignToppingNeededSprite()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _toppingSwitcher.toppingSprites[(int)toppingNeeded];
    }
}
