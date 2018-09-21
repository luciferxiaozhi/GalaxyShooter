using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyShip;
    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;

    // Use this for initialization
    void Awake () {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    public void StartSpawnRoutines()
    {
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(powerupSpawnRoutine());
    }
	
	IEnumerator enemySpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            yield return new WaitForSeconds(2.0f);
            Instantiate(_enemyShip, new Vector3(Random.Range(-8.3f, 8.3f), 6.4f, 0), Quaternion.identity);
        }
    }

    IEnumerator powerupSpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-8.3f, 8.3f), 6.4f, 0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }
}
