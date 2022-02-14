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

    List<List<Item>> shoppingLists;

    [HideInInspector] public UnityEvent<List<List<Item>>> UpdateListsEvent;

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
        shoppingLists = new List<List<Item>>();
        NewShoppingList();

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

    public void AddList(List<Item> newList)
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
        List<Item> list = new List<Item>();

        int count = allItems.Length;
        int repeatTime = Random.Range(2, 4);

        for (int i = 0; i < repeatTime; i++)
        {
            int rand = Random.Range(0, count);

            list.Add(allItems[rand]);
        }

        AddList(list);
    }
}

