using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionArea : MonoBehaviour
{
    [Header("Item")]
    [SerializeField]
    private Item item;

    private GameObject myItem;
    private List<GameObject> inventory;

    // Start is called before the first frame update
    void Start()
    {
        myItem = Instantiate(item.itemModelPrefab, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add this collection area's item to the player's inventory 
    public void AddToInventory(List<GameObject> playerInventory)
    {
        playerInventory.Add(myItem);

        Destroy(myItem);
        myItem = Instantiate(item.itemModelPrefab, this.transform.position, Quaternion.identity);
        Score.Instance.AddScore(item.score);
    }
}
