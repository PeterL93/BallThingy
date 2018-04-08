using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartmanMove : MonoBehaviour {

    public float velocity;
    public GameObject bazooka;
    private Transform bodyTransform;
    private Rigidbody2D rb;
    private BoxCollider2D bc;



    // Use this for initialization
    void Start () {
        bc = GetComponentInParent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bodyTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.W))
        {

            bazooka.SetActive(true);
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = Vector2.right * velocity;
           

           
        }
        else if (!Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = -Vector2.right * velocity;
           

            
        }
        else
        {
            rb.velocity = Vector2.zero;
            
        }

    }
}
