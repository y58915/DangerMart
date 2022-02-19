using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject ShoppingListPanel;
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] Button InventoryButton;

    Keyboard keyboard;

    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        keyboard = Keyboard.current;
        InventoryButton.onClick.AddListener(ToggleInventoryPanel);

        ShoppingListPanel.SetActive(true);
        InventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleInventoryPanel()
    {
        InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
    }
}
