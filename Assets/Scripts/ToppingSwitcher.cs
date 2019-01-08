using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppingSwitcher : MonoBehaviour 
{
    public static ToppingSwitcher instance;
    public Image[] toppingsP1;
    public Image[] toppingsP2;

    void Awake()
    {
        instance = this;
    }

    public void Update() 
    {
        /* Buttons 1-5 on the keyboard are for Player 1*/

        if (Input.GetKeyDown("1"))
        {
            SwitchPlayer1Topping(0); // Switch Player 1's topping to Pepperoni

            //make the current topping bold
            Color col = toppingsP1[0].color;
            col.a = 1;
            toppingsP1[0].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP1[1].color = col;
            toppingsP1[2].color = col;
            toppingsP1[3].color = col;
            toppingsP1[4].color = col;
        }

        if (Input.GetKeyDown("2"))
        {
            SwitchPlayer1Topping(1); // Switch Player 1's topping to GreenPepper

            //make the current topping bold
            Color col = toppingsP1[1].color;
            col.a = 1;
            toppingsP1[1].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP1[0].color = col;
            toppingsP1[2].color = col;
            toppingsP1[3].color = col;
            toppingsP1[4].color = col;
        }
        
        if (Input.GetKeyDown("3"))
        {
            SwitchPlayer1Topping(2); // Switch Player 1's topping to Mushroom

            //make the current topping bold
            Color col = toppingsP1[2].color;
            col.a = 1;
            toppingsP1[2].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP1[0].color = col;
            toppingsP1[1].color = col;
            toppingsP1[3].color = col;
            toppingsP1[4].color = col;
        }

        if (Input.GetKeyDown("4"))
        {
            SwitchPlayer1Topping(3); // Switch Player 1's topping to Onion

            //make the current topping bold
            Color col = toppingsP1[3].color;
            col.a = 1;
            toppingsP1[3].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP1[0].color = col;
            toppingsP1[1].color = col;
            toppingsP1[2].color = col;
            toppingsP1[4].color = col;
        }

        if (Input.GetKeyDown("5"))
        {
            SwitchPlayer1Topping(4); // Switch Player 1's topping to BlackOlive

            //make the current topping bold
            Color col = toppingsP1[4].color;
            col.a = 1;
            toppingsP1[4].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP1[0].color = col;
            toppingsP1[1].color = col;
            toppingsP1[2].color = col;
            toppingsP1[3].color = col;
        }

        /* Buttons 6-0 on the keyboard are for Player 2*/

        if (Input.GetKeyDown("6"))
        {
            SwitchPlayer2Topping(0); // Switch Player 2's topping to Pepperoni

            //make the current topping bold
            Color col = toppingsP2[0].color;
            col.a = 1;
            toppingsP2[0].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP2[1].color = col;
            toppingsP2[2].color = col;
            toppingsP2[3].color = col;
            toppingsP2[4].color = col;

        }

        if (Input.GetKeyDown("7"))
        {
            SwitchPlayer2Topping(1); // Switch Player 2's topping to GreenPepper

            //make the current topping bold
            Color col = toppingsP2[1].color;
            col.a = 1;
            toppingsP2[1].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP2[0].color = col;
            toppingsP2[2].color = col;
            toppingsP2[3].color = col;
            toppingsP2[4].color = col;
        }

        if (Input.GetKeyDown("8"))
        {
            SwitchPlayer2Topping(2); // Switch Player 2's topping to Mushroom

            //make the current topping bold
            Color col = toppingsP2[2].color;
            col.a = 1;
            toppingsP2[2].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP2[0].color = col;
            toppingsP2[1].color = col;
            toppingsP2[3].color = col;
            toppingsP2[4].color = col;
        }

        if (Input.GetKeyDown("9"))
        {
            SwitchPlayer2Topping(3); // Switch Player 2's topping to Onion

            //make the current topping bold
            Color col = toppingsP2[3].color;
            col.a = 1;
            toppingsP2[3].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP2[0].color = col;
            toppingsP2[1].color = col;
            toppingsP2[2].color = col;
            toppingsP2[4].color = col;
        }

        if (Input.GetKeyDown("0"))
        {
            SwitchPlayer2Topping(4); // Switch Player 2's topping to BlackOlive

            //make the current topping bold
            Color col = toppingsP2[4].color;
            col.a = 1;
            toppingsP2[4].color = col;
            //makes all other topping bins transparent
            col.a = .5f;
            toppingsP2[0].color = col;
            toppingsP2[1].color = col;
            toppingsP2[2].color = col;
            toppingsP2[3].color = col;
        }
    }

    public enum Toppings 
    { 
        Pepperoni,   //topping 0
        GreenPepper, //topping 1
        Mushroom,    //topping 2
        Onion,       //topping 3
        BlackOlive   //topping 4
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

    public string getPlayer1ToppingName()
    {
        return toppings[(int)GetPlayer1Topping()];
    }

    public string getPlayer2ToppingName()
    {
        return toppings[(int)GetPlayer2Topping()];
    }

    public string getToppingObject (int toppingNeeded)
    {
        return toppings[toppingNeeded];
    }
}
