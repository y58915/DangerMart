using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShoppingListManager : MonoBehaviour
{
    public int MAXSIZE = 8;
    public float newListTimer = 5f;
    public Item[] allItems;

    
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
        NewShoppingList();

        Inventory.instance.addItemEvent.AddListener(AddNewItem);

        timer = 0f;
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

    public void RemoveList()
    {
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
    private void AddNewItem(Item item)
    {
    }

    public class ShoppingList
    {
        public List<Item> itemList;
        public int score;

        public ShoppingList()
        {
            itemList = new List<Item>();
            score = 0;

        }

        public void Add(Item item)
        {
            itemList.Add(item);
            // TODO: revise the true score of an item
            score += 1;
        }
    }
}

