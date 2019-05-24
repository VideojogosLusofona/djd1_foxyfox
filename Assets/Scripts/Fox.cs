using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Character
{
    [Header("Fox")]
    [SerializeField] float          jumpSpeed = 250.0f;
    [SerializeField] float          maxJumpTime = 0.15f;
    [SerializeField] float          maxGravityScale = 3.0f;
    [SerializeField] ParticleSystem walkDust;
    [SerializeField] float          knockbackSpeed = 150.0f;
    [SerializeField] float          knockbackDuration = 0.5f;
    [SerializeField] Transform      damageSensor;

    [SerializeField] Collider2D groundCollider;
    [SerializeField] Collider2D airCollider;

    Animator        animator;
    float           timeOfJump;
    float           hAxis;
    bool            jumpPressed;
    float           knockbackTimer;

    bool isOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Ground"));

            return (collider != null);
        }
    }

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();

        timeOfJump = -1000.0f;
    }

    void FixedUpdate()
    {
        if (currentHP <= 0) return;

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

        Collider2D collider = Physics2D.OverlapCircle(damageSensor.position, 2.0f, LayerMask.GetMask("Character"));
        if (collider != null)
        {
            Character character = collider.GetComponent<Character>();
            if ((character) && (character.faction != faction))
            {
                character.DealDamage(1, Vector3.up);

                rigidBody.velocity = Vector3.up * jumpSpeed * 0.8f;
            }
        }
    }

    protected override void Update()
    {
        if (currentHP <= 0) return;

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

        base.Update();

        if (knockbackTimer > 0.0f)
        {
            knockbackTimer -= Time.deltaTime;
        }
    }

    protected override void OnHit(Vector2 hitDirection)
    {
        knockbackTimer = knockbackDuration;
        rigidBody.velocity = hitDirection * knockbackSpeed;

        animator.SetTrigger("Hit");
    }

    protected override void OnDie()
    {
        StartCoroutine(OnDieCR());
    }

    IEnumerator OnDieCR()
    {
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector2.zero;

        sprite.color = new Color(0.5f, 1.0f, 1.0f, 0.5f);
        animator.SetTrigger("Dead");

        float elapsedTime = 0.0f;
        while (elapsedTime < 2.0f)
        {
            Vector3 currentPos = transform.position;
            currentPos.y = currentPos.y + 25 * Time.fixedDeltaTime;
            transform.position = currentPos;

            yield return new WaitForFixedUpdate();

            elapsedTime += Time.fixedDeltaTime;
        }

        Destroy(gameObject);

        LevelManager.instance.LoseLife();
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
