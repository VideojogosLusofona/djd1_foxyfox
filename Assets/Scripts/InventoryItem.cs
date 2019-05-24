using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : Pickup
{
    [SerializeField] string itemName;

    protected override void NotifyPickup(Character character)
    {
        character.AddToInventory(itemName);
    }
}
