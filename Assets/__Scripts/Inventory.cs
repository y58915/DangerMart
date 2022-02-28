using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Analytics;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<Item> container;             //value: the number of the item in the inventory

    [HideInInspector] public UnityEvent<Item> addItemEvent;
    [HideInInspector] public UnityEvent itemUpdatedEvent;
    [HideInInspector] public UnityEvent updateInventoryEvent;

    #region Singleton
    public static Inventory instance;

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

    private void Start()
    {
        container = new List<Item>();

        addItemEvent.AddListener(AddItem);
        ShoppingListManager.instance.ShoppingListCompleteEvent.AddListener(CompleteShoppingList);
    }


    //we maybe dont need amount. Add item only happened once a time
    public void AddItem(Item _item)
    {
        int capacity = 6;
        if (container.Count >= capacity) {
            return;
        }
        else {
            container.Add(_item);
        }

        AnalyticsResult analyticsResult_Item = Analytics.CustomEvent("Item Picked Up", new Dictionary<string, object> { { _item.itemName, 1 } }); // TODO: remove?
        Debug.Log("New Items " + analyticsResult_Item);


        PrintInventory();

        itemUpdatedEvent.Invoke();
        updateInventoryEvent.Invoke();
    }


    //remove a single item (maybe remove multiple at same time?)
    public void RemoveItem(Item _item)
    {
        if (!container.Contains(_item))
        {
            return;
        }

        container.Remove(_item);
        AnalyticsResult analyticsResult_Item = Analytics.CustomEvent("Item Discarded", new Dictionary<string, object> { { _item.itemName, 1 } }); // TODO: remove?
        Debug.Log("Discarded Items " + analyticsResult_Item);
        updateInventoryEvent.Invoke();
    }


    //remove item based on a shopping list
    public void CompleteShoppingList(ShoppingList list)
    {
        foreach (Item item in list.itemList)
        {
            RemoveItem(item);
        }

        PrintInventory();
        Score.instance.AddScore(list.rating);
        itemUpdatedEvent.Invoke();
    }

    public List<Item> GetInventory()
    {
        
        return container;
    }

    /*public Item GetRandomItem()
    {
        container.Keys.ToString();

        return container.
    }*/

    void PrintInventory()
    {
        string temp = "";
        foreach (var entry in container)
        {
            temp += entry.itemName;
            temp += ", ";
        }

        Debug.Log(temp);
    }
}

//[System.Serializable]
//public class InventorySlot
//{
//    public Item item;
//    public int amount;
//    public InventorySlot(Item _item, int _amount)
//    {
//        item = _item;
//        amount = _amount;
//    }
//    public void AddAmount(int value)
//    {
//        amount += value;
//    }
//    public void RemoveAmount(int value)
//    {
//        amount -= value;
//    }
//}
