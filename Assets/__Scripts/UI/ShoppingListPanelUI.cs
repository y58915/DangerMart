using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingListPanelUI : MonoBehaviour
{
    private ShoppingListUI[] shoppingListUIArray;
    // Start is called before the first frame update

    private void Awake()
    {
    }

    void Start()
    {
        shoppingListUIArray = gameObject.GetComponentsInChildren<ShoppingListUI>();
        ShoppingListManager.instance.UpdateListsEvent.AddListener(UpdateShoppingListPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateShoppingListPanel(List<ShoppingListManager.ShoppingList> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            shoppingListUIArray[i].UpdateText(list[i]);
        }
    }
}
