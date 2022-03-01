using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        // List<Item> items = new List<Item>(Inventory.instance.container);
        // detect which index of inventory slots is dropped, and then remove it

        Inventory.instance.RemoveMovingItem();
        // if (InventorySlotUI.draggedIndex < items.Count) {
        //     Inventory.instance.RemoveItem(items[InventorySlotUI.draggedIndex]);
        // }
    }
}
