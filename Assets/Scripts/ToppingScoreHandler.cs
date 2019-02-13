using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingScoreHandler : MonoBehaviour {

	public static ToppingScoreHandler inst;

	#region Singleton

	void Awake()
	{
		if(inst == null)
		{
			inst = this;
		}
	}

	#endregion

	private float pepperoniScore = 100;
	private float greenPepperScore = 100;
	private float mushroomScore = 100;
	private float oliveScore = 100;
	private float onionScore = 100;

	public float GetScore(string topping)
	{
		switch(topping)
		{
			case "pepperoni":
				return pepperoniScore;
			case "greenpepper":
				return greenPepperScore;
			case "mushroom":
				return mushroomScore;
			case "olive":
				return oliveScore;
			case "onion":
				return onionScore;
		}
	}
}
