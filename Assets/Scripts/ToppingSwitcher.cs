using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public string[] toppings;

    private Toppings p1Topping;
    private Toppings p2Topping;

    public void SwitchPlayer1Topping(int toppingToSwitch)
    {
        p1Topping = (Toppings)toppingToSwitch;
    }

    public void SwitchPlayer2Toppin(int toppingToSwitch)
    {
        p2Topping = (Toppings)toppingToSwitch;
    }

    public void sayHi()
    {

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
