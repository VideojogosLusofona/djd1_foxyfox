using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    [SerializeField] float      moveSpeed = 75.0f;
    [SerializeField] Transform  groundSensor;
    [SerializeField] Transform  wallSensor;
    [SerializeField] Collider2D damageSensor;
    [SerializeField] int        maxHP = 3;
    [SerializeField] float      invulnerabilityDuration = 1.0f;
    [SerializeField] GameObject deathPrefab;

    Rigidbody2D     rigidBody;
    SpriteRenderer  sprite;
    float           moveDir = -1.0f;
    float           invulnerabilityTimer;
    int             currentHP;

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
        sprite = GetComponent<SpriteRenderer>();

        currentHP = maxHP;
    }
        
    void Update()
    {
        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity.x = moveDir * moveSpeed;

        rigidBody.velocity = currentVelocity;

        if (moveDir < 0.0f) transform.rotation = Quaternion.identity;
        else transform.rotation = Quaternion.Euler(0, 180, 0);

        Collider2D collider = Physics2D.OverlapCircle(groundSensor.position, 2.0f, LayerMask.GetMask("Ground"));
        if (collider == null)
        {
            moveDir = -moveDir;
        }
        else
        {
            collider = Physics2D.OverlapCircle(wallSensor.position, 2.0f, LayerMask.GetMask("Ground"));
            if (collider != null)
            {
                moveDir = -moveDir;
            }
        }

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.ClearLayerMask();
        contactFilter.SetLayerMask(LayerMask.GetMask("Player"));

        Collider2D[] results = new Collider2D[8];

        int nCollisions = Physics2D.OverlapCollider(damageSensor, contactFilter, results);
        if (nCollisions > 0)
        {
            for (int i = 0; i < nCollisions; i++)
            {
                collider = results[i];

                Fox fox = collider.GetComponent<Fox>();
                if (fox)
                {
                    Vector2 hitDirection = (fox.transform.position - transform.position).normalized;
                    hitDirection.y = 1.0f;

                    fox.DealDamage(1, hitDirection);
                }
            }
        }

        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            if ((Mathf.FloorToInt(invulnerabilityTimer * 10.0f) % 2) == 0)
            {
                sprite.color = new Color(1.0f, 0.25f, 0.25f, 1.0f);
            }
            else
            {
                sprite.color = Color.white;
            }

            if (invulnerabilityTimer <= 0.0f)
            {
                sprite.color = Color.white;
            }
        }
    }

    public void DealDamage(int nDamage, Vector2 hitDirection)
    {
        if (isInvulnerable) return;

        currentHP = currentHP - nDamage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);

            Instantiate(deathPrefab, transform.position, transform.rotation);
        }

        isInvulnerable = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundSensor)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundSensor.position, 2.0f);
        }

        if (wallSensor)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(wallSensor.position, 2.0f);
        }
    }
}
