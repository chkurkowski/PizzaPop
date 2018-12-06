using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppingSwitcher : MonoBehaviour 
{
    public enum Toppings 
    {
        Pepperoni,
        GreenPepper,
        Mushroom,
        Onion,
        BlackOlive
    }

    [SerializeField]
    private Image p1ToppingDisplay;

    [SerializeField]
    private Image p2ToppingDisplay;

    public Sprite[] toppingSprites;
    public string[] toppings;

    private Toppings p1Topping;
    private Toppings p2Topping;

    public void SwitchPlayer1Topping(int toppingToSwitch)
    {
        p1Topping = (Toppings)toppingToSwitch;
        p1ToppingDisplay.sprite = toppingSprites[toppingToSwitch];
    }

    public void SwitchPlayer2Topping(int toppingToSwitch)
    {
        p2Topping = (Toppings)toppingToSwitch;
        p2ToppingDisplay.sprite = toppingSprites[toppingToSwitch];
    }


    public Toppings GetPlayer1Topping()
    {
        return p1Topping;
    }

    public Toppings GetPlayer2Topping()
    {
        return p2Topping;
    }

    public string getToppingObject (int toppingNeeded)
    {
        return toppings[toppingNeeded];
    }


}
