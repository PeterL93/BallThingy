using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{

    public GameObject bazooka;
    public float speed = 1f;
    public float jumpSpeed = 3f;
    public bool groundCheck;
    public bool isSwinging;
    private SpriteRenderer playerSprite;
    private Rigidbody2D rBody;
    private bool isJumping;
    
    private float jumpInput;
    private float horizontalInput;

    public Vector2 ropeHook;
    public float swingForce = 4f;
    

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rBody = GetComponent<Rigidbody2D>();
       
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            
            bazooka.SetActive(true);
        }

        if (!isLocalPlayer)
        {
            return;
        }

            jumpInput = Input.GetAxis("Jump");
            horizontalInput = Input.GetAxis("Horizontal");

            groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y  - 0.04f), Vector2.down, 0.025f);
        
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (horizontalInput < 0f || horizontalInput > 0f)
        {
          
            playerSprite.flipX = horizontalInput < 0f;
            if (isSwinging)
            {


            
                var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

            
                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                    Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                rBody.AddForce(force, ForceMode2D.Force);
            }
            else
            {
           
                if (groundCheck)
                {
                    var groundForce = speed * 2f;
                    rBody.AddForce(new Vector2((horizontalInput * groundForce - rBody.velocity.x) * groundForce, 0));
                    rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);
                }
            }
        }
       


        if (!isSwinging)
        {
            if (!groundCheck) return;

            isJumping = jumpInput > 0f;
            if (isJumping)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
            }
        }
    }
}
