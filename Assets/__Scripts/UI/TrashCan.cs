using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        List<Item> items = new List<Item>(Inventory.instance.container.Where(x => x.Value != 0).Select(item=>item.Key).ToList());
        Inventory.instance.RemoveItem(items[0]);
    }
}
