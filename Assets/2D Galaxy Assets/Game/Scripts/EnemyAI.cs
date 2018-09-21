using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private GameObject instantiatedObj;
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _clip;    

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(Random.Range(-8.3f, 8.3f), 6.4f, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update () {


        // Move down the enemy
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);

        // respawn
        if(transform.position.y <= -6.44f)
        {
            transform.position = new Vector3(Random.Range(-8.3f, 8.3f), 6.4f, 0);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "laser")
        {
            Laser laser = other.GetComponent<Laser>();
            instantiatedObj = (GameObject)Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(instantiatedObj, 3.0f);

            // Update score UI
            _uiManager.UpdateScore();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 0.5f);

            Destroy(this.gameObject);
            Destroy(laser.gameObject);
        }
        else if(other.tag == "Player")
        {
            // Update score UI
            _uiManager.UpdateScore();

            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            instantiatedObj = (GameObject) Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 0.5f);
            Destroy(instantiatedObj, 3.0f);

            Destroy(this.gameObject);
        }
    }
    
}
