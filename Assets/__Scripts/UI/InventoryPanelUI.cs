using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelUI : MonoBehaviour
{
    Dictionary<Item, int> inventory;

    [SerializeField] GameObject inventoryImageParent;
    Image[] inventoryImageList;

    bool skipfirst = true;

    // Start is called before the first frame update
    void Start()
    {
        inventoryImageList = inventoryImageParent.GetComponentsInChildren<Image>();

        Inventory.instance.updateInventoryEvent.AddListener(UpdateDisplay);
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
        if (!gameObject.activeInHierarchy) return;

        inventory = Inventory.instance.GetInventory();

        int i = 0;
        foreach (KeyValuePair<Item, int> entry in inventory)
        {
            if (entry.Value == 0)
                continue;
            inventoryImageList[i].sprite = entry.Key.itemImage;
            inventoryImageList[i].color = Color.white;
            i++;
        }

        for (int j = i; j < inventoryImageList.Length; j++)
        {
            inventoryImageList[j].sprite = null;
            inventoryImageList[j].color = Color.clear;
        }
    }
}
