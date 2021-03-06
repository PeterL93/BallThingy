﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int shootvalue;
    private Rigidbody2D rb;
    public int speed;
    private bool left = false; 
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		
	}

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        if (left){
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootvalue*-1, 0);
        } else{
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootvalue, 0);
        }
        NetworkServer.Spawn(bullet);
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    // Update is called once per frame

    void Update()
    {
        if(!isLocalPlayer)  {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            left = true;
            transform.rotation = new Quaternion(0, 180, 0, 0);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            left = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        var x = Input.GetAxis("Horizontal");// * Time.deltaTime * 150.0f;
        var y = Input.GetAxis("Vertical");//;

        Vector2 movement = new Vector2(x, y);

        rb.AddForce(movement * speed);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }


}
