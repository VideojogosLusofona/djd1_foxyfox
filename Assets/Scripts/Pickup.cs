using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int          scoreToAdd;
    public GameObject   pickupFX;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if (character != null)
        {
            if (character.faction == Character.Faction.Player)
            {
                GameMng.instance.AddScore(scoreToAdd);
                Destroy(gameObject);

                if (pickupFX != null)
                {
                    Instantiate(pickupFX, transform.position, transform.rotation);
                }
            }
        }
    }
}
