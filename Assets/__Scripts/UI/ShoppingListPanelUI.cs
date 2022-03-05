using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingListPanelUI : MonoBehaviour
{
    private ShoppingListUI[] shoppingListUIArray;
    private StarRatingUI[] ratingUIs;
    // Start is called before the first frame update

    private void Awake()
    {
    }

    void Start()
    {
        shoppingListUIArray = gameObject.GetComponentsInChildren<ShoppingListUI>();
        ratingUIs = gameObject.GetComponentsInChildren<StarRatingUI>();
        ShoppingListManager.instance.UpdateListsEvent.AddListener(UpdateShoppingListPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePanelAfterCompletion()
    {
        List<ShoppingList> list = ShoppingListManager.instance.GetAllShoppingLists();
        foreach (ShoppingList shoppingList in list)
        {
            
        }
    }

    void UpdateShoppingListPanel(List<ShoppingList> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            shoppingListUIArray[i].enabled = true;
            shoppingListUIArray[i].UpdateText(list[i]);
            ratingUIs[i].SetStar(list[i].rating);
        }

        for (int i = list.Count-1; i < ShoppingListManager.instance.MAXSIZE-list.Count; i++)
        {
            shoppingListUIArray[i].enabled = false;
        }
    }
}
