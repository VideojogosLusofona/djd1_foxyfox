using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float        moveSpeed = 50.0f;
    public float        jumpVelocity = 250.0f;
    public float        jumpTime = 0.15f;
    public Collider2D   groundCollider;
    public Collider2D   airCollider;

    Rigidbody2D rigidBody;
    Animator    animator;
    float       timeOfJump;
    float       hAxis;
    bool        isJumpPressed;

    bool isInGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Ground"));

            if (collider != null) return true;

            return false;
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

        if (isJumpPressed)
        {
            if (isInGround)
            {
                rigidBody.gravityScale = 1.0f;
                currentVelocity.y = jumpVelocity;
                timeOfJump = Time.time;
            }
            else if ((Time.time - timeOfJump) < jumpTime)
            {
                rigidBody.gravityScale = 1.0f;
            }
            else
            {
                rigidBody.gravityScale = 4.0f;
            }
        }
        else
        {
            timeOfJump = -1000.0f;
            rigidBody.gravityScale = 4.0f;
        }

        rigidBody.velocity = currentVelocity;

        groundCollider.enabled = isInGround;
        airCollider.enabled = !isInGround;
    }

    private void Update()
    { 
        hAxis = Input.GetAxis("Horizontal");
        isJumpPressed = Input.GetButton("Jump");
        
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2.0f);
    }
}
