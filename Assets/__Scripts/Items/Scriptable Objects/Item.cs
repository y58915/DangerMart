using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemCategory;
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
