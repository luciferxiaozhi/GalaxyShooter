using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void loadSinglePlayerGame()
    {
        Debug.Log("Single player game loading...");
        SceneManager.LoadScene("Single_Player");
    }

    public void loadCoopModeGame()
    {
        Debug.Log("Coop game loading...");
        SceneManager.LoadScene("Coop_Mode");
    }
}
