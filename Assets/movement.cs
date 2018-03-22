using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class movement : NetworkBehaviour {


    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateRight();
        }
        var x = Input.GetAxis("Horizontal");// * Time.deltaTime * 150.0f;
        var y = Input.GetAxis("Vertical");//;

        Vector2 movement = new Vector2(x, y);

        rb.AddForce(movement * speed);
    }

    private void RotateLeft()
    {
        rb.transform.rotation = new Quaternion(0,180,0,0);
    }
    private void RotateRight()
    {
        rb.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

}
