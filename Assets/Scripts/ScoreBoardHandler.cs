using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardHandler : MonoBehaviour
{
    // Reference for last score text.
    public Text previousScore;

    // Text References for names and highscores.
    public Text[] nameDisplayText = new Text[10];
    public Text[] scoreDisplayText = new Text[10];

    // Stuff for input Text.
    public InputField inputField;
    public Text inputText;

    // Updates Highscores
    void Start()
    {
        HighScores.setPerviousName(null);

        // Pulls from text file.
        TextFileHandler.ReadString();
        updateScoreBoard();

        // Displays your score.
        previousScore.text = "Your Score:\n" + HighScores.getPerviousScore();

        // Checks player score to see if it's a new high score.
        if (HighScores.isNewHighScore(HighScores.getPerviousScore()))
        {
            // This activates the input field to allow for player input. 
            inputField.gameObject.SetActive(true);
            // Call AcceptStringInput() in On End Edit (String) field in the inspector window on inputField;
        }
        else
        {
            // Deactivates the input field, saves the data in the text file, and updates the scoreboard.
            inputField.gameObject.SetActive(false);
            TextFileHandler.WriteString();
            updateScoreBoard();
        }
    }

    // Used to update the scoreboard with the values stored in the array.
    public void updateScoreBoard()
    {
        for (int index = 0; index < nameDisplayText.Length; index++)
        {
            nameDisplayText[index].text = "" + HighScores.getNames()[index];
            scoreDisplayText[index].text = "" + HighScores.getScores()[index];
        }
    }

    // Used to save the players name from the input field and add the score to the scoreboard.
    // The scores will then be saved to the text file and the input field will be deactivated.
    public void AcceptStringInput()
    {
        HighScores.setPerviousName(inputText.text);
        HighScores.addHighScore(HighScores.findHighScoreIndex(HighScores.getPerviousScore()), HighScores.getPerviousName(), HighScores.getPerviousScore());

        // Saves the data in the text file.
        TextFileHandler.WriteString();
        updateScoreBoard();

        inputField.text = null;

        inputField.gameObject.SetActive(false);
    }
}