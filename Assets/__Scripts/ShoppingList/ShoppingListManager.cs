using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShoppingListManager : MonoBehaviour
{
    public int MAXSIZE = 8;
    public float newListTimer = 5f;
    public Item[] allItems;
    public int listsCompleted = 0;
    private float timer;

    List<ShoppingList> shoppingLists;

    [HideInInspector] public UnityEvent<List<ShoppingList>> UpdateListsEvent;
    [HideInInspector] public UnityEvent<ShoppingList> ShoppingListCompleteEvent;          //Link: inventory remove item, score add score

    #region Singleton
    public static ShoppingListManager instance;

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
        shoppingLists = new List<ShoppingList>();

        Inventory.instance.itemUpdatedEvent.AddListener(NewItemAdded);

        for (int i = 0; i < MAXSIZE-1; i++)
        {
            NewShoppingList();
        }

        timer = newListTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= newListTimer)
        {
            timer = 0;
            if (shoppingLists.Count < MAXSIZE)
                NewShoppingList();
        }
    }

    public void AddList(ShoppingList newList)
    {
        shoppingLists.Add(newList);

        UpdateListsEvent.Invoke(shoppingLists);
    }

    public void RemoveList(int i)
    {
        shoppingLists.RemoveAt(i);
        UpdateListsEvent.Invoke(shoppingLists);
    }



    private void NewShoppingList()
    {
        ShoppingList list = new ShoppingList();

        int count = allItems.Length;
        int repeatTime = Random.Range(2, 4);

        for (int i = 0; i < repeatTime; i++)
        {
            int rand = Random.Range(0, count);

            list.Add(allItems[rand]);
        }

        AddList(list);
    }

    
    //update shoppinglist based on the inventory
    private void NewItemAdded()
    {
        CompareInventoryShoppingLists();
    }


    //temp, we may need to optimize the algorithm
    public void CompareInventoryShoppingLists()
    {
        for (int i = 0; i < shoppingLists.Count; i++)
        {
            Dictionary<Item, int> curr = new Dictionary<Item, int>();
            foreach (Item item in shoppingLists[i].itemList)
            {
                if (!curr.ContainsKey(item))
                {
                    curr[item] = 0;
                }
                curr[item] += 1;
            }
            bool completed = true;

            var inventory = Inventory.instance.GetInventory();

            foreach (KeyValuePair<Item, int> entry in curr)
            {
                if (!(inventory.ContainsKey(entry.Key) && inventory[entry.Key] >= entry.Value))
                {
                    completed = false;
                    break;
                }
            }

            if (completed)
            {
                // Increase the amount of lists completed
                listsCompleted++;

                // Remove a shopping list
                ShoppingListCompleteEvent.Invoke(shoppingLists[i]);
                RemoveList(i);
            }
        }
    }

    public void AddSpecificList(List<Item> list)
    {
        ShoppingList temp = new ShoppingList();
        foreach (Item item in list)
        {
            temp.Add(item);
        }
        shoppingLists.Add(temp);
    }
}


public class ShoppingList
{
    public List<Item> itemList;
    public float score;

    public ShoppingList()
    {
        itemList = new List<Item>();
        score = 0;
    }

    public void Add(Item item)
    {
        itemList.Add(item);
        // TODO: revise the true score of an item
        score += item.score;
    }

    public override string ToString()
    {
        return score.ToString() + " " + itemList.ToString();
    }
}