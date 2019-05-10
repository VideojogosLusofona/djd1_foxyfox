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
    public int          maxHP = 3;
    public float        invulnerableDuration = 1.0f;
    public float        knockbackSpeed = 250.0f;
    public Transform    damageSensor;

    Rigidbody2D     rigidBody;
    Animator        animator;
    SpriteRenderer  sprite;
    float           timeOfJump;
    float           hAxis;
    bool            isJumpPressed;
    int             currentHP;
    float           invulnerabilityTimer;
    float           knockbackTimer;

    bool isOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Ground"));

            if (collider != null) return true;

            return false;
        }
    }

    bool isInvulnerable
    {
        get
        {
            if (invulnerabilityTimer > 0.0f)
            {
                return true;
            }

            return false;
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        timeOfJump = -1000.0f;
        currentHP = maxHP;
    }

    void FixedUpdate()
    {
        if (knockbackTimer <= 0.0f)
        {
            Vector2 currentVelocity = rigidBody.velocity;

            currentVelocity = new Vector2(hAxis * moveSpeed, currentVelocity.y);

            if (isJumpPressed)
            {
                if (isOnGround)
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
        }

        groundCollider.enabled = isOnGround;
        airCollider.enabled = !isOnGround;

        Collider2D collider = Physics2D.OverlapCircle(damageSensor.position, 2.0f, LayerMask.GetMask("Enemy"));
        if (collider != null)
        {
            Opossum opossum = collider.GetComponent<Opossum>();
            if (opossum)
            {
                opossum.DealDamage(1, Vector3.up);

                rigidBody.velocity = Vector3.up * jumpVelocity * 0.5f;
            }
        }

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
        animator.SetFloat("VelocityY", currentVelocity.y);
        animator.SetBool("isOnGround", isOnGround);

        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            if (invulnerabilityTimer > 0.0f)
            {
                sprite.enabled = (Mathf.FloorToInt(invulnerabilityTimer * 10.0f) % 2) == 0;
            }
            else
            {
                sprite.enabled = true;
            }
        }

        if (knockbackTimer > 0.0f)
        {
            knockbackTimer -= Time.deltaTime;
        }
    }

    public void DealDamage(int damagePoints, Vector2 hitDirection)
    {
        if (isInvulnerable) return;

        currentHP = currentHP - damagePoints;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

        invulnerabilityTimer = invulnerableDuration;
        knockbackTimer = 0.5f;
        rigidBody.velocity = knockbackSpeed * hitDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2.0f);

        if (damageSensor)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(damageSensor.position, 2.0f);
        }
    }    
}
