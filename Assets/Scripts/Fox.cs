﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public float jumpSpeed = 250.0f;
    public float maxJumpTime = 0.15f;
    public float maxGravityScale = 3.0f;

    [SerializeField] Collider2D groundCollider;
    [SerializeField] Collider2D airCollider;

    Rigidbody2D rigidBody;
    Animator    animator;
    float       timeOfJump;
    float       hAxis;
    bool        jumpPressed;

    bool isOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Ground"));

            return (collider != null);
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timeOfJump = -1000.0f;
    }

    void FixedUpdate()
    {
        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity = new Vector2(hAxis * moveSpeed, currentVelocity.y);

        bool grounded = isOnGround;

        if (jumpPressed)
        {
            if (grounded)
            {
                currentVelocity.y = jumpSpeed;
                timeOfJump = Time.time;
                rigidBody.gravityScale = 1.0f;
            }
            else
            {
                float elapsedJumpTime = (Time.time - timeOfJump);

                if (elapsedJumpTime < maxJumpTime)
                {
                    rigidBody.gravityScale = 1.0f;
                }
                else
                {
                    rigidBody.gravityScale = maxGravityScale;
                }
            }
        }
        else
        {
            timeOfJump = -1000.0f;
            rigidBody.gravityScale = maxGravityScale;
        }

        rigidBody.velocity = currentVelocity;

        groundCollider.enabled = grounded;
        airCollider.enabled = !grounded;
    }

    private void Update()
    {
        hAxis = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetButton("Jump");

        Vector2 currentVelocity = rigidBody.velocity;

        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }


        animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x));
    }
}
