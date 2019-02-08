using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TextFileHandler : MonoBehaviour
{
    public TextAsset Asset;
    [MenuItem("Tools/Write file")]
    public static void WriteString()
    {
        string path = "Assets/Resources/SavedScores.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        for (int index = 0; index < HighScores.getNames().Length; index++)
        {
            writer.WriteLine(HighScores.getNames()[index]);
            writer.WriteLine(HighScores.getScores()[index]);
        }
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load<TextAsset>("test");

        //Print the text from the file
        Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    public static void ReadString()
    {
        string path = "Assets/Resources/SavedScores.txt";
        string tempStr;
        int tempInt;

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        for (int index = 0; index < HighScores.getNames().Length; index++)
        {
            tempStr = reader.ReadLine();
            tempInt = Int32.Parse(reader.ReadLine());
            HighScores.addHighScore(index, tempStr, tempInt);
        }
        reader.Close();
    }
}