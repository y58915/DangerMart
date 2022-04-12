using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingListUI : MonoBehaviour
{
    public Text text;

    void Awake()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //switch to updateImage in the future
    public void UpdateText(ShoppingList shoppingList)
    {
        string temp = "";


        foreach (Item item in shoppingList.itemList)
        {
            temp += item.itemName;
            temp += "\n";
        }

        text.text = temp;
    }
    public void UpdateText()
    {
        text.text = "";
    }
}
