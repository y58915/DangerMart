using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingListPanelUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShoppingListManager.instance.UpdateListsEvent.AddListener(UpdateShoppingListPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateShoppingListPanel()
    {

    }
}
