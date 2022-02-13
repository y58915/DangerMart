using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShoppingListManager : MonoBehaviour
{
    public int MAXSIZE = 8;
    public List<Item> allItems;

    bool once = false;

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
        AddList(NewShoppingList());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddList(List<Item> newList)
    {
        shoppingLists.Add(newList);

        Debug.Log(newList[0]);
        UpdateListsEvent.Invoke(shoppingLists);
    }

    public void RemoveList()
    {
        UpdateListsEvent.Invoke(shoppingLists);
    }
    private List<Item> NewShoppingList()
    {
        List<Item> list = new List<Item>();

        int count = allItems.Count;
        int repeatTime = Random.Range(2, 4);

        for (int i = 0; i < repeatTime; i++)
        {
            int rand = Random.Range(0, count);

            list.Add(allItems[rand]);
        }

        return list;
    }
}

