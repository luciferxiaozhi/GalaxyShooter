using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleLaserDeletion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, 1, 0) *  Time.deltaTime);
        if (this.transform.position.y >= 6.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
