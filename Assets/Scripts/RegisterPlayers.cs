using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RavingBots.MultiInput;
using UnityEngine.UI;


public class RegisterPlayers : MonoBehaviour 
{

    private InputState inputState;

    private IDevice player1Device;
    private IDevice player2Device;

    private IVirtualAxis player1Axis;
    private IVirtualAxis player2Axis;

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
                    player1Axis = player1Device[InputCode.MouseLeft];
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
                    player2Axis = player2Device[InputCode.MouseLeft];
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

    public IVirtualAxis Axis1()
    {
        return player1Axis;
    }

    public IVirtualAxis Axis2()
    {
        return player2Axis;
    }
}
