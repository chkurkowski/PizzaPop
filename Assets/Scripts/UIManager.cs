using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public GameObject startScreen;
    public GameObject titleScreen;
    public GameObject payoffScreen;
    public GameObject highScoreScreen;
    public GameObject demoScreen;
    public GameObject gameUI;

    public GameObject TutorialPizzas;

    public TMP_Text titleScreenMessage;

    public TMP_Text player1ReadyUpText;
    public Image player1ReadyUpImage;

    public TMP_Text player2ReadyUpText;
    public Image player2ReadyUpImage;


    public Text countDownText;

    public GameObject introVideo;
    private float player1ReadyUp;
    private float player2ReadyUp;
    bool videoSkipped = false;


    public bool leftStartScreen = false;
    public bool onPayoffScreen = false;
    private bool onTitleScreen = true;
    private bool onDemoScreen = false;
    public float secsSinceLastInput = 0;

	// Use this for initialization
	void Start () {
        onTitleScreen = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("P1SwitchRight") || Input.GetButton("P1SwitchLeft"))
        {
            player1ReadyUp += Time.deltaTime;
            player1ReadyUpImage.fillAmount = player1ReadyUp;

            if (player1ReadyUp >= 1.0f)
            {
                player1ReadyUpText.text = "Ready!";
                player1ReadyUpText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ready!";

                player1ReadyUpText.gameObject.GetComponent<flash>().StopFlashing();
                player1ReadyUpText.transform.GetChild(0).GetComponent<flash>().StopFlashing();
                player1ReadyUpImage.enabled = false;

            }
            else
            {
                player1ReadyUpText.enabled = false;
                player1ReadyUpText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            }

            //Debug.Log("Player 1 Ready up " + player1ReadyUp.ToString());
        }
        else if (player1ReadyUp < 1)
        {
            player1ReadyUp = 0f;
            player1ReadyUpImage.fillAmount = player1ReadyUp;
        }

        if (Input.GetButton("P2SwitchRight") || Input.GetButton("P2SwitchLeft"))
        {
            player2ReadyUp += Time.deltaTime;
            player2ReadyUpImage.fillAmount = player2ReadyUp;


            if (player2ReadyUp >= 1.0f)
            {
                player2ReadyUpText.text = "Ready!";
                player2ReadyUpText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ready!";

                player2ReadyUpText.gameObject.GetComponent<flash>().StopFlashing();
                player2ReadyUpText.transform.GetChild(0).GetComponent<flash>().StopFlashing();
                player2ReadyUpImage.enabled = false;
            }
            else
            {
                player2ReadyUpText.enabled = false;
                player2ReadyUpText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;

            }
        }
        else if (player2ReadyUp < 1)
        {
            player2ReadyUp = 0f;
            player2ReadyUpImage.fillAmount = player2ReadyUp;
        }

        if (player1ReadyUp >= 1.0f && player2ReadyUp >= 1.0f)
        {
            if (!videoSkipped && introVideo.activeSelf)
            {
                videoSkipped = true;
                skipVideo();
            }
        }



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

            introVideo.SetActive(true);

            titleScreenMessage.gameObject.SetActive(false);

            StartCoroutine(CountDown(22));

            //payoffScreen.SetActive(false);
            //highScoreScreen.SetActive(true);
            //onPayoffScreen = false;
        }

        if (!Input.anyKeyDown && !leftStartScreen && secsSinceLastInput > 10)
        {
            titleScreen.SetActive(false);
            if (Random.Range(0, 2) == 1)
            {
                demoScreen.SetActive(true);
                onTitleScreen = false;
                onDemoScreen = true;
            }
            else
            {
                highScoreScreen.SetActive(true);
                onTitleScreen = false;
                onDemoScreen = true;
            }
            
            leftStartScreen = true;
        }

        if (Input.anyKeyDown && onDemoScreen)
        {
            onTitleScreen = true;
            onDemoScreen = false;
            titleScreen.SetActive(true);
            demoScreen.SetActive(false);
            highScoreScreen.SetActive(false);
            leftStartScreen = false;
            secsSinceLastInput = 0;
        }

        secsSinceLastInput += Time.deltaTime;
	}

    private IEnumerator CountDown(float timeToWait)
    {
        //spawn the pizzas roughly after 8 seconds;

        if (timeToWait != 0.1f)
        {
            yield return new WaitForSeconds(8.0f);
            TutorialPizzas.SetActive(true);

            Debug.Log("Spawning Pizzas");

            yield return new WaitForSeconds(timeToWait - 8);
            TutorialPizzas.SetActive(false);

            videoSkipped = true;
        }
        //int countdown = 4;

        //while (countdown > 0)
        //{
        //    yield return new WaitForSeconds(1.0f);
        //    countdown--;
        //    countDownText.text = countdown.ToString();
        //}

        introVideo.SetActive(false);
        countDownText.text = "ORDER UP!";

        yield return new WaitForSeconds(0.3f);

        countDownText.text = "";

        gameUI.SetActive(true);
        GameManager.manager.StartGame();
    }

    private void skipVideo()
    {
        introVideo.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(CountDown(0.1f));
    }
}
