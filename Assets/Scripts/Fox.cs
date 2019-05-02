using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float moveSpeed = 50.0f;

    Rigidbody2D rigidBody;
    Animator    animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity = new Vector2(hAxis * moveSpeed, currentVelocity.y);

        rigidBody.velocity = currentVelocity;
    }

    private void Update()
    {
        Vector2 currentVelocity = rigidBody.velocity;

        if (currentVelocity.x < 0.0f)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x));
    }
}
