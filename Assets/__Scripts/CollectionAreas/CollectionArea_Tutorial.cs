using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionArea_Tutorial : CollectionArea
{
    public bool canCollect = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterControl>() != null && canCollect)
        {
            // Add item to the player's inventory
            Inventory.instance.addItemEvent.Invoke(item);

            // Reset the trigger 
            collectionTrigger.enabled = false;
        }
    }
}
