using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class ShoppingListManager : MonoBehaviour
{
    public int MAXSIZE = 5;
    public int MAXRATING = 4;
    public float newListTimer = 5f;
    public Item[] allItems;
    public int listsCompleted = 0;
    private float timer;

    public List<ShoppingList> PRESET;

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

        // Inventory.instance.itemUpdatedEvent.AddListener(NewItemAdded);

        // prepopulate the list to maxsize-1 and let update() add the remaining list so that the UI can be automatically updated
        for (int i = 0; i < MAXSIZE-1; i++)
        {
            NewShoppingList();
        }
        timer = newListTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelController.instance.gamePaused && shoppingLists.Count < MAXSIZE)
        {
            Debug.Log("New shopping list");
            NewShoppingList();
        }
            
    }

    public void AddList(ShoppingList newList)
    {
        shoppingLists.Add(newList);

        UpdateListsEvent.Invoke(shoppingLists);
    }

    public void RemoveList(ShoppingList shoppingList)
    {
        shoppingLists.Remove(shoppingList);
        UpdateListsEvent.Invoke(shoppingLists);
    }
    public ShoppingList GetShoppingListByIndex(int index)
    {
        return shoppingLists[index];
    }
    public List<ShoppingList> GetAllShoppingLists()
    {
        return shoppingLists;
    }


    private void NewShoppingList()
    {
        AddList(PRESET[Random.Range(0, PRESET.Count)]);
    }

    private void NewShoppingList(int i){
        AddList(PRESET[i]);
    }

    
    //update shoppinglist based on the inventory
    private void NewItemAdded()
    {
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

    public override string ToString()
    {
        string final = "";
        foreach (ShoppingList shoppingList in shoppingLists)
        {
            List<string> tmp = new List<string>();
            foreach (Item item in shoppingList.itemList)
            {
                tmp.Add(item.itemName);
            }

            tmp[tmp.Count - 1] += "\n";
            final += string.Join(",", tmp);
        }

        return final;
    }
}
