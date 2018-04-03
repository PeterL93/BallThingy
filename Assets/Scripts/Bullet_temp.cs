using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_temp : MonoBehaviour {

	// Use this for initialization
    void OnCollisionEnter2D(Collision2D coll)
    {
        var hit = coll.gameObject;
        var health = hit.GetComponent<Health_temp>();
        if (health != null)
        {
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
