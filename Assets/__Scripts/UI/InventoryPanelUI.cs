using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelUI : MonoBehaviour
{
    Dictionary<Item, int> inventory;

    [SerializeField] GameObject inventoryImageParent;
    [SerializeField] GameObject inventoryCountParent;

    Image[] inventoryImageList;
    Text[] inventoryCountList;

    bool skipfirst = true;

    // Start is called before the first frame update
    void Start()
    {
        inventoryImageList = inventoryImageParent.GetComponentsInChildren<Image>();
        inventoryCountList = inventoryCountParent.GetComponentsInChildren<Text>();

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
            // inventoryCountList[i].text = entry.Value.ToString();
            // inventoryCountList[i].color = Color.black;
            i++;
        }

        for (int j = i; j < inventoryImageList.Length; j++)
        {
            inventoryImageList[j].sprite = null;
            // inventoryImageList[j].color = Color.clear;
            // inventoryCountList[j].text = "";
            // inventoryCountList[j].color = Color.clear;
        }
    }
}
