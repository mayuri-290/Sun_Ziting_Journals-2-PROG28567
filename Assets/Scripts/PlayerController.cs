using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D playerRigidbody;
    private FacingDirection currentFacing = FacingDirection.right;
    Vector2 playerInput;
    public float apexTime, apexHeight;
    public float gravity, initialJumpVelocity;
    private bool jumpTrigger;
    private bool jumpTest;
    public float terminalSpeed=-20f;
    public float coyoteTime;
    private float coyoteTimer=0f;
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    private Vector3 currentVelocity;


    bool isGrounded;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        //set the gravity
        gravity = -2 * apexHeight / (Mathf.Pow(apexTime,2));
        //set the initial jump velocity
        initialJumpVelocity = 2 * apexHeight / apexTime;
        playerRigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - 0.51f);
        bool wasGrounded = isGrounded;
        
        bool hitGround = Physics2D.Raycast(pos, Vector2.down, 0.2f);
        isGrounded = hitGround;
        Debug.DrawRay(pos,Vector2.down * 0.2f, Color.red);

          if(isGrounded&& !wasGrounded)
        {
            jumpTest = false;
            coyoteTimer = coyoteTime;
        }
        else if(!isGrounded)
        {
            coyoteTimer -= Time.deltaTime;
        }
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        playerInput = new Vector2();
        MovementUpdate(playerInput);
        //Debug.Log(playerInput);     
    }

    private void FixedUpdate()
    {
            playerRigidbody.linearVelocityY+=gravity * Time.fixedDeltaTime;

            if(jumpTrigger)
            {
                playerRigidbody.linearVelocityY = initialJumpVelocity;
                jumpTrigger = false;
            } 
            if(playerRigidbody.linearVelocityY < terminalSpeed)
            {
                playerRigidbody.linearVelocityY = terminalSpeed;
            }
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerRigidbody.AddForce(playerInput * speed);     

    if (Input.GetKeyDown(KeyCode.Space) && !jumpTest && (isGrounded || coyoteTimer > 0f))        
        {
            jumpTrigger = true;
            jumpTest = true;
        }
//------------------Class code------------------//
        // if(playerInput.x != 0)
        // {
        //    currentVelocity += playerInput.x * acceleration * Vector3.right * Time.deltaTime;
        //    if(Mathf.Abs(currentVelocity.x) > maxSpeed)
        //    {
        //     currentVelocity = new Vector3(Mathf.Sign(currentVelocity.x) * maxSpeed, currentVelocity.y);
        //    }
        // }
        // else
        // {
        //     Vector3 amountWeWantToChange = deceleration * currentVelocity.normalized * Time.deltaTime;

        //     if(amountWeWantToChange.magnitude > currentVelocity.x)
        //     {
        //         currentVelocity.x = 0;
        //     }
        //     else
        //     {
        //         currentVelocity -= amountWeWantToChange;
        //     }
        // }
//------------------Class code------------------//
     }

    public bool IsWalking()
    {
        return Mathf.Abs(playerRigidbody.linearVelocityX) > 0.1f;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }

    public FacingDirection GetFacingDirection()
    {
        float x =playerRigidbody.linearVelocityX;
        if(x>0)
        {
            currentFacing = FacingDirection.right;
        }
        else if(x<0)
        {
            currentFacing = FacingDirection.left;
        }
        return currentFacing;
    }
}