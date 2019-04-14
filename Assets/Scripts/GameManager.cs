using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour 
{
    private int player1Score;
    private int player2Score;

    [SerializeField]
    private float player1Combo = 1f;
    [SerializeField]
    private float player2Combo = 1f;
    [SerializeField]
    private float player1Timer = 0;
    [SerializeField]
    private float player2Timer = 0;
    private const float PLAYERCOMBOMAXTIME = 3f;

    [SerializeField]
    private float timeLeft;

    [SerializeField]
    private TextMeshProUGUI player1ScoreText;

    [SerializeField]
    private TextMeshProUGUI player1ScoreTextOutline;

    [SerializeField]
    private TextMeshProUGUI player2ScoreText;

    [SerializeField]
    private TextMeshProUGUI player2ScoreTextOutline;

    [SerializeField]
    private Text timer;

    [HideInInspector]
    public bool gameStarted = false;

    public GameObject endGamePanel;

    public Image leftFill;
    public Image rightFill;
    public Text player1ComboText;
    public Text player2ComboText;

    [SerializeField]
    private Image timerFront;

    [SerializeField]
    private Image timerCircle;

    public static GameManager manager;

    private UIManager UIManagerScript;

    [SerializeField]
    private GameObject highScorePanel;

    private void Awake()
    {
        manager = this;
    }

    private void Start()
    {
        UIManagerScript = GameObject.Find("GameCanvas").GetComponent<UIManager>();
    }

    public void StartGame()
    {
        timeLeft = 47f; // CHANGE THIS BACK TO 30

        gameStarted = true;
        
        AudioManager.instance.Play("Click");
        AudioManager.instance.Stop("SplashMusic");
        AudioManager.instance.Play("Gameplay Music");

       // Cursor.visible = false; //makes mouse cursor invisible on start of game

        foreach (PlayerBehaviour p in FindObjectsOfType<PlayerBehaviour>())
        {
            p.resetPosition();
        }

        foreach (CharacterBehaviour c in FindObjectsOfType<CharacterBehaviour>())
        {
            c.resetCharacter();
        }
    }

    public void EndGame() 
    {
        Cursor.visible = true;
        gameStarted = false;
        endGamePanel.SetActive(true);
        StartCoroutine(EnableHighScoreScreen());
        UIManagerScript.onPayoffScreen = true;
    }

    public void ResetGame()
    {
        AudioManager.instance.Stop("Results Music");
        player1Score = 0;
        player2Score = 0;

        endGamePanel.SetActive(false);
        StartGame();
    }

    public void setPlayer1Score(int score)
    {
        player1Score = score;
    }

    public int getPlayer1Score()
    {
        return player1Score;
    }

    public void setPlayer2Score(int score)
    {
        player2Score = score;
    }

    public int getPlayer2Score()
    {
        return player2Score;
    }

    public float GetPlayer1Combo()
    {
        return player1Combo;
    }

    public void SetPlayer1Combo(float multiplier)
    {
        player1Combo += multiplier;
    }

    public float GetPlayer2Combo()
    {
        return player2Combo;
    }

    public void SetPlayer2Combo(float multiplier)
    {
        player2Combo += multiplier;
    }

    // Update is called once per frame
    void Update () 
    {
        if (gameStarted)
        {
            timeLeft -= Time.deltaTime;
            timerFront.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Lerp(0f, -360f, (timeLeft / 47))));
            timerCircle.fillAmount = (float)(timeLeft / 47.75f);
            timer.text = ((int)timeLeft).ToString();

            player1ScoreText.text = player1ScoreTextOutline.text = "Rosso: " + player1Score.ToString();

            if (GameManager.manager.GetPlayer1Combo() > 1.0f)
            {
                player1ScoreTextOutline.text = player1ScoreText.text += " X " + GameManager.manager.GetPlayer1Combo();
            }

            player2ScoreText.text = player2ScoreTextOutline.text = "Verde: " + player2Score.ToString();

            if (GameManager.manager.GetPlayer2Combo() > 1.0f)
            {
                player2ScoreTextOutline.text = player2ScoreText.text += " X " + GameManager.manager.GetPlayer2Combo();
            }

            HandlePlayerComboUI();

            if (timeLeft <= 0)
            {
                AudioManager.instance.Stop("Gameplay Music");
                AudioManager.instance.Play("End Game Chime");
                EndGame();
            }

            if(player1Combo > 1 && player1Timer == 0)
                StartCoroutine("Player1Timer");
            if(player2Combo > 1 && player2Timer == 0)
                StartCoroutine("Player2Timer");
        }
    }

    private IEnumerator Player1Timer()
    {
        while(player1Combo > 1)
        {
            player1Timer += Time.deltaTime;
            if(player1Timer >= PLAYERCOMBOMAXTIME)
            {
                player1Combo = 1;
                player1Timer = 0;
            }
            yield return null;
        }
    }

    private IEnumerator Player2Timer()
    {
        while(player2Combo > 1)
        {
            player2Timer += Time.deltaTime;
            if(player2Timer >= PLAYERCOMBOMAXTIME)
            {
                player2Combo = 1;
                player2Timer = 0;
            }
            yield return null;
        }
    } 

    private IEnumerator EnableHighScoreScreen()
    {
        yield return new WaitForSeconds(8f);
        endGamePanel.SetActive(false);
        highScorePanel.SetActive(true);
        yield return new WaitForSeconds(6f);
        ResetScene();
        yield return null;
    }

    private void HandlePlayerComboUI()
    {
        float p1;
        float p2;
        if(player1Timer != 0)
            p1 = (PLAYERCOMBOMAXTIME - player1Timer) / PLAYERCOMBOMAXTIME;
        else
            p1 = 0;

        if(player2Timer != 0)
            p2 = (PLAYERCOMBOMAXTIME - player2Timer) / PLAYERCOMBOMAXTIME; 
        else
            p2 = 0;

        //leftFill.fillAmount = p1;
        //rightFill.fillAmount = p2;
        //player1ComboText.text = player1Combo.ToString();
        //player2ComboText.text = player2Combo.ToString();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float GetSecondsLeft()
    {
        return timeLeft;
    }
}
