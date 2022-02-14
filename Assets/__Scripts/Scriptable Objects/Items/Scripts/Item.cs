using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemCategory
{
    Tool,
    Food,
    Default     //why default?
}
public abstract class Item : ScriptableObject
{
    public string itemName;
    public ItemCategory itemCategory;
    [TextArea(10, 20)]
    public string itemDescription;
    public Image itemImage;
    public GameObject itemModelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
