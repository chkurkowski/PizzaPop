using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreInput : MonoBehaviour {

	private HighScoreManager manager;

	public GameManager gameManager;

	public TextMeshProUGUI currentPlayer;

	public TextMeshProUGUI name;

	public int scoreP1;
	public int scoreP2;

	public bool PlayerOneHighScore = false;
	public bool PlayerTwoHighScore = false;

	private int letterIndexP1 = 0;
	private int letterIndexP2 = 0;

	private int[] lettersP1 = {1, 1, 1};
	private int[] lettersP2 = {1, 1, 1};

	// Use this for initialization
	void Start () 
	{
		manager = HighScoreManager.inst;
	}
	
	// Update is called once per frame // P1Trigger/P2Trigger P1SwitchLeft/P1SwitchRight 
	void Update () 
	{
		if(PlayerOneHighScore)
			name.text = (Alphabet(lettersP1[0]) + Alphabet(lettersP1[1]) + Alphabet(lettersP1[2]));
		if(PlayerTwoHighScore && !PlayerOneHighScore)
			name.text = (Alphabet(lettersP2[0]) + Alphabet(lettersP2[1]) + Alphabet(lettersP2[2]));

		if(PlayerOneHighScore)
		{
			currentPlayer.text = ("Rosso:");
			currentPlayer.color = new Color32(168, 49, 55, 255);
			PlayerOneSwitch();
		}

		if(PlayerTwoHighScore && !PlayerOneHighScore)
		{
			currentPlayer.text = ("Verde:");
			currentPlayer.color = new Color32(65, 155, 7, 255);
			PlayerTwoSwitch();
		}

		if(!PlayerOneHighScore && !PlayerTwoHighScore)
		{
			if(manager.inputBoard.activeSelf)
			{
				manager.inputBoard.SetActive(false);
				manager.UpdateHighScoreUI();
				gameManager.EnableFinalText();
			}
		}
	}

	private void PlayerOneSwitch()
	{
		if(Input.GetButtonDown("P1SwitchLeft"))
		{
			LetterSwitcher(1, letterIndexP1, -1);
			if(lettersP1[letterIndexP1] < 0)
			{
				lettersP1[letterIndexP1] = 26;
			}
		}
		else if(Input.GetButtonDown("P1SwitchRight"))
		{
			LetterSwitcher(1, letterIndexP1, 1);
			if(lettersP1[letterIndexP1] > 26)
			{
				lettersP1[letterIndexP1] = 0;
			}
		}

		if(Input.GetButtonDown("P1Trigger"))
		{
			letterIndexP1++;
			if(letterIndexP1 > 2)
			{
				manager.AddHighScoreName(Alphabet(lettersP1[0]) + Alphabet(lettersP1[1]) + Alphabet(lettersP1[2]), scoreP1);
				PlayerOneHighScore = false;
			}
		}
	}

	private void PlayerTwoSwitch()
	{
		if(Input.GetButtonDown("P2SwitchLeft"))
		{
			LetterSwitcher(2, letterIndexP2, -1);
			if(lettersP2[letterIndexP2] < 0)
			{
				lettersP2[letterIndexP2] = 26;
			}
		}
		else if(Input.GetButtonDown("P2SwitchRight"))
		{
			LetterSwitcher(2, letterIndexP2, 1);
			if(lettersP2[letterIndexP2] > 26)
			{
				lettersP2[letterIndexP2] = 0;
			}
		}

		if(Input.GetButtonDown("P2Trigger"))
		{
			letterIndexP2++;
			if(letterIndexP2 > 2)
			{
				manager.AddHighScoreName(Alphabet(lettersP2[0]) + Alphabet(lettersP2[1]) + Alphabet(lettersP2[2]), scoreP2);
				PlayerTwoHighScore = false;
			}
		}
	}

	private void LetterSwitcher(int player, int index, int amt)
	{
		if(player == 1)
		{
			lettersP1[index] += amt;
		}
		else if (player == 2)
		{
			lettersP2[index] += amt;
		}
	}

	private string Alphabet(int letter)
	{
		switch(letter)
		{
			case 0:
				return " ";
			case 1:
				return "A";
			case 2:
				return "B";
			case 3:
				return "C";
			case 4:
				return "D";
			case 5:
				return "E";
			case 6:
				return "F";
			case 7:
				return "G";
			case 8:
				return "H";
			case 9:
				return "I";
			case 10:
				return "J";
			case 11:
				return "K";
			case 12:
				return "L";
			case 13:
				return "M";
			case 14:
				return "N";
			case 15:
				return "O";
			case 16:
				return "P";
			case 17:
				return "Q";
			case 18:
				return "R";
			case 19:
				return "S";
			case 20:
				return "T";
			case 21:
				return "U";
			case 22:
				return "V";
			case 23:
				return "W";
			case 24:
				return "X";
			case 25:
				return "Y";
			case 26:
				return "Z";

		}

		return "";
	}
}
