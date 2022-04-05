using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan_Tutorial : TrashCan
{
    [SerializeField]
    private Level1Tutorial level1Tutorial;

    private bool completed = false;

    private int itemsTossed = 0;

    public override void OnDrop(PointerEventData eventData)
    {
        Inventory.instance.DiscardItem();

        itemsTossed++;
        if (itemsTossed > 0 && !completed)
        {
            completed = true;
            level1Tutorial.TrashToScore();
        }
    }
}
