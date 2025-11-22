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
    public float terminalSpeed=-20f;
    public float coyoteTime;
    private float coyoteTimer=0f;


    bool isWalking;
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
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        playerInput = new Vector2();
        MovementUpdate(playerInput);

        //Debug.Log(playerInput);

        bool hitGround = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGrounded = hitGround;
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.red);

        if(isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
        
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

        if(Input.GetKeyDown(KeyCode.Space) && (isGrounded ||coyoteTimer>0f))
        {
            jumpTrigger = true;
        }
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
