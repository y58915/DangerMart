using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingListUI : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //switch to updateImage in the future
    public void UpdateText(List<Item> shoppingList)
    {
        string temp = "";

        foreach (Item item in shoppingList)
        {
            temp += item.itemName;
            temp += "\n";

        }

        text.text = temp;
    }
}
