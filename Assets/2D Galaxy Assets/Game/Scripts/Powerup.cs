using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private float _speed = 2;
    [SerializeField]
    private int powerupID; // 0 = Triple shot  1 = Speed boost  2 = Shields
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update () {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -5.6f)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if (other.tag == "Player")
        {
            // access the player
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 0.6f);

            if(player != null)
            {
                if (powerupID == 0)
                {
                    // enable triple shot
                    player.TripleShotPowerupOn();
                }
                else if (powerupID == 1)
                {
                    // enable speed boost
                    player.SpeedUpPowerupOn();
                }
                else if (powerupID == 2)
                {
                    // enable shield
                    player.SheildUpPowerupOn();
                }              
            }

            // destroy ourself
            Destroy(this.gameObject);
        }
    }
}
