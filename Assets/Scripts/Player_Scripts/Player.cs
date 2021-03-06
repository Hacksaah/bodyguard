﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float airMoveSpeed = 10.0f;
    public float MAXSPEED = 7.0f;
    public float JumpForce = 5.0f;
    public float fastFall = 50f;
    Rigidbody2D rb;

    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    // This collision function checks to see if the player is standing on a platform
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            isGrounded = true;           
        }
    }

    // Checks for jump input
    void CheckForJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * JumpForce);
            isGrounded = false;
        }
    }

    // Checks for fast fall inputC:\425Proj\bodyguard\Assets\Scripts\Player_Scripts\
    void CheckForFastFall()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 v = rb.velocity;
            v.y = -16;
            rb.velocity = v;
        }
    }

    // Player options for moving the player around
    void MovePlayer()
    {
        // Grounded Movement
        if (isGrounded)
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                if (rb.velocity.x > 0)
                    rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.right * -moveSpeed);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (rb.velocity.x < 0)
                    rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.right * moveSpeed);
            }

            CheckForJump();
        }
        // Air movement
        else
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                if (rb.velocity.x > 0)
                {
                    rb.AddForce(Vector2.right * -airMoveSpeed);
                }
                else
                    rb.AddForce(Vector2.right * -moveSpeed);
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                if (rb.velocity.x < 0)
                {
                    rb.AddForce(Vector2.right * airMoveSpeed);
                }
                else
                    rb.AddForce(Vector2.right * moveSpeed);
            }

            CheckForFastFall();
        }
    }


}
