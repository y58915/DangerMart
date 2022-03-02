using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionArea : MonoBehaviour
{
    [Header("Navigation")]
    public Transform collectionPosition;
    [HideInInspector] public SphereCollider collectionTrigger;

    [Header("Item")]
    [SerializeField]
    private Item item;

    private GameObject myItem;

    // Start is called before the first frame update
    void Start()
    {
        myItem = Instantiate(item.itemModelPrefab, this.transform.position, Quaternion.identity);

        collectionTrigger = GetComponent<SphereCollider>();
        collectionTrigger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterControl>() != null)
        {
            // Add item to the player's inventory
            Inventory.instance.addItemEvent.Invoke(item);

            // Reset the trigger 
            collectionTrigger.enabled = false;
        }
    }
}
