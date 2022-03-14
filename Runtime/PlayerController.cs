using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Rigidbody2D rigidbody;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private float jumpPower = 10f;

    [SerializeField]
    private float wallJumpPower = 10f;

    [SerializeField]
    private float movSpeed = 10f;
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float dashSpeed = 20f;

    private bool dash = false;
    private bool jump = false;
    private float axisHorizontal;

    [SerializeField]
    private int maxAirJumps = 2;
    int currentJump = 0;

    [SerializeField]
    private float speedDamping = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dash |= Input.GetButtonDown("Dash");
        jump |= Input.GetButtonDown("Jump");
        axisHorizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        bool isGrounded = IsGrounded();

        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.left, 0.01f, groundLayerMask);
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right, 0.01f, groundLayerMask);

        bool collideLeft = raycastHitLeft.collider != null;
        bool collideRight = raycastHitRight.collider != null;

        int collide = (collideLeft) ? (-1) : ((collideRight) ? (1) : (0));

        float xVelocity = rigidbody.velocity.x;

        if (jump)
        {
            if (isGrounded)
            {
                rigidbody.velocity = Vector2.up * jumpPower;
                Debug.Log("JUMP");

                //GameObject.Find("SoundManager").GetComponent<SoundManager>().playerJump();
            } 
            else
            {
                if(collide != 0)
                {
                    Vector2 wallJump = Vector2.up * 2 + Vector2.right * collide;
                    wallJump.Normalize();

                    rigidbody.velocity += Vector2.up * wallJump.y * wallJumpPower;
                    Debug.Log("WALLJUMP");
                    xVelocity -= wallJump.x * wallJumpPower;

                    //GameObject.Find("SoundManager").GetComponent<SoundManager>().playerJump();

                } else
                {
                    if(currentJump < maxAirJumps)
                    {
                        rigidbody.velocity = Vector2.up * jumpPower;
                        currentJump++;
                        Debug.Log("AIRJUMP");

                        //GameObject.Find("SoundManager").GetComponent<SoundManager>().playerJump();
                    }
                }
            }
        }


        if (Math.Abs(axisHorizontal + collide) < 2 
            && (Mathf.Abs(xVelocity + Time.fixedDeltaTime * axisHorizontal * movSpeed) < Mathf.Abs(rigidbody.velocity.x) 
            || Mathf.Abs(xVelocity + Time.fixedDeltaTime * axisHorizontal * movSpeed) < maxSpeed))
        {
            xVelocity += Time.fixedDeltaTime * axisHorizontal * movSpeed;
        }

        if(Mathf.Abs(xVelocity) > maxSpeed)
        {
            xVelocity = Mathf.Lerp(xVelocity, maxSpeed * Mathf.Sign(xVelocity), Time.fixedDeltaTime * speedDamping);
        }
        rigidbody.velocity = new Vector2(xVelocity, Mathf.Clamp(rigidbody.velocity.y, -jumpPower, jumpPower));

        /*if (Mathf.Abs(xVelocity) < Mathf.Abs(rigidbody.velocity.x))
        {
            rigidbody.velocity = new Vector2(xVelocity, Mathf.Clamp(rigidbody.velocity.y, -jumpPower, jumpPower));
        } else if(Mathf.Abs(xVelocity) < maxSpeed)
        {
            rigidbody.velocity = new Vector2(Mathf.Clamp(xVelocity, -maxSpeed, maxSpeed), Mathf.Clamp(rigidbody.velocity.y, -jumpPower, jumpPower));
        }*/

        if (dash && axisHorizontal != 0)
        {
            rigidbody.velocity += Vector2.right * axisHorizontal * dashSpeed;
            Debug.Log("DASH");
        }

        if (isGrounded && axisHorizontal == 0) {
            rigidbody.velocity *= Vector2.up;
        }

        if(isGrounded | collide != 0)
        {
            currentJump = 0;
        }


        if(axisHorizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(axisHorizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }


        jump = false;
        dash = false;
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.01f, groundLayerMask);
        return raycastHit.collider != null;
    }
}
