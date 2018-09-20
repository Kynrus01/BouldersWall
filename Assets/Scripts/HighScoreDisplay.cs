using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] Text[] highScore;
    [SerializeField] Text[] highScoreNames;
    [SerializeField] int currentHS = 0;
    [SerializeField] float blinkDelay;
    bool canEnterName = false;
    int scoreTemp1;
    int scoreTemp2;
    string nameTemp1;
    string nameTemp2;
    int setScore = 0;
    int loopNumber = 1;
    int loopName = 1;
    bool blinkIsOn;

    void Start()
    {
        //PlayerPrefs.SetInt("CurrentHighScore", currentHS);
        for (int i = 0; i < 9; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i) == false)
            {
                PlayerPrefs.SetInt("HighScore" + i, 0);
            }
            if (PlayerPrefs.HasKey("HighScoreName" + i) == false)
            {
                PlayerPrefs.SetString("HighScoreName" + i, "-----");
            }
        }
        for (int i = 0; i < 10; i++)
        {
            highScoreNames[i].text = PlayerPrefs.GetString("HighScoreName" + i);
        }
        for (int i = 0; i < 10; i++)
        {
            highScore[i].text = PlayerPrefs.GetInt("HighScore" + i).ToString();
        }
        StartCoroutine(Blink());
    }

    void Update()
    {
        NameSelect();
        NameDisplay();
        ScoreSet();
            UpdateDisplay();
    }

    void ScoreSet()
    {
        if (setScore == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                if (PlayerPrefs.GetInt("CurrentHighScore") > PlayerPrefs.GetInt("HighScore" + i) && setScore == 0)
                {
                    currentHS = i;
                    scoreTemp1 = PlayerPrefs.GetInt("HighScore" + currentHS);
                    scoreTemp2 = PlayerPrefs.GetInt("HighScore" + (currentHS + 1));
                    PlayerPrefs.SetInt("HighScore" + currentHS, PlayerPrefs.GetInt("CurrentHighScore"));
                    PlayerPrefs.SetInt("HighScore" + (currentHS + 1), scoreTemp1);

                    nameTemp1 = PlayerPrefs.GetString("HighScoreName" + currentHS);
                    nameTemp2 = PlayerPrefs.GetString("HighScoreName" + (currentHS + 1));
                    PlayerPrefs.SetString("HighScoreName" + currentHS, "-----");
                    PlayerPrefs.SetString("HighScoreName" + (currentHS + 1), nameTemp1);
                    canEnterName = true;
                    setScore = 1;
                }
            }
        }
        else if (setScore == 1)
        {
            for (int i = currentHS + 1; i < 10; i++)
            {
                if (loopNumber == 1)
                {
                    scoreTemp1 = PlayerPrefs.GetInt("HighScore" + (1 + i));
                    PlayerPrefs.SetInt("HighScore" + (1 + i), scoreTemp2);
                    loopNumber = 2;
                }
                else if (loopNumber == 2)
                {
                    scoreTemp2 = PlayerPrefs.GetInt("HighScore" + (1 + i));
                    PlayerPrefs.SetInt("HighScore" + (1 + i), scoreTemp1);
                    loopNumber = 1;
                }

                if (loopName == 1)
                {
                    nameTemp1 = PlayerPrefs.GetString("HighScoreName" + (1 + i));
                    PlayerPrefs.SetString("HighScoreName" + (1 + i), nameTemp2);
                    loopName = 2;
                }
                else if (loopName == 2)
                {
                    nameTemp2 = PlayerPrefs.GetString("HighScoreName" + (1 + i));
                    PlayerPrefs.SetString("HighScoreName" + (1 + i), nameTemp1);
                    loopName = 1;
                }
            }
            setScore = 2;
        }
    }


    //**********************************************************
    int currentLetterInAlphabet = 0;
    int currentLetterInName = 0;
    string[] alphabet = new string[] {"-","_","A","B","C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "!", "#", "$", "&", "*", "^", ":", ";", "?", "(", ")" };
    string[] name = new string[] {"-", "-", "-", "-", "-" };

    void NameSelect()
    {
        if (canEnterName == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                name[currentLetterInName] = alphabet[currentLetterInAlphabet];
                PlayerPrefs.Save();
                SceneManager.LoadScene("CreditScene");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                name[currentLetterInName] = alphabet[currentLetterInAlphabet];
                StopAllCoroutines();
                StartCoroutine(Blink());
                if (currentLetterInName == 4)
                {
                    name[currentLetterInName] = alphabet[currentLetterInAlphabet];
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("CreditScene");
                }
                else
                {
                    currentLetterInName += 1;
                }
                if (name[currentLetterInName] == alphabet[0])
                {
                    currentLetterInAlphabet = 0;
                }
                else
                {
                    for (int i = 0; i < alphabet.Length; i++)
                    {
                        if (name[currentLetterInName] == alphabet[i])
                        {
                            currentLetterInAlphabet = i;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                name[currentLetterInName] = alphabet[currentLetterInAlphabet];
                StopAllCoroutines();
                StartCoroutine(Blink());
                if (currentLetterInName != 0)
                {
                    currentLetterInName -= 1;
                }
                if (name[currentLetterInName] == alphabet[0])
                {
                    currentLetterInAlphabet = 0;
                }
                else
                {
                    for (int i = 0; i < alphabet.Length; i++)
                    {
                        if(name[currentLetterInName] == alphabet[i])
                        {
                            currentLetterInAlphabet = i;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StopAllCoroutines();
                StartCoroutine(Blink());
                if (currentLetterInAlphabet == alphabet.Length - 1)
                {
                    currentLetterInAlphabet = 0;
                }
                else
                {
                    currentLetterInAlphabet += 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StopAllCoroutines();
                StartCoroutine(Blink());
                if (currentLetterInAlphabet == 0)
                {
                    currentLetterInAlphabet = alphabet.Length - 1;
                }
                else
                {
                    currentLetterInAlphabet -= 1;
                }
            }
            if(blinkIsOn == false)
            {
                name[currentLetterInName] = alphabet[currentLetterInAlphabet];
            }
            else
            {
                name[currentLetterInName] = " ";
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                name[currentLetterInName] = alphabet[currentLetterInAlphabet];
                PlayerPrefs.Save();
                SceneManager.LoadScene("CreditScene");
            }
        }
    }
    IEnumerator Blink()
    {
        blinkIsOn = false;
        yield return new WaitForSeconds(blinkDelay);
        blinkIsOn = true;
        yield return new WaitForSeconds(blinkDelay);
        StartCoroutine(Blink());
    }

    //***********************************************************************************************
    void NameDisplay()
    {
        if (canEnterName == true)
        {
            PlayerPrefs.SetString("HighScoreName" + currentHS, name[0] + name[1] + name[2] + name[3] + name[4]);
        }
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < 10; i++)
        {
            highScoreNames[i].text = PlayerPrefs.GetString("HighScoreName" + i);
        }
        for (int i = 0; i < 10; i++)
        {
            highScore[i].text = PlayerPrefs.GetInt("HighScore" + i).ToString("###,###,##0");
        }
    }

}
