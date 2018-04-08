using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour {

    private Rigidbody2D rb;
    public Transform rocketSpriteTransform;
    private bool updateAngle = true;
    public GameObject bulletSmoke;
    public CircleCollider2D destructionCircle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
