using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfItem : MonoBehaviour
{
    [SerializeField] string itemName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.collider.GetComponent<Character>();
        if (character != null)
        {
            if (character.faction == Character.Faction.Player)
            {
                if (character.HasItem(itemName))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
