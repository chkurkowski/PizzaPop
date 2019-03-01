using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject startScreen;
    public GameObject titleScreen;
    public GameObject payoffScreen;
    public GameObject highScoreScreen;
    public GameObject demoScreen;
    public GameObject gameUI;

    public Text countDownText;


    public bool leftStartScreen = false;
    public bool onPayoffScreen = false;
    public bool onDemoScreen = false;
    private bool onTitleScreen = true;
    public float secsSinceLastInput = 0;

	// Use this for initialization
	void Start () {
        onTitleScreen = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown && !leftStartScreen)
        {
            titleScreen.SetActive(false);
            startScreen.SetActive(true);
            leftStartScreen = true;
        }

        if (Input.anyKeyDown && onTitleScreen)
        {
            onTitleScreen = false;
            titleScreen.SetActive(false);

            StartCoroutine(CountDown());
     

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

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(8.0f);

        int countdown = 4;

        while (countdown > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countdown--;
            countDownText.text = countdown.ToString();
        }

        countDownText.text = "GO!";

        yield return new WaitForSeconds(0.3f);

        countDownText.text = "";

        gameUI.SetActive(true);
        GameManager.manager.StartGame();


    }
}
