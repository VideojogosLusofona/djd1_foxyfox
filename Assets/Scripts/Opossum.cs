using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Character
{
    [Header("Opossum")]
    [SerializeField] Transform  groundSensor;
    [SerializeField] Transform  wallSensor;
    [SerializeField] Collider2D damageCollider;
    [SerializeField] GameObject deathPrefab;
    
    void FixedUpdate()
    {
        Vector3 currentVelocity = rigidBody.velocity;

        currentVelocity.x = -transform.right.x * moveSpeed;

        rigidBody.velocity = currentVelocity;

        if (Mathf.Abs(currentVelocity.y) < 0.1f)
        {
            Collider2D groundCollider = Physics2D.OverlapCircle(groundSensor.position, 2.0f, LayerMask.GetMask("Ground"));
            if (groundCollider == null)
            {
                transform.rotation = transform.rotation * Quaternion.AngleAxis(180.0f, Vector3.up);
            }
        }

        Collider2D wallCollider = Physics2D.OverlapCircle(wallSensor.position, 2.0f, LayerMask.GetMask("Ground"));
        if (wallCollider)
        {
            transform.rotation = transform.rotation * Quaternion.AngleAxis(180.0f, Vector3.up);
        }

        if (damageCollider)
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.ClearLayerMask();
            filter.SetLayerMask(LayerMask.GetMask("Character"));

            Collider2D[] results = new Collider2D[8];

            int nCollision = Physics2D.OverlapCollider(damageCollider, filter, results);

            if (nCollision > 0)
            {
                foreach (var collider in results)
                {
                    if (collider)
                    {
                        Character character = collider.GetComponent<Character>();
                        if ((character) && (character.faction != faction))
                        {
                            Vector3 hitDirection = (character.transform.position - transform.position).normalized;
                            hitDirection.y = 1.0f;

                            character.DealDamage(1, hitDirection);
                        }
                    }
                }
            }
        }
    }

    protected override void SetInvulnerableEffect(bool b)
    {
        if (b) sprite.color = Color.red;
        else sprite.color = Color.white;
    }

    protected override void OnDie()
    {
        Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
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
