using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveS = 100.0f;
    public float drag = 0.8f;

    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 currentVelocity;


    /*
    public float acceleration = 10000.0f;
    public float maxSpeed = 400.0f;
    public float drag = 0.8f;
    public Vector2 limit = new Vector2(600.0f, 300.0f);

    Vector3 moveVector;
    Vector3 currentVelocity;
    */

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        float xAxis = Input.GetAxis("Horizontal");
        //Vector2 currentVelocity = rigidBody.velocity;
        
        currentVelocity = currentVelocity * drag;

        currentVelocity = new Vector2(xAxis * moveS, currentVelocity.y);

        rigidBody.velocity = currentVelocity;

        /*
         * currentVelocity = currentVelocity + moveVector * acceleration * Time.fixedDeltaTime;

        currentVelocity = currentVelocity.normalized * Mathf.Min(currentVelocity.magnitude, maxSpeed);
        
        Vector3 newPos = transform.position + currentVelocity * Time.fixedDeltaTime;
        if (newPos.x > limit.x) newPos.x = limit.x;
        else if (newPos.x < -limit.x) newPos.x = -limit.x;

        if (newPos.y > limit.y) newPos.y = limit.y;
        else if (newPos.y < -limit.y) newPos.y = -limit.y;

        transform.position = newPos;
        */
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
