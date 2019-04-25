using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Hold eight high scores
//TODO: Build helper functions to test high score
public class HighScoreManager : MonoBehaviour {

	#region Singleton

	public static HighScoreManager inst;

	void Awake()
	{
		if(inst == null)
			inst = this;
		else
			Destroy(gameObject);
	}

	#endregion

	public TextMeshProUGUI[] scoreDisplay;
	public TextMeshProUGUI[] nameDisplay;

	[Space(10)]

	public GameManager manager;

	public GameObject inputBoard;

	public int[] dummyScores;

	public List<int> highScores;
	public List<string> highScoreNames; //Needs further logic to hold names

	void Start()
	{
		// TestUtility();
		// ParseHighScores(); 
	}

	void OnEnable()
	{
		ParseHighScores();
		UpdateHighScoreUI();
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
		{
			PlayerPrefs.SetString("HighScoreNames", "");
			PlayerPrefs.SetString("HighScores", "");
		}

		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D))
			print(PlayerPrefs.GetString("HighScoreNames"));
	}

	public void UpdateHighScoreUI()
	{
		for(int i = 0; i < highScores.Count; i++)
		{
			if(highScores.Count > 0)
				scoreDisplay[i].text = highScores[i].ToString();
			else
				scoreDisplay[i].text = "-";
		}

		for(int i = 0; i < highScoreNames.Count; i++)
		{
			// print(highScoreNames[i]);
			string[] nameString = highScoreNames[i].Split('|');
			print(nameString[0]);
			if(nameString[0] != "")
			{
				nameDisplay[i].text = nameString[0];
			}
			else
				nameDisplay[i].text = "-";
		}
		// PrintUtility(); //For Debugging only
	}

	public void CheckHighScore(int score, int player)
	{
		//Check against the lowest of the high scores.
		//if it is a high score AddHighScore(score);
		if(highScores.Count < 8)
		{
			//Activate Panel for name typing
			TypingOnGuns(player, score);
			AddHighScore(score);
			return;
		}
		else if(score > highScores[7])
		{
			//Activate Panel for Typing
			TypingOnGuns(player, score);
			AddHighScore(score);
		}
	}

	private void AddHighScore(int score)
	{
		//Add the new score to the list
		highScores.Add(score);
		//Sort the list with list.Sort()
		if(highScores.Count > 1)
		{
			highScores.Sort();
			highScores.Reverse();
		}
		//if it has more than 8 numbers cull the last
		if(highScores.Count > 8)
		{
			RemoveFromNameList(highScores[8]);
			highScores.RemoveAt(8);
		}
		//Add all values into PlayerPrefs
		string scores = "";
		for(int i = 0; i < highScores.Count; i++)
		{
			if((i + 1) == highScores.Count)
				scores += highScores[i].ToString();
			else
				scores += highScores[i].ToString() + ",";
		}
		// Debug.Log("Adding to PlayerPrefs");
		PlayerPrefs.SetString("HighScores", scores);
	}

	//highScores.IndexOf(score);
	public void AddHighScoreName(string name, int score)
	{
		highScoreNames.Add(name + "|" + score);

		SortNames();

		if(highScoreNames.Count > highScores.Count)
			highScoreNames.RemoveAt(highScores.Count);

		string names = "";
		for(int i = 0; i < highScoreNames.Count; i++)
		{
			if((i + 1) == highScoreNames.Count)
				names += highScoreNames[i];
			else
				names += highScoreNames[i] + ",";
		}
		PlayerPrefs.SetString("HighScoreNames", names);
	}

	//Add the name string in with the score and then parse the name out seperately in a second split.
	private void ParseHighScores()
	{
		// Debug.Log("Parsing High Scores");
		highScores.Clear();
		string tempString = PlayerPrefs.GetString("HighScores");
		string[] subStrings = tempString.Split(',');

		if(subStrings.Length != 0 && tempString != "")
		{
			//Add second split here
			foreach(string s in subStrings)
				// if(!highScores.Contains(int.Parse(s)))
					AddHighScore(int.Parse(s));
		}

		highScoreNames.Clear();
		string tempName = PlayerPrefs.GetString("HighScoreNames");
		string[] subNames = tempName.Split(',');

		if(subNames.Length != 0 && tempName != "")
		{
			foreach(string s in subNames)
			{
				string[] names = s.Split('|');
				print(s);
				AddHighScoreName(names[0], int.Parse(names[1]));
			}
		}
	}

	public void TypingOnGuns(int player, int score)
	{
		inputBoard.SetActive(true);

		if(player == 1)
		{
			GetComponent<HighScoreInput>().scoreP1 = score;
			GetComponent<HighScoreInput>().PlayerOneHighScore = true;
		}
		else if(player == 2)
		{
			GetComponent<HighScoreInput>().scoreP2 = score;
			GetComponent<HighScoreInput>().PlayerTwoHighScore = true;
		}
		else
			manager.InvokeFinalText();
	}

	private void SortNames()
	{
		string[] names = new string[highScoreNames.Count];

		for(int i = 0; i < highScoreNames.Count; i++)
			names[i] = highScoreNames[i];

		for(int i = 0; i < highScoreNames.Count; i++)
		{
			string[] namesSplit = highScoreNames[i].Split('|');

			if(namesSplit.Length > 1 && highScores.IndexOf(int.Parse(namesSplit[1])) < names.Length)
				names[highScores.IndexOf(int.Parse(namesSplit[1]))] = highScoreNames[i]; //Throws error occasionally when first player finishes input
		}

		for(int i = 0; i < highScoreNames.Count; i++)
			highScoreNames[i] = names[i];
	}

	private void RemoveFromNameList(int target)
	{
		for(int i = 0; i < highScoreNames.Count; i++)
		{
			string[] str = highScoreNames[i].Split('|');
			if(target == int.Parse(str[1]))
				highScoreNames.RemoveAt(i);
		}
	}

	private void TestUtility()
	{
		foreach(int i in dummyScores)
			AddHighScore(i);
	}

	private void PrintUtility()
	{
		int temp = 0;
		foreach(int i in highScores)
		{
			temp++;
			Debug.Log("HighScore Number " + temp + " is: " + i);
		}
	}
}
