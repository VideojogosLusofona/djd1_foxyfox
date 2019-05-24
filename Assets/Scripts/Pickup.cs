using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int        scoreToAdd = 0;
    [SerializeField] GameObject pickupFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();
        if (character != null)
        {
            if (character.faction == Character.Faction.Player)
            {
                GameMng.instance.AddScore(scoreToAdd);
                NotifyPickup(character);

                if (pickupFX != null)
                {
                    Instantiate(pickupFX, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else
                {
                    Animator animator = GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.SetTrigger("Pickup");
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    protected virtual void NotifyPickup(Character character)
    {

    }
}
