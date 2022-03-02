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
        [SerializeField] List<Text> CashRegisterUISlots;
        [SerializeField] GameObject CashRegisterObj;
        [HideInInspector] public UnityEvent CashRegisterUpdate;
        private List<ShoppingList> completable;

        // Start is called before the first frame update
        void Start()
        {
            inventory = Inventory.instance;
            shoppingList = ShoppingListManager.instance;
            inventory.updateInventoryEvent.AddListener(CompletableOptions);
        }

        // Update is called once per frame
        void Update()
        {
        }

        // temp solution for complete a shopping list
        public void CompleteAction(int completeIndex)
        {
            Debug.Log(completeIndex);
            // shoppingList.RemoveList(shoppingList.FindShoppingListIndex(completable[completeIndex]));
            inventory.CompleteShoppingList(completable[completeIndex]);
            ShoppingListManager.instance.ShoppingListCompleteEvent.Invoke(completable[completeIndex]);

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (completable.Count == 0)
                {
                    return;
                }
                Transform images = CashRegisterObj.transform.GetChild(0);
                Transform text = CashRegisterObj.transform.GetChild(1);
                // Only show list can be complete
                for (int i = 0; i < completable.Count; i++)
                {
                    text.GetChild(i).gameObject.SetActive(true);
                    images.GetChild(i).gameObject.SetActive(true);
                    CashRegisterUISlots[i].text = completable[i].ToString();

                }
                CashRegisterObj.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CashRegisterObj.SetActive(false);

            }
        }
        private void CompletableOptions()
        {
            completable = new List<ShoppingList>();
            List<Item> inventoryItems = inventory.container;
            Debug.Log(inventory.ToString());

            foreach (ShoppingList shopList in shoppingList.GetAllShoppingLists())
            {
                List<Item> cartItems = shopList.itemList;
                int count = 0;
                foreach (Item item in cartItems)
                {
                    if (inventoryItems.Contains(item)) count++;
                }
                if (count == cartItems.Count) completable.Add(shopList);
                
            }

            completable.ForEach(data =>
            {
                List<string> l = new List<string>();
                foreach (Item item in data.itemList)
                {
                    l.Add(item.itemName);
                }
                Debug.Log("Shop List："+string.Join(",", l) + "\n");
            });

            CashRegisterUpdate.Invoke();
        }
        
    }
}