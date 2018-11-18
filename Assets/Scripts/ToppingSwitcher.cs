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

    public GameObject[] toppingObjects;

    public Toppings topping;

    public void SwitchTopping(Toppings toppingToSwitch)
    {
        topping = toppingToSwitch;
    }
}
