using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject ShoppingListPanel;
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject CashRegister;

    [SerializeField] Button InventoryButton;
    [SerializeField] Button PauseButton;

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
        // InventoryButton.onClick.AddListener(ToggleInventoryPanel);
        PauseButton.onClick.AddListener(TogglePausePanel);

        ShoppingListPanel.SetActive(true);
        InventoryPanel.SetActive(true);
        PausePanel.SetActive(false);
        CashRegister.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
    // public void ToggleInventoryPanel()
    // {
    //     InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
    // }

    public void TogglePausePanel()
    {
        PausePanel.SetActive(!PausePanel.activeInHierarchy);
    }
}
