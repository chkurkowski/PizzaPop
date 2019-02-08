using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject startScreen;
    public GameObject titleScreen;
    public GameObject payoffScreen;
    public GameObject highScoreScreen;
    public bool leftStartScreen = false;
    public bool onPayoffScreen = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && !leftStartScreen)
        {
            titleScreen.SetActive(false);
            startScreen.SetActive(true);
            leftStartScreen = true;
        }

        if (Input.anyKeyDown && onPayoffScreen)
        {
            payoffScreen.SetActive(false);
            highScoreScreen.SetActive(true);
            onPayoffScreen = false;
        }
	}   
}
