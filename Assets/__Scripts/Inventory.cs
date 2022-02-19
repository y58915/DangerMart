using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> container;           //Maybe have a dictionary?

    [HideInInspector]public UnityEvent<Item> addItemEvent;

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
        container = new List<InventorySlot>();

        ShoppingListManager.instance.ShoppingListCompleteEvent.AddListener(CompleteShoppingList);
    }


    //we maybe dont need amount. Add item only happened once a time
    public void AddItem(Item _item, int _amount) {
        bool hasItem = false;
        Debug.Log(container.ToString());
        for (int i = 0; i < container.Count; i++) {
            if (container[i].item == _item)
            {

                Score.instance.AddScore(_item.score);
                container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Score.instance.AddScore(_item.score);
            container.Add(new InventorySlot(_item, _amount));
        }

        addItemEvent.Invoke(_item);
    }


    //remove a single item (maybe remove multiple at same time?)
    public void RemoveItem(Item item)
    {

    }


    //remove item based on a shopping list
    public void CompleteShoppingList(List<Item> list)
    {

    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;
    public InventorySlot(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value) {
        amount += value;
    }
}
