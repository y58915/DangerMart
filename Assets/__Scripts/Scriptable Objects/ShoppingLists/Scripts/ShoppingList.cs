using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Shopping List", menuName = "ShoppingList")]
public class ShoppingList : ScriptableObject
{
    public List<Item> itemList;
    [HideInInspector] public float score;
    public int rating = 2;

    public ShoppingList()
    {
        itemList = new List<Item>();
        score = 0;
    }

    public void Add(Item item)
    {
        itemList.Add(item);
        // TODO: revise the true score of an item
        score += item.score;
    }

    public override string ToString()
    {
        string temp = "";


        foreach (Item item in itemList)
        {
            temp += item.itemName;
            temp += "\n";
        }
        return temp;
    }
}