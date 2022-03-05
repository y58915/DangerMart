using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace __Scripts
{
    public class CashRegister : MonoBehaviour
    {
        private Inventory inventory;
        private ShoppingListManager shoppingList;
        [SerializeField] GameObject ShoppingListPanel;
        // [SerializeField] GameObject CashRegisterObj;
        [HideInInspector] public UnityEvent CashRegisterUpdate;
        private List<ShoppingList> completable;
        private List<int> completeableShoppingListIdx;

        // Start is called before the first frame update
        void Start()
        {
            inventory = Inventory.instance;
            shoppingList = ShoppingListManager.instance;
            // inventory.updateInventoryEvent.AddListener(CompletableOptions);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        // temp solution for complete a shopping list
        public void CompleteAction(int index)
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

        }

        private void UIUpdate(List<int> indices, bool isSelect)
        {
            foreach (int idx in indices)
            {
                Transform shoppingListSlot = ShoppingListPanel.transform.GetChild(idx);
                shoppingListSlot.GetComponent<Outline>().enabled = isSelect;
                shoppingListSlot.GetComponent<Image>().enabled = isSelect;
                shoppingListSlot.GetComponent<Button>().enabled = isSelect;
            }
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CompletableOptions();
                if (completeableShoppingListIdx.Count == 0)
                {
                    return;
                }

                // completeableShoppingListIdx = new List<int>();
                // for (int i = 0; i < shoppingList.GetAllShoppingLists().Count; i++)
                // {
                //     ShoppingList list = shoppingList.GetAllShoppingLists()[i];
                //     if (completable.Contains(list)) completeableShoppingListIdx.Add(i);
                // }
                UIUpdate(completeableShoppingListIdx, true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                UIUpdate(Enumerable.Range(0, shoppingList.MAXSIZE).ToList(), false);
            }
        }
        private void CompletableOptions()
        {
            completable = new List<ShoppingList>();
            List<Item> inventoryItems = inventory.container;
            completeableShoppingListIdx = new List<int>();
            int idx = 0;
            foreach (ShoppingList shopList in shoppingList.GetAllShoppingLists())
            {
                List<Item> cartItems = shopList.itemList;
                int count = 0;
                foreach (Item item in cartItems)
                {
                    if (inventoryItems.Contains(item)) count++;
                }
                if (count == cartItems.Count)
                {
                    completable.Add(shopList);
                    completeableShoppingListIdx.Add(idx);
                }

                idx++;
            }
            // Debug.Log($"completable: {completable.ToString()}");

            CashRegisterUpdate.Invoke();
        }
        
    }
}