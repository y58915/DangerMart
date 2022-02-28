using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts
{
    public class CashRegister : MonoBehaviour
    {
        private Inventory inventory;
        private ShoppingListManager shoppingList;
        [SerializeField] List<Text> CashRegisterUISlots;
        [SerializeField] GameObject CashRegisterObj;


        // Start is called before the first frame update
        void Start()
        {
            inventory = Inventory.instance;
            shoppingList = ShoppingListManager.instance;

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void CompleteAction(int completeIndex)
        {
            List<ShoppingList> completable = CompletableOptions();
            shoppingList.RemoveList(shoppingList.FindShoppingListIndex(completable[completeIndex]));

            ShoppingListManager.instance.ShoppingListCompleteEvent.Invoke(completable[completeIndex]);

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                List<ShoppingList> completable = CompletableOptions();
                if (completable.Count == 0)
                {
                    return;
                }
                Transform images = CashRegisterObj.transform.GetChild(0);
                Transform text = CashRegisterObj.transform.GetChild(1);
                for (int i = 0; i < completable.Count; i++)
                {
                    text.GetChild(i).gameObject.SetActive(true);
                    images.GetChild(i).gameObject.SetActive(true);
                    CashRegisterUISlots[i].text = completable[i].ToString();

                    //Put when you to click

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
        private List<ShoppingList> CompletableOptions()
        {
            List<ShoppingList> completable = new List<ShoppingList>();
            List<Item> inventoryItems = inventory.container;
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

            return completable;

        }
    }
}