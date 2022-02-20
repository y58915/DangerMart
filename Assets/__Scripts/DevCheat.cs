using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevCheat : MonoBehaviour
{
    [SerializeField] bool startWithInventory;
    [SerializeField] List<Item> inventory;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetCheat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SetCheat()
    {
        yield return new WaitForEndOfFrame();

        if (startWithInventory)
        {
            foreach (Item item in inventory)
            {
                Inventory.instance.addItemEvent.Invoke(item);
            }
        }
    }
}
