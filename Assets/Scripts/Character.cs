using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum Faction { Player, Enemy };

    [Header("Character")]
    public Faction  faction;

    [SerializeField] protected float        moveSpeed = 50.0f;
    [SerializeField] protected int          maxHP = 3;
    [SerializeField] protected float        invulnerabilityDuration = 1.0f;
    [SerializeField] protected GameObject   deathPrefab;

    protected Rigidbody2D     rigidBody;
    protected SpriteRenderer  sprite;
    protected int             currentHP;
    protected float           invulnerabilityTimer;
    protected List<string>    inventory;

    protected bool isInvulnerable
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

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        inventory = new List<string>();

        currentHP = maxHP;
    }

    protected virtual void Update()
    {
        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            SetInvulnerabilitFX((Mathf.FloorToInt(invulnerabilityTimer * 10.0f) % 2) == 0);

            if (invulnerabilityTimer <= 0.0f)
            {
                SetInvulnerabilitFX(false);
            }
        }
    }

    protected virtual void SetInvulnerabilitFX(bool b)
    {
        sprite.enabled = !b;
    }

    public void DealDamage(int nDamage, Vector2 hitDirection)
    {
        if (isInvulnerable) return;

        currentHP = currentHP - nDamage;

        if (currentHP <= 0)
        {
            OnDie();
        }
        else
        {
            isInvulnerable = true;

            OnHit(hitDirection);
        }
    }

    protected virtual void OnHit(Vector2 hitDirection)
    { 
    }

    protected virtual void OnDie()
    {
        Destroy(gameObject);

        if (deathPrefab)
        {
            Instantiate(deathPrefab, transform.position, transform.rotation);
        }
    }

    public void AddToInventory(string itemName)
    {
        if (inventory.IndexOf(itemName) == -1)
        {
            inventory.Add(itemName);
        }
    }

    public void RemoveFromInventory(string itemName)
    {
        int index = inventory.IndexOf(itemName);
        if (index != -1)
        {
            inventory.RemoveAt(index);
        }
    }

    public bool HasItem(string itemName)
    {
        int index = inventory.IndexOf(itemName);

        return (index != -1);
    }
}
