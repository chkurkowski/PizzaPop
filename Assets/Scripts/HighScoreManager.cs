using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hold eight high scores
//TODO: Build helper functions to test high score
public class HighScoreManager : MonoBehaviour {

	#region Singleton

	HighScoreManager inst;

	void Awake()
	{
		if(inst == null)
			inst = this;
		else
			Destroy(gameObject);
	}

	#endregion

	private List<int> highScores;

	void Start()
	{
		ParseHighScores();
	}

	public void UpdateHighScoreUI()
	{
		//Update the high score UI when you go to the High score page
	}

	public void CheckHighScore(int score)
	{
		//Check against the lowest of the high scores.
		//if it is a high score AddHighScore(score);
		if(score > highScores[7])
			AddHighScore(score);
	}

	private void AddHighScore(int score)
	{
		//Add the new score to the list
		highScores.Add(score);
		//Sort the list with list.Sort()
		highScores.Sort();
		//if it has more than 8 numbers cull the last
		if(highScores.Count > 8)
			highScores.RemoveAt(8);
		//Add all values into PlayerPrefs
		string scores = "";
		foreach(int i in highScores)
		{
			scores += highScores[i] + ",";
		}
		PlayerPrefs.SetString("HighScores", scores);
	}

	private void ParseHighScores()
	{
		string tempString = PlayerPrefs.GetString("HighScores");
		string[] subStrings = tempString.Split(',');
	}

	private void TestUtility()
	{
		
	}
}
