using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum Faction { Player, Enemy };

    [Header("Character")]
                     public    Faction  faction;
    [SerializeField] protected float    moveSpeed = 50.0f;
    [SerializeField] protected int      maxHP = 3;
    [SerializeField] protected float    invulnerableDuration = 1.0f;

    protected Rigidbody2D     rigidBody;
    protected SpriteRenderer  sprite;
    protected Animator        animator;
    protected int             currentHP;
    protected float           invulnerabilityTimer;

    protected bool isInvulnerable
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

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentHP = maxHP;
    }

    protected virtual void Update()
    {
        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            if (invulnerabilityTimer > 0.0f)
            {
                SetInvulnerableEffect((Mathf.FloorToInt(invulnerabilityTimer * 10.0f) % 2) == 0);
            }
            else
            {
                SetInvulnerableEffect(false);
            }
        }
    }

    protected virtual void SetInvulnerableEffect(bool b)
    {
        sprite.enabled = !b;
    }

    public void DealDamage(int damagePoints, Vector2 hitDirection)
    {
        if (isInvulnerable) return;

        currentHP = currentHP - damagePoints;

        if (currentHP <= 0)
        {
            OnDie();
        }
        else
        {
            invulnerabilityTimer = invulnerableDuration;

            OnHit(hitDirection);

            animator.SetTrigger("Hit");
        }
    }

    protected virtual void OnHit(Vector2 hitDirection)
    {

    }

    protected virtual void OnDie()
    {
        Destroy(gameObject);
    }
}
