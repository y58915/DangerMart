using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Tool")]
public class ToolItem : Item
{
    [TextArea(10, 20)]
    public string usage;
    public void Awake()
    {
        itemCategory = ItemCategory.Tool;
    }
}
