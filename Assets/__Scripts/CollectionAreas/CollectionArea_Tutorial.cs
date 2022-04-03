using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionArea_Tutorial : CollectionArea
{
    public bool canCollect = false;

    [SerializeField]
    private bool completed = false;

    [SerializeField]
    private Level1Tutorial level1Tutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterControl>() != null && canCollect)
        {
            // Add item to the player's inventory
            Inventory.instance.addItemEvent.Invoke(item);

            if (Inventory.instance.container.Contains(item) && !completed)
            {
                completed = true;
                level1Tutorial.CollectionToRegister();
            }

            // Reset the trigger 
            collectionTrigger.enabled = false;
        }
    }
}
