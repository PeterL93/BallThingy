using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {


    private Rigidbody2D rb;

    public Transform spriteTransform;

    private bool updateAngle = true;

    public GameObject rocketSmoke;

    public CircleCollider2D circleCollider;
    


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(5f, 10f);
    }


    void Update()
    {
        if (updateAngle)
        {
            Vector2 dir = new Vector2(rb.velocity.x, rb.velocity.y);

            dir.Normalize();
            float angle = Mathf.Asin(dir.y) * Mathf.Rad2Deg;
            if (dir.x < 0f)
            {
                angle = 180 - angle;
            }


            spriteTransform.localEulerAngles = new Vector3(0f, 0f, angle + 45f);
        }
    }

   void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.collider.tag == "Ground")
        {
            updateAngle = false;
            rocketSmoke.SetActive(false);

            Destroy(gameObject);
        }
    } 

}
