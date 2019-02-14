using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingScoreHandler : MonoBehaviour {

	public static ToppingScoreHandler instance;

	#region Singleton

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	#endregion

	private int pepperoniScore = 120;
	private int greenPepperScore = 300;
	private int mushroomScore = 50;
	private int oliveScore = 45;
	private int onionScore = 80;

	public int GetScore(string topping)
	{
		switch(topping)
		{
			case "Pepperoni":
				return pepperoniScore;
			case "GreenPepper":
				return greenPepperScore;
			case "Mushroom":
				return mushroomScore;
			case "Olive":
				return oliveScore;
			case "Onion":
				return onionScore;
		}

		return pepperoniScore;
	}
}
