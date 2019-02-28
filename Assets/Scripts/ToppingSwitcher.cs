using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppingSwitcher : MonoBehaviour 
{
    public static ToppingSwitcher instance;

    public Sprite[] player1CenterIcons;
    public Sprite[] player2CenterIcons;

    public Sprite[] player1WheelIcons;
    public Sprite[] player2WheelIcons;

    private RegisterPlayers _regPlayers;

    [Range(1f, 50f)] [SerializeField] private float speed = 5f; // Movement speed of the icons.
    [SerializeField] private GameObject player1Reticule;        // Reference to player 1 reticule.
    private Transform player1Icon0;                             // Transform reference for rotating icons.
    private Transform player1Icon1;                             // ^^
    private Transform player1Icon2;                             // ^^
    private Transform player1Icon3;                             // ^^
    private Transform player1Icon4;                             // ^^
    private Vector3 player1Icon0StartPt;                        // Value for holding the final position for an icon while lerping.
    private Vector3 player1Icon1StartPt;                        // ^^
    private Vector3 player1Icon2StartPt;                        // ^^
    private Vector3 player1Icon3StartPt;                        // ^^
    private Vector3 player1Icon4StartPt;                        // ^^
    private Vector3 player1Icon0EndPt;                          // Value for holding the final position for an icon while lerping.
    private Vector3 player1Icon1EndPt;                          // ^^
    private Vector3 player1Icon2EndPt;                          // ^^
    private Vector3 player1Icon3EndPt;                          // ^^
    private Vector3 player1Icon4EndPt;                          // ^^
    private float player1RotationStart = 0;                     // Used to lerp player 1's icons when lerping.
    private float player1Dist;                                  // Value for holding the distance for an icon while lerping.
    private bool isPlayer1Rotating = false;                     // Is player 1 rotating?

    [SerializeField] private GameObject player2Reticule;        // Reference to player 2 reticule.
    private Transform player2Icon0;                             // Transform reference for rotating icons.
    private Transform player2Icon1;                             // ^^
    private Transform player2Icon2;                             // ^^
    private Transform player2Icon3;                             // ^^
    private Transform player2Icon4;                             // ^^
    private Vector3 player2Icon0StartPt;                        // Value for holding the final position for an icon while lerping.
    private Vector3 player2Icon1StartPt;                        // ^^
    private Vector3 player2Icon2StartPt;                        // ^^
    private Vector3 player2Icon3StartPt;                        // ^^
    private Vector3 player2Icon4StartPt;                        // ^^
    private Vector3 player2Icon0EndPt;                          // Value for holding the final position for an icon while lerping.
    private Vector3 player2Icon1EndPt;                          // ^^
    private Vector3 player2Icon2EndPt;                          // ^^
    private Vector3 player2Icon3EndPt;                          // ^^
    private Vector3 player2Icon4EndPt;                          // ^^
    private float player2RotationStart = 0;                     // Used to lerp player 2's icons when lerping.
    private float player2Dist;                                  // Value for holding the distance for an icon while lerping.
    private bool isPlayer2Rotating = false;                     // Is player 2 rotating?


    void Awake()
    {
        instance = this;
        _regPlayers = FindObjectOfType<RegisterPlayers>();

        // Sets reference to the rotating icons in the reticule.
        player1Icon0 = player1Reticule.transform.GetChild(0);
        player1Icon1 = player1Reticule.transform.GetChild(1);
        player1Icon2 = player1Reticule.transform.GetChild(2);
        player1Icon3 = player1Reticule.transform.GetChild(3);
        player1Icon4 = player1Reticule.transform.GetChild(4);

        player2Icon0 = player1Reticule.transform.GetChild(0);
        player2Icon1 = player1Reticule.transform.GetChild(1);
        player2Icon2 = player1Reticule.transform.GetChild(2);
        player2Icon3 = player1Reticule.transform.GetChild(3);
        player2Icon4 = player1Reticule.transform.GetChild(4);
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

        if (_regPlayers.Axis1RightClick() != null)
        {
            if (_regPlayers.Axis1RightClick().IsDown)
            {
                if ((int)(GetPlayer1Topping()) < toppings.Length - 1)
                {
                    SwitchPlayer1Topping((int)GetPlayer1Topping() + 1);
                }
                else
                {
                    SwitchPlayer1Topping(0);
                }
            }
        }

        //if (_regPlayers.Axis2LeftClick() != null)
        //{
        //    if (_regPlayers.Axis2LeftClick().IsDown)
        //    {
        //        if ((int)(GetPlayer2Topping()) > 0)
        //        {
        //            SwitchPlayer2Topping((int)GetPlayer2Topping() - 1);
        //        }
        //        else
        //        {
        //            SwitchPlayer2Topping(toppings.Length - 1);
        //        }
        //    }

        //}

        if (_regPlayers.Axis2RightClick() != null)
        {
            if (_regPlayers.Axis2RightClick().IsDown)
            {
                if ((int)(GetPlayer2Topping()) > 0)
                {
                    SwitchPlayer2Topping((int)GetPlayer2Topping() - 1);
                }
                else
                {
                    SwitchPlayer2Topping(toppings.Length - 1);
                }
            }
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







        if (Input.GetButtonDown("P1SwitchLeft"))
            Player1RotateToppings(false);
        if (Input.GetButtonDown("P1SwitchRight"))
            Player1RotateToppings(true);

        if (Input.GetButtonDown("P2SwitchLeft"))
            Player2RotateToppings(false);
        if (Input.GetButtonDown("P2SwitchRight"))
            Player2RotateToppings(true);

        // Moves the reticule icons.
        if (isPlayer1Rotating)
        {
            float distCovered = (Time.time - player1RotationStart) * speed;
            float fracJourney = distCovered / player1Dist;

            player1Icon0.position = Vector3.Lerp(player1Icon0StartPt, player1Icon0EndPt, fracJourney);
            player1Icon1.position = Vector3.Lerp(player1Icon1StartPt, player1Icon1EndPt, fracJourney);
            player1Icon2.position = Vector3.Lerp(player1Icon2StartPt, player1Icon2EndPt, fracJourney);
            player1Icon3.position = Vector3.Lerp(player1Icon3StartPt, player1Icon3EndPt, fracJourney);
            player1Icon4.position = Vector3.Lerp(player1Icon4StartPt, player1Icon4EndPt, fracJourney);

            if (fracJourney == 1)
                isPlayer1Rotating = false;
        }

        // Moves the reticule icons.
        if (isPlayer2Rotating)
        {
            float distCovered = (Time.time - player1RotationStart) * speed;
            float fracJourney = distCovered / player2Dist;

            player2Icon0.position = Vector3.Lerp(player2Icon0StartPt, player2Icon0EndPt, fracJourney);
            player2Icon1.position = Vector3.Lerp(player2Icon1StartPt, player2Icon1EndPt, fracJourney);
            player2Icon2.position = Vector3.Lerp(player2Icon2StartPt, player2Icon2EndPt, fracJourney);
            player2Icon3.position = Vector3.Lerp(player2Icon3StartPt, player2Icon3EndPt, fracJourney);
            player2Icon4.position = Vector3.Lerp(player2Icon4StartPt, player2Icon4EndPt, fracJourney);

            if (fracJourney == 1)
                isPlayer2Rotating = false;
        }
    }

    // Swaps topping clockwise for player 1.
    public void Player1RotateToppings(bool isClockwise)
    {
        if (!isPlayer1Rotating)
        {
            player1RotationStart = Time.time;

            player1Icon0StartPt = player1Icon0.position;
            player1Icon1StartPt = player1Icon1.position;
            player1Icon2StartPt = player1Icon2.position;
            player1Icon3StartPt = player1Icon3.position;
            player1Icon4StartPt = player1Icon4.position;

            if (isClockwise)
            {
                player1Icon0EndPt = player1Icon1StartPt;
                player1Icon1EndPt = player1Icon2StartPt;
                player1Icon2EndPt = player1Icon3StartPt;
                player1Icon3EndPt = player1Icon4StartPt;
                player1Icon4EndPt = player1Icon0StartPt;
            }
            else
            {
                player1Icon0EndPt = player1Icon4StartPt;
                player1Icon1EndPt = player1Icon0StartPt;
                player1Icon2EndPt = player1Icon1StartPt;
                player1Icon3EndPt = player1Icon2StartPt;
                player1Icon4EndPt = player1Icon3StartPt;
            }

            player1Dist = Vector3.Distance(player1Icon0.position, player1Icon0EndPt);

            isPlayer1Rotating = true;
        }
    }

    // Swaps topping clockwise for player 1.
    public void Player2RotateToppings(bool isClockwise)
    {
        if (!isPlayer2Rotating)
        {
            player2RotationStart = Time.time;

            player2Icon0StartPt = player2Icon0.position;
            player2Icon1StartPt = player2Icon1.position;
            player2Icon2StartPt = player2Icon2.position;
            player2Icon3StartPt = player2Icon3.position;
            player2Icon4StartPt = player2Icon4.position;

            if (isClockwise)
            {
                player2Icon0EndPt = player2Icon1StartPt;
                player2Icon1EndPt = player2Icon2StartPt;
                player2Icon2EndPt = player2Icon3StartPt;
                player2Icon3EndPt = player2Icon4StartPt;
                player2Icon4EndPt = player2Icon0StartPt;
            }
            else
            {
                player2Icon0EndPt = player2Icon4StartPt;
                player2Icon1EndPt = player2Icon0StartPt;
                player2Icon2EndPt = player2Icon1StartPt;
                player2Icon3EndPt = player2Icon2StartPt;
                player2Icon4EndPt = player2Icon3StartPt;
            }

            player2Dist = Vector3.Distance(player2Icon0.position, player2Icon0EndPt);

            isPlayer2Rotating = true;
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
        if ((int)p1Topping < toppingToSwitch || ((int)p1Topping == toppings.Length && toppingToSwitch == 0))
            Player1RotateToppings(false);
        else
            Player1RotateToppings(true);

        p1Topping = (Toppings)toppingToSwitch;
        p1ToppingDisplay.sprite = toppingSprites[toppingToSwitch];
    }

    public void SwitchPlayer2Topping(int toppingToSwitch)
    {
        if ((int)p2Topping < toppingToSwitch || ((int)p2Topping == toppings.Length && toppingToSwitch == 0))
            Player2RotateToppings(false);
        else
            Player2RotateToppings(true);

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
