using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevCheat : MonoBehaviour
{
    [SerializeField] bool startWithInventory;
    [SerializeField] List<Item> inventory;
    [SerializeField] bool startWithShoppingList;
    [SerializeField] List<Item> shoppingListItems;


    [SerializeField] bool NoGameOver;


#if UNITY_EDITOR
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetCheat());
    }
#endif

    // Update is called once per frame
    void Update()
    {
    }
    
    IEnumerator SetCheat()
    {
        yield return new WaitForEndOfFrame();

        if (startWithInventory)
        {
            foreach (Item item in inventory)
            {
                Inventory.instance.addItemEvent.Invoke(item);
            }
        }

        if (startWithShoppingList)
        {
            ShoppingListManager.instance.AddSpecificList(shoppingListItems);
        }

        if (NoGameOver)
        {
            LevelController.instance.SetNoGameOver(NoGameOver);
        }
    }

    public void AddScore()
    {
        Score.instance.AddScore(200);
    }
}
