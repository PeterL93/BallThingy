using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBazooka : MonoBehaviour {

    
    public float bulletMaxInitialVelocity; 
    public float maxTimeShooting; 

    public GameObject rocketPrefab; 

    private BoxCollider2D bc; 
    private Rigidbody2D rb; 

    private bool shooting;
    private bool targetting;
    private float timeShooting;
    private Vector2 shootDirection; 

    public GameObject shootingEffect; 
    public Transform gunTransform; 
    public Transform bodyTransform; 
    public Transform bulletInitialTransform; 


    // Use this for initialization
    void Start () {
        bc = GetComponentInParent<BoxCollider2D>();
        rb = GetComponentInParent<Rigidbody2D>();
        targetting = true;
	}
	
	// Update is called once per frame
	void Update () {
       
        if (targetting)
        {
           UpdateTargetting();
            UpdateShootDetection();
            if (shooting)
                UpdateShooting();
        }
    }

    void UpdateShootDetection()
    {
 
        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
            shootingEffect.SetActive(true);
            timeShooting = 0f;
        }
    }

    void UpdateShooting()
    {
        timeShooting += Time.deltaTime;
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            shooting = false;
            shootingEffect.SetActive(false);
            Shoot();
        }
        if (timeShooting > maxTimeShooting)
        {
            shooting = false;
            shootingEffect.SetActive(false);
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector2 playerToMouse = new Vector2(mousePosWorld.x - transform.position.x,
                                            mousePosWorld.y - transform.position.y);

        playerToMouse.Normalize();

        shootDirection = playerToMouse;
        Debug.Log("Shoot!");
        GameObject bullet = Instantiate(rocketPrefab);
        bullet.transform.position = bulletInitialTransform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletMaxInitialVelocity * (timeShooting / maxTimeShooting);
    }

    void UpdateTargetting()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector2 playerToMouse = new Vector2(mousePosWorld.x - transform.position.x,
                                            mousePosWorld.y - transform.position.y);

        playerToMouse.Normalize();

        float angle = Mathf.Asin(playerToMouse.y) * Mathf.Rad2Deg;
        if (playerToMouse.x < 0f)
            angle = 180 - angle;

        if (playerToMouse.x > 0f && bodyTransform.localScale.x > 0f)
        {
            bodyTransform.localScale = new Vector3(-bodyTransform.localScale.x, bodyTransform.localScale.y, 0f);
        }
        else if (playerToMouse.x < 0f && bodyTransform.localScale.x < 0f)
        {
            bodyTransform.localScale = new Vector3(-bodyTransform.localScale.x, bodyTransform.localScale.y, 0f);
        }

        gunTransform.localEulerAngles = new Vector3(0f, 0f, angle);
    }

}
