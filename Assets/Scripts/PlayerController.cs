using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        playerInput = new Vector2();
        MovementUpdate(playerInput);

        if (playerRigidbody.totalForce.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        //Debug.Log(playerInput);

        if (playerRigidbody.totalForce.y != 0)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
        
    }

    private void FixedUpdate()
    {
        {
            playerRigidbody.linearVelocityY+=gravity * Time.fixedDeltaTime;
            if(jumpTrigger)
            {
                playerRigidbody.linearVelocityY = initialJumpVelocity;
                jumpTrigger = false;
            }
        }
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");

        playerRigidbody.AddForce(playerInput * speed);     

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpTrigger = true;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }

    public FacingDirection GetFacingDirection()
    {
        if (playerRigidbody.totalForce.x > 0)
        {
            currentFacing = FacingDirection.right;
        }
        else
        {
            currentFacing = FacingDirection.left;
        }

        return currentFacing;
    }
}
