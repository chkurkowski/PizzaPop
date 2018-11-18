using System;

[Serializable]

public class HighScores
{
    static String _perviousGameName;
    static int _perviousGameScore;
    static String[] _topTenNames = new string[10];
    static int[] _topTenScores = new int[10];

    // Return the pervious name.
    public static String getPerviousName()
    {
        return _perviousGameName;
    }

    // Returns the pervious score.
    public static int getPerviousScore()
    {
        return _perviousGameScore;
    }

    // Sets the name of the previous game.
    public static void setPerviousName(String name)
    {
        _perviousGameName = name;
    }

    // Sets the score of the previous game to a varable in the class.
    public static void setPerviousScore(int score)
    {
        _perviousGameScore = score;
    }

    // Accessor for highscore names.
    public static String[] getNames()
    {
        return _topTenNames;
    }

    // Accessor for highscore scores.
    public static int[] getScores()
    {
        return _topTenScores;
    }

    // Returns true if score is bigger than the smallest highscore.
    public static bool isNewHighScore(int score)
    {
        return _topTenScores[9] < score;
    }

    // Returns the index the the new score should be placed at.
    public static int findHighScoreIndex(int score)
    {
        for (int index = 8; index >= 0; index--)
        {
            if (_topTenScores[index] > score) return index + 1;
        }
        return 0;
    }

    // Adds the new score to the array along w/ a given name.
    public static void addHighScore(int index, String name, int score)
    {
        for (int moving = 9; moving > index; moving--)
        {
            _topTenNames[moving] = _topTenNames[moving - 1];
            _topTenScores[moving] = _topTenScores[moving - 1];
        }
        _topTenNames[index] = name;
        _topTenScores[index] = score;
    }
}