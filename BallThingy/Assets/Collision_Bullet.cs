using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Collision_Bullet : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            health otherScript = col.gameObject.GetComponent<health>();
            otherScript.takeDamage(10);
            this.gameObject.SetActive(false);
        }
    }
}
