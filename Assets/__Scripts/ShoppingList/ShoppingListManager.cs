using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShoppingListManager : MonoBehaviour
{
    List<ShoppingList> shoppingLists;

    public UnityEvent UpdateListsEvent;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddList()
    {
        UpdateListsEvent.Invoke();
    }

    public void RemoveList()
    {
        UpdateListsEvent.Invoke();
    }
}
