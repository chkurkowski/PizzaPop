﻿using System.Collections;
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
		ParseHighScores();
	}

	void OnEnable()
	{
		UpdateHighScoreUI();
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
			PlayerPrefs.SetString("HighScores", "");
	}

	public void UpdateHighScoreUI()
	{
		ParseHighScores();
		for(int i = 0; i < highScores.Count; i++)
		{
			if(highScores.Count > 0)
				scoreDisplay[i].text = highScores[i].ToString();
			else
				scoreDisplay[i].text = "-";
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
			TypingOnGuns(player);
			AddHighScore(score);
			return;
		}
		else if(score > highScores[7])
		{
			//Activate Panel for Typing
			TypingOnGuns(player);
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
			highScores.RemoveAt(8);
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

	public void AddHighScoreName(string name)
	{
		
	}

	//Add the name string in with the score and then parse the name out seperately in a second split.
	private void ParseHighScores()
	{
		// Debug.Log("Parsing High Scores");
		highScores.Clear();
		string tempString = PlayerPrefs.GetString("HighScores");
		string[] subStrings = tempString.Split(',');

		if(subStrings.Length != 0 )
		{
			//Add second split here
			foreach(string s in subStrings)
				// if(!highScores.Contains(int.Parse(s)))
					AddHighScore(int.Parse(s));
		}
	}

	public void TypingOnGuns(int player)
	{
		inputBoard.SetActive(true);

		if(player == 1)
			inputBoard.GetComponent<HighScoreInput>().PlayerOneHighScore = true;
		else if(player == 2)
			inputBoard.GetComponent<HighScoreInput>().PlayerTwoHighScore = true;
		else
			manager.InvokeFinalText();
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
