using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelUI : MonoBehaviour
{
    List<Item> inventory;

    [SerializeField] GameObject inventorySlotParent;
    [SerializeField] GameObject inventoryCountParent;

    public InventorySlotUI[] inventorySlotList;
    Text[] inventoryCountList;

    bool skipfirst = true;

    #region Singleton
    public static InventoryPanelUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        inventorySlotList = inventorySlotParent.GetComponentsInChildren<InventorySlotUI>();
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
        foreach (var item in inventory)
        {
            inventorySlotList[i].SetItem(item);
            // inventoryCountList[i].text = entry.Value.ToString();
            // inventoryCountList[i].color = Color.black;
            i++;
        }

        for (int j = i; j < inventorySlotList.Length; j++)
        {
            inventorySlotList[i].ClearItem();
        }
    }
}
