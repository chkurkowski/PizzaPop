using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RavingBots.MultiInput;
using UnityEngine.UI;


public class RegisterPlayers : MonoBehaviour 
{
    private InputState inputState;

    private IDevice player1Device = null;
    private IDevice player2Device = null;

    private IVirtualAxis player1Left;
    private IVirtualAxis player2Left;

    private IVirtualAxis player1Right;
    private IVirtualAxis player2Right;


    private void Awake()
    {
        inputState = FindObjectOfType<InputState>();
    }

    [SerializeField]
    private Text instructionText;


    [SerializeField]
    private Image player1progressImage;
    private float player1progress;

    [SerializeField]
    private Image player2progressImage;
    private float player2progress;

    // Use this for initialization
    void Start () 
    {
        instructionText.text = "Player 1 hold the mouse button";
	}
    	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButton(0))
        {
            if (player1Device == null)
            {
                player1progress += Time.deltaTime;

                if (player1progress >= 1.0f)
                {
                    player1Device = inputState.FindFirstHeld();
                    Debug.Log(inputState.FindFirstHeld());
                    player1Left = player1Device[InputCode.MouseLeft];
                    player1Right = player1Device[InputCode.MouseRight];
                    instructionText.text = "Player 2 hold the mouse button";
                }
            }
            else if (player2Device == null && inputState.FindFirstHeld() != player1Device)
            {
               
                //player2Device = inputState.FindFirstHeld();
                //player2Axis = player2Device[InputCode.MouseLeft];
                player2progress += Time.deltaTime;

                if (player2progress >= 1.0f)
                {
                    player2Device = inputState.FindFirstHeld();
                    player2Left = player2Device[InputCode.MouseLeft];
                    player2Right = player2Device[InputCode.MouseRight];
                }
            }
            else if (player1Device != null & player2Device != null)
            {
                instructionText.text = "All players Synced";
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (player1progress < 1)
                player1progress = 0f;
            else if (player2progress < 1)
                player2progress = 0f;
        }

        player1progressImage.fillAmount = player1progress;
        player2progressImage.fillAmount = player2progress;
	}

    public IDevice Mouse1()
    {
        if (player1Device != null)
        {
            return player1Device;
        }

        return null;
    }

    public IDevice Mouse2()
    {
        if (player2Device != null)
        {
            return player2Device;
        }

        return null;
    }

    public IVirtualAxis Axis1LeftClick()
    {
        return player1Left;
    }

    public IVirtualAxis Axis2LeftClick()
    {
        return player2Left;
    }

    public IVirtualAxis Axis1RightClick()
    {
        return player1Right;
    }

    public IVirtualAxis Axis2RightClick()
    {
        return player2Right;
    }

}
