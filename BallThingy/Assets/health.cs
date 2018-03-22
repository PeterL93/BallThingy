using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class health : NetworkBehaviour {

    public int hp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void takeDamage(int damage)
    {
        hp = hp - damage;
    }
}
