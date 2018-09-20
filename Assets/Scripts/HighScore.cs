using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {

	void Start ()
    {
        PlayerPrefs.SetInt("HighScore0", 10);
        PlayerPrefs.SetInt("HighScore1", 9);
        PlayerPrefs.SetInt("HighScore2", 8);
        PlayerPrefs.SetInt("HighScore3", 7);
        PlayerPrefs.SetInt("HighScore4", 6);
        PlayerPrefs.SetInt("HighScore5", 5);
        PlayerPrefs.SetInt("HighScore6", 4);
        PlayerPrefs.SetInt("HighScore7", 3);
        PlayerPrefs.SetInt("HighScore8", 2);
        PlayerPrefs.SetInt("HighScore9", 1);
        PlayerPrefs.SetString("HighScoreName0", "-----");
        PlayerPrefs.SetString("HighScoreName1", "-----");
        PlayerPrefs.SetString("HighScoreName2", "-----");
        PlayerPrefs.SetString("HighScoreName3", "-----");
        PlayerPrefs.SetString("HighScoreName4", "-----");
        PlayerPrefs.SetString("HighScoreName5", "-----");
        PlayerPrefs.SetString("HighScoreName6", "-----");
        PlayerPrefs.SetString("HighScoreName7", "-----");
        PlayerPrefs.SetString("HighScoreName8", "-----");
        PlayerPrefs.SetString("HighScoreName9", "-----");
        PlayerPrefs.SetInt("CurrentHighScore", 0);
        PlayerPrefs.Save();
    }
}
