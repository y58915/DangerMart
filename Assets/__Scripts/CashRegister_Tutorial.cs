using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using __Scripts;

public class CashRegister_Tutorial : CashRegister
{
    [SerializeField]
    private Level1Tutorial level1Tutorial;

    private int listCompleted = 0;

    private bool completed = false;

    public override void CompleteAction(int index)
    {
        UIUpdate(Enumerable.Range(0, shoppingList.MAXSIZE).ToList(), false);
        // shoppingList.RemoveList(index);
        ShoppingList shopList = shoppingList.GetShoppingListByIndex(index);
        // // Debug.Log(shopList);
        // // Debug.Log(shoppingList.ToString());
        //
        inventory.CompleteShoppingList(shopList);
        shoppingList.RemoveList(shopList);
        // Debug.Log($"Updated List Size: {shoppingList.GetAllShoppingLists().Count}");
        shoppingList.UpdateListsEvent.Invoke(shoppingList.GetAllShoppingLists());
        CompletableOptions();
        UIUpdate(completeableShoppingListIdx, true);
        soundManager.Play("Register");
        //base.CompleteAction(index);

        listCompleted++;
        if (listCompleted > 0 && !completed)
        {
            print("Success");
            completed = true;

            level1Tutorial.RegisterToTrash();
        }
    }
}
