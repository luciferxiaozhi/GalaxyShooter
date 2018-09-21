using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject beginTitle;
    public GameObject beginTitle_text;
    public GameObject opertionReminder_text;
    public Text scoreText, bestText;
    public int score, bestScore;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestText.text = "Best: " + bestScore;
        scoreText.text = "Score: 0";
    }

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }
    
    public void CheckForBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
            bestText.text = "Best: " + bestScore;
        }
    }

    public void ShowTitleScreen()
    {
        beginTitle.SetActive(true);
        beginTitle_text.SetActive(true);
        opertionReminder_text.SetActive(true);
    }

    public void HideTitleScreen()
    {
        beginTitle.SetActive(false);
        beginTitle_text.SetActive(false);
        opertionReminder_text.SetActive(false);
        scoreText.text = "Score: 0";
        score = 0;
    }

    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    public void BackToMainMenu()
    {
        ResumePlay();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
    }

}