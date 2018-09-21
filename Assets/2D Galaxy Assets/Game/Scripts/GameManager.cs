using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;
    public bool gameOver = true;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject _coopPlayers;
    [SerializeField]
    private GameObject _pauseMenuPanel;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private Animator _pauseAnimator;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    void Update ()
    {
        if (gameOver == true)
        {
            ClearBattleField();

            _uiManager.ShowTitleScreen();
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (!isCoopMode)
                {
                    Instantiate(player, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    Instantiate(_coopPlayers, Vector3.zero, Quaternion.identity);
                }
                gameOver = false;
                _uiManager.HideTitleScreen();
                _spawnManager.StartSpawnRoutines();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
            }
        }
        
        // pause
        if (Input.GetKeyDown(KeyCode.P))
        {

            _pauseMenuPanel.SetActive(true);
            _pauseAnimator.SetBool("isPaused", true);
            Time.timeScale = 0;
        }
    }

    private void ClearBattleField()
    {
        // clear all enemies and powerups
        var enemy = GameObject.FindGameObjectsWithTag("Enemy");
        var powerup = GameObject.FindGameObjectsWithTag("Powerup");
        var coopPlayers = GameObject.FindGameObjectsWithTag("Player");

        if (powerup != null)
        {
            foreach (var item in powerup)
            {
                Destroy(item);
            }
        }

        if (enemy != null)
        {
            foreach (var item in enemy)
            {
                Destroy(item);
            }
        }

        if (coopPlayers != null)
        {
            foreach (var item in coopPlayers)
            {
                Destroy(item);
            }
        }
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
