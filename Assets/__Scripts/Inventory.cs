using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Analytics;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<Item> container;
    [HideInInspector] public UnityEvent<Item> addItemEvent;
    [HideInInspector] public UnityEvent itemUpdatedEvent;
    [HideInInspector] public UnityEvent updateInventoryEvent;
    SoundManager soundManager;

    public int movingIndex;

    public int MAXSIZE = 6;

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
        soundManager = GameObject.Find("SoundManager/SFX").GetComponent<SoundManager>();
    }


    //we maybe dont need amount. Add item only happened once a time
    public void AddItem(Item _item)
    {
        if (container.Count >= MAXSIZE) {
            return;
        }
        else {
            container.Add(_item);
        }
        AnalyticsResult analyticsResult_Item = Analytics.CustomEvent("Item Picked Up", new Dictionary<string, object> 
        {
            { "Level", LevelController.instance.GetLevel() },
            { "Item Picked Up", _item.itemName } 
        });
        // Debug.Log("New Item " + _item.itemName + analyticsResult_Item);
        PrintInventory();
        itemUpdatedEvent.Invoke();
        updateInventoryEvent.Invoke();

        if (soundManager != null)
            soundManager.Play("Pickup");
    }


    //remove a single item (called when the shopping list is completed)
    public void RemoveItem(Item _item)
    {
        if (!container.Contains(_item))
        {
            return;
        }
        container.Remove(_item);
        updateInventoryEvent.Invoke();
    }

    public void StealItem(Item _item)
	{
        RemoveItem(_item);
        AnalyticsResult analyticsResult_Item = Analytics.CustomEvent("Item Stolen", new Dictionary<string, object>
        {
            { "Level", LevelController.instance.GetLevel() },
            { "Item Stolen", _item.itemName }
        });
    }

    public void RemoveWildCard()
    {
        foreach (Item _item in container)
        {
            // Debug.Log("item_name: "+ _item.itemName);
            if(_item.itemName == "Wildcard"){
                container.Remove(_item);
                break;
            }
        }
        updateInventoryEvent.Invoke();
    }

    public void DiscardItem()
    {
        // Debug.Log(movingIndex);
        if (movingIndex == -1){
            return;
        }
        if (movingIndex >= container.Count){
            return;
        }
        AnalyticsResult analyticsResult_Item = Analytics.CustomEvent("Item Discarded", new Dictionary<string, object> 
        {
            { "Level", LevelController.instance.GetLevel() },
            { "Item Discarded", container[movingIndex].itemName } 
        });
        // Debug.Log("Discarded Item "+ container[movingIndex].itemName + analyticsResult_Item);
        container.RemoveAt(movingIndex);
        movingIndex = -1;
        updateInventoryEvent.Invoke();
        soundManager.Play("Trash");
    }


    //remove item based on a shopping list
    public void CompleteShoppingList(ShoppingList list)
    {
        foreach (Item item in list.itemList)
        {
            // Debug.Log(item.itemName);
            if (container.Contains(item)){
                RemoveItem(item);
            } else{
                RemoveWildCard();
            }
            AnalyticsResult analyticsResult_Item = Analytics.CustomEvent("Item Completed", new Dictionary<string, object> 
            {
                { "Level", LevelController.instance.GetLevel() },
                { "Item Completed", item.itemName } 
            });
            // Debug.Log("Completed Item " + item.itemName + analyticsResult_Item);
        }
        PrintInventory();
        Score.instance.AddScore(list.rating * 100);
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
        // Debug.Log(temp);
    }

    public override string ToString()
    {
        List<string> itemNames = new List<string>();


        foreach (Item item in container)
        {
            itemNames.Add(item.itemName);
        }
        return String.Join(",", itemNames);
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
