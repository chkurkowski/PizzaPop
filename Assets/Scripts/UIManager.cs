using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject startScreen;
    public GameObject titleScreen;
    public GameObject payoffScreen;
    public GameObject highScoreScreen;
    public GameObject demoScreen;
    public bool leftStartScreen = false;
    public bool onPayoffScreen = false;
    public bool onDemoScreen = false;
    public float secsSinceLastInput = 0;

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
            //payoffScreen.SetActive(false);
            //highScoreScreen.SetActive(true);
            //onPayoffScreen = false;
        }

        if (!Input.anyKeyDown && !leftStartScreen && secsSinceLastInput > 10)
        {
            titleScreen.SetActive(false);
            demoScreen.SetActive(true);
            onDemoScreen = true;
            leftStartScreen = true;
        }

        if (Input.anyKeyDown && onDemoScreen)
        {
            titleScreen.SetActive(true);
            demoScreen.SetActive(false);
            onDemoScreen = false;
            leftStartScreen = false;
            secsSinceLastInput = 0;
        }

        secsSinceLastInput += Time.deltaTime;
	}   
}
