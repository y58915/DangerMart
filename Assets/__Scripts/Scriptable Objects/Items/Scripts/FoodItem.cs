using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Food Item", menuName = "Inventory System/Items/Food")]
public class FoodItem : Item
{
    public int restoreHealthValue;
    public void Awake()
    {
        itemCategory = ItemCategory.Food;
    }
}
