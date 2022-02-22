using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] public Dictionary<Item, int> container;             //value: the number of the item in the inventory

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
        container = new Dictionary<Item, int>();

        addItemEvent.AddListener(AddItem);
        ShoppingListManager.instance.ShoppingListCompleteEvent.AddListener(CompleteShoppingList);
    }


    //we maybe dont need amount. Add item only happened once a time
    public void AddItem(Item _item)
    {
        if (container.Count == 0)
        {
            container.Add(_item, 1);
        }
        else if (container.ContainsKey(_item))
        {
            container[_item]++;
        }
        else
        {
            container.Add(_item, 1);
        }


        PrintInventory();

        itemUpdatedEvent.Invoke();
        updateInventoryEvent.Invoke();
    }


    //remove a single item (maybe remove multiple at same time?)
    public void RemoveItem(Item _item)
    {
        if (!container.ContainsKey(_item))
        {
            return;
        }

        int value = container[_item];

        if (value == 0)
        {
            return;
        }

        container[_item]--;

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
        Score.instance.AddScore(list.score);
        itemUpdatedEvent.Invoke();
    }

    public Dictionary<Item, int> GetInventory()
    {
        return container;
    }

    void PrintInventory()
    {
        string temp = "";
        foreach (var entry in container)
        {
            temp += entry.Key.itemName;
            temp += ": ";
            temp += entry.Value;
            temp += "; ";
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
