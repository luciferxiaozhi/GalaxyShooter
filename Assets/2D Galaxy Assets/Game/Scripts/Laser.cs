using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;
	
	// Update is called once per frame
	void Update () {
        // I move up at 10 speed.
        transform.Translate(new Vector3(0, 1, 0) * _speed * Time.deltaTime);

        if (this.transform.position.y >= 6.0f)
        {
            Destroy(this.gameObject);
        }
	}
}
