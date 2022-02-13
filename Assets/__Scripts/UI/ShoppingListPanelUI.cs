using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingListPanelUI : MonoBehaviour
{
    public ShoppingListUI[] shoppingListUIArray;
    // Start is called before the first frame update

    private void Awake()
    {
        ShoppingListManager.instance.UpdateListsEvent.AddListener(UpdateShoppingListPanel);
    }

    void Start()
    {
        shoppingListUIArray = gameObject.GetComponentsInChildren<ShoppingListUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateShoppingListPanel(List<List<Item>> list)
    {
        Debug.Log("update");
        for (int i = 0; i < list.Count; i++)
        {
            shoppingListUIArray[i].UpdateText(list[i]);
        }
    }
}
