using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData) {
        Inventory.instance.DiscardItem();
    }
}
