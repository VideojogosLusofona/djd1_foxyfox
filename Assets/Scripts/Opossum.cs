using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Character
{
    [Header("Opossum")]
    [SerializeField] Transform  groundSensor;
    [SerializeField] Transform  wallSensor;
    [SerializeField] Collider2D damageSensor;

    float           moveDir = -1.0f;
    
    protected override void Update()
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
        contactFilter.SetLayerMask(LayerMask.GetMask("Character"));

        Collider2D[] results = new Collider2D[8];

        int nCollisions = Physics2D.OverlapCollider(damageSensor, contactFilter, results);
        if (nCollisions > 0)
        {
            for (int i = 0; i < nCollisions; i++)
            {
                collider = results[i];

                Character character = collider.GetComponent<Character>();
                if ((character) && (character.faction != faction))
                {
                    Vector2 hitDirection = (character.transform.position - transform.position).normalized;
                    hitDirection.y = 1.0f;

                    character.DealDamage(1, hitDirection);
                }
            }
        }

        base.Update();
    }

    protected override void SetInvulnerabilitFX(bool b)
    {
        if (b) sprite.color = Color.red;
        else sprite.color = Color.white;
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
