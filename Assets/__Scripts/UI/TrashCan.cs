using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        List<Item> items = new List<Item>(Inventory.instance.container);
        Inventory.instance.RemoveItem(items[0]);
    }
}
