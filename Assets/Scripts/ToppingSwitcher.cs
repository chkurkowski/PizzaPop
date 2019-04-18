using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppingSwitcher : MonoBehaviour 
{
    public static ToppingSwitcher instance;

    [SerializeField] private Animator player1Animator;
    [SerializeField] private Animator player2Animator;

    public Sprite[] player1CenterIcons;
    public Sprite[] player2CenterIcons;

    public Sprite[] player1WheelIcons;
    public Sprite[] player2WheelIcons;

    private float player1switchTimer;
    private float player2switchTimer;



    void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        /* Buttons 1-5 on the keyboard are for Player 1*/

        if (Input.GetKeyDown("1"))
        {
            SwitchPlayer1Topping(0); // Switch Player 1's topping to Pepperoni}
        }
            if (Input.GetKeyDown("2"))
        {
            SwitchPlayer1Topping(1); // Switch Player 1's topping to GreenPepper
        }
        
        if (Input.GetKeyDown("3"))
        {
            SwitchPlayer1Topping(2); // Switch Player 1's topping to Mushroom
        }

        if (Input.GetKeyDown("4"))
        {
            SwitchPlayer1Topping(3); // Switch Player 1's topping to Onion
        }

        if (Input.GetKeyDown("5"))
        {
            SwitchPlayer1Topping(4); // Switch Player 1's topping to BlackOlive
        }

        if (player1switchTimer > 0)
            player1switchTimer -= Time.deltaTime;

        if (player2switchTimer > 0)
            player2switchTimer -= Time.deltaTime;

        /* Player 1 Topping cycle*/

        //if (_regPlayers.Axis1LeftClick() != null)
        //{
        //    if (_regPlayers.Axis1LeftClick().IsDown)
        //    {
        //        if ((int)(GetPlayer1Topping()) < toppings.Length - 1)
        //        {
        //            SwitchPlayer1Topping((int)GetPlayer1Topping() + 1);
        //        }
        //        else
        //        {
        //            SwitchPlayer1Topping(0);
        //        }

        //        //if (toppings[(int)GetPlayer1Topping()] == "Onion")
        //        //{
        //        //    SwitchPlayer1Topping(0);
        //        //}
        //        //else if (toppings[(int)GetPlayer1Topping()] == "Pepperoni") 
        //        //{
        //        //    SwitchPlayer1Topping(1);        
        //        //}
        //        //else if (toppings[(int)GetPlayer1Topping()] == "GreenPepper")
        //        //{
        //        //    SwitchPlayer1Topping(4);
        //        //}
        //        //else if (toppings[(int)GetPlayer1Topping()] == "Mushroom")
        //        //{
        //        //    SwitchPlayer1Topping(2);
        //        //}
        //        //else if (toppings[(int)GetPlayer1Topping()] == "BlackOlive")
        //        //{
        //        //    SwitchPlayer1Topping(3);
        //        //}
        //        //else
        //        //{
        //        //    SwitchPlayer1Topping(1); // The pepperoni line wouldn't work so I added this. No idea why it didn't work to begin with.
        //        //}
        //    }
        //}

        //Toggling Toppings

        if (Input.GetButtonDown("P1SwitchLeft") && player1switchTimer <= 0)
        {
            player1switchTimer = 0.5f;

            AudioManager.instance.Play("SwitchLeft");
            player1Animator.SetBool("RotateClockwise", true);

            if ((int)(GetPlayer1Topping()) < toppings.Length - 1)
            {
                SwitchPlayer1Topping((int)GetPlayer1Topping() + 1);
            }
            else
            {
                SwitchPlayer1Topping(0);
            }
        }
        else if (Input.GetButtonDown("P1SwitchRight") && player1switchTimer <= 0)
        {
            player1switchTimer = 0.5f;

            AudioManager.instance.Play("SwitchLeft");
            player1Animator.SetBool("RotateCounterClockwise", true);

            if ((int)(GetPlayer1Topping()) > 0)
            {
                SwitchPlayer1Topping((int)GetPlayer1Topping() - 1);
            }
            else
            {
                SwitchPlayer1Topping(toppings.Length - 1);
            }

            AudioManager.instance.Play("SwitchLeft");
        }
        else
        {
            player1Animator.SetBool("RotateClockwise", false);
            player1Animator.SetBool("RotateCounterClockwise", false);
        }


        if (Input.GetButtonDown("P2SwitchLeft") && player2switchTimer <= 0)
            {

            player2switchTimer = 0.5f;

            AudioManager.instance.Play("SwitchLeft");
            player2Animator.SetBool("RotateClockwise", true);

            if ((int)(GetPlayer2Topping()) < toppings.Length - 1)
            {
                SwitchPlayer2Topping((int)GetPlayer2Topping() + 1);
            }
            else
            {
                SwitchPlayer2Topping(0);
            }
            AudioManager.instance.Play("SwitchLeft");
        }
        else if (Input.GetButtonDown("P2SwitchRight") && player2switchTimer <= 0)
        {
            player2switchTimer = 0.5f;

            AudioManager.instance.Play("SwitchLeft");
            player2Animator.SetBool("RotateCounterClockwise", true);

            if ((int)(GetPlayer2Topping()) > 0)
            {
                SwitchPlayer2Topping((int)GetPlayer2Topping() - 1);
            }
            else
            {
                SwitchPlayer2Topping(toppings.Length - 1);
            }

            AudioManager.instance.Play("SwitchLeft");
        }
        else
        {
            player2Animator.SetBool("RotateClockwise", false);
            player2Animator.SetBool("RotateCounterClockwise", false);
        }



        /* Buttons 6-0 on the keyboard are for Player 2*/

        if (Input.GetKeyDown("6"))
        {
            SwitchPlayer2Topping(0); // Switch Player 2's topping to Pepperoni
        }

        if (Input.GetKeyDown("7"))
        {
            SwitchPlayer2Topping(1); // Switch Player 2's topping to GreenPepper
        }

        if (Input.GetKeyDown("8"))
        {
            SwitchPlayer2Topping(2); // Switch Player 2's topping to Mushroom
        }

        if (Input.GetKeyDown("9"))
        {
            SwitchPlayer2Topping(3); // Switch Player 2's topping to Onion
        }

        if (Input.GetKeyDown("0"))
        {
            SwitchPlayer2Topping(4); // Switch Player 2's topping to BlackOlive
        }

        if (Input.GetKeyDown("y"))
        {
           if (toppings[(int)GetPlayer2Topping()] == "BlackOlive")
           {
                SwitchPlayer2Topping(0);
           }
           else if (toppings[(int)GetPlayer2Topping()] == "Pepperoni")
           {
                SwitchPlayer2Topping(1);
           }
           else if (toppings[(int)GetPlayer2Topping()] == "GreenPepper")
           {
                SwitchPlayer2Topping(2);
           }
           else if (toppings[(int)GetPlayer2Topping()] == "Mushroom")
           {
                SwitchPlayer2Topping(3);
           }
           else if (toppings[(int)GetPlayer2Topping()] == "Onion")
           {
                SwitchPlayer2Topping(4);
           }
           else
            {
                SwitchPlayer2Topping(1);
            }
        }
    }

    

    public enum Toppings 
    { 
        Pepperoni,   //topping 0
        Mushroom, //topping 1
        BlackOlive,    //topping 2
        GreenPepper,       //topping 3
        Onion  //topping 4
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
        //p1ToppingDisplay.sprite = toppingSprites[toppingToSwitch];
    }

    public void SwitchPlayer2Topping(int toppingToSwitch)
    {
        p2Topping = (Toppings)toppingToSwitch;
        //p2ToppingDisplay.sprite = toppingSprites[toppingToSwitch];
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
