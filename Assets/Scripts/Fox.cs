using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Character
{
    [Header("Fox")]
    public float        jumpVelocity = 250.0f;
    public float        jumpTime = 0.15f;
    public Collider2D   groundCollider;
    public Collider2D   airCollider;
    public float        knockbackSpeed = 250.0f;
    public Transform    damageSensor;
    public AudioClip    jumpSound;

    float           timeOfJump;
    float           hAxis;
    bool            isJumpPressed;
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

    protected override void Start()
    {
        base.Start();

        timeOfJump = -1000.0f;
    }

    void FixedUpdate()
    {
        if (currentHP <= 0) return;

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

                    SoundManager.PlaySound(jumpSound, 1.0f, Random.Range(0.75f, 1.25f));
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

        Collider2D collider = Physics2D.OverlapCircle(damageSensor.position, 2.0f, LayerMask.GetMask("Character"));
        if (collider != null)
        {
            Character otherCharacter = collider.GetComponent<Character>();
            if ((otherCharacter) && (otherCharacter.faction != faction))
            {
                otherCharacter.DealDamage(1, Vector3.up);

                rigidBody.velocity = Vector3.up * jumpVelocity * 0.5f;
            }
        }

    }

    protected override void Update()
    {
        if (currentHP <= 0) return;

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

        base.Update();

        if (knockbackTimer > 0.0f)
        {
            knockbackTimer -= Time.deltaTime;
        }
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

    protected override void OnHit(Vector2 hitDirection)
    {
        knockbackTimer = 0.5f;
        rigidBody.velocity = knockbackSpeed * hitDirection;
    }

    protected override void OnDie()
    {
        sprite.color = new Color(0.5f, 1.0f, 1.0f, 0.5f);
        rigidBody.velocity = Vector2.zero;
        rigidBody.isKinematic = true;
        animator.SetTrigger("Dead");

        StartCoroutine(OnDieCR());
    }

    IEnumerator OnDieCR()
    {
        float deadTimer = 2.0f;

        while (deadTimer > 0.0f)
        {
            Vector3 currentPos = transform.position;
            currentPos.y = currentPos.y + 25.0f * Time.deltaTime;
            transform.position = currentPos;

            yield return null;

            deadTimer -= Time.deltaTime;
        }

        Destroy(gameObject);

        LevelManager.instance.LoseLife();
    }
}
