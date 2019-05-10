using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    [SerializeField] float          moveSpeed = 50.0f;
    [SerializeField] float          jumpSpeed = 250.0f;
    [SerializeField] float          maxJumpTime = 0.15f;
    [SerializeField] float          maxGravityScale = 3.0f;
    [SerializeField] ParticleSystem walkDust;
    [SerializeField] int            maxHP = 3;
    [SerializeField] float          invulnerabilityDuration = 1.0f;
    [SerializeField] float          knockbackSpeed = 150.0f;
    [SerializeField] float          knockbackDuration = 0.5f;
    [SerializeField] Transform      damageSensor;

    [SerializeField] Collider2D groundCollider;
    [SerializeField] Collider2D airCollider;

    Rigidbody2D     rigidBody;
    Animator        animator;
    SpriteRenderer  sprite;
    float           timeOfJump;
    float           hAxis;
    bool            jumpPressed;
    int             currentHP;
    float           invulnerabilityTimer;
    float           knockbackTimer;

    bool isOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Ground"));

            return (collider != null);
        }
    }

    bool isInvulnerable
    {
        get
        {
            if (invulnerabilityTimer > 0.0f) return true;

            return false;
        }
        set
        {
            if (value)
            {
                invulnerabilityTimer = invulnerabilityDuration;
            }
            else
            {
                invulnerabilityTimer = 0.0f;
            }
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
        bool grounded = isOnGround;

        if (knockbackTimer <= 0.0f)
        {
            Vector2 currentVelocity = rigidBody.velocity;

            currentVelocity = new Vector2(hAxis * moveSpeed, currentVelocity.y);

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
        }

        groundCollider.enabled = grounded;
        airCollider.enabled = !grounded;

        Collider2D collider = Physics2D.OverlapCircle(damageSensor.position, 2.0f, LayerMask.GetMask("Enemy"));
        if (collider != null)
        {
            Opossum opossum = collider.GetComponent<Opossum>();
            if (opossum)
            {
                opossum.DealDamage(1, Vector3.up);

                rigidBody.velocity = Vector3.up * jumpSpeed * 0.8f;
            }
        }
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
        animator.SetFloat("VelocityY", currentVelocity.y);
        animator.SetBool("isOnGround", isOnGround);

        var emission = walkDust.emission;
        emission.enabled = isOnGround;

        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            sprite.enabled = (Mathf.FloorToInt(invulnerabilityTimer * 10.0f) % 2) == 0;

            if (invulnerabilityTimer <= 0.0f)
            {
                sprite.enabled = true;
            }
        }

        if (knockbackTimer > 0.0f)
        {
            knockbackTimer -= Time.deltaTime;
        }
    }

    public void DealDamage(int nDamage, Vector2 hitDirection)
    {
        if (isInvulnerable) return;

        currentHP = currentHP - nDamage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

        isInvulnerable = true;

        knockbackTimer = knockbackDuration;
        rigidBody.velocity = hitDirection * knockbackSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2.0f);

        if (damageSensor)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(damageSensor.position, 2.0f);
        }
    }
}
