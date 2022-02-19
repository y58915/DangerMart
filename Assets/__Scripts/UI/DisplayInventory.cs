using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    List<InventorySlot> inventory;

    [SerializeField] GameObject inventoryImageParent;
    Image[] inventoryImageList;

    bool skipfirst = true;

    // Start is called before the first frame update
    void Start()
    {
        inventoryImageList = inventoryImageParent.GetComponentsInChildren<Image>();
    }

    private void OnEnable()
    {
        if (skipfirst)
        {
            skipfirst = false;
            return;
        }
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
    }
 
    public void UpdateDisplay()
    {
        inventory = Inventory.instance.GetInventory();

        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i] == null)
            {
                inventoryImageList[i].sprite = null;
                inventoryImageList[i].color = Color.clear;
            }
            else
            {
                inventoryImageList[i].sprite = inventory[i].item.itemImage;
                inventoryImageList[i].color = Color.white;
            }
        }
    }
}
