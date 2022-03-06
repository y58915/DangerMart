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
    [SerializeField] GameObject GameOverPanel;

    [SerializeField] Button PauseButton;

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
        PauseButton.onClick.AddListener(TogglePausePanel);

        LevelController.instance.gameOverEvent.AddListener(ToggleGameOverPanel);

        ShoppingListPanel.SetActive(true);
        InventoryPanel.SetActive(true);
        PausePanel.SetActive(false);
        CashRegister.SetActive(false);
        GameOverPanel.SetActive(false);
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
        if (GameOverPanel.activeInHierarchy) return;

        PausePanel.SetActive(!PausePanel.activeInHierarchy);

        LevelController.instance.SetPause(PausePanel.activeInHierarchy);
    }

    public void ToggleGameOverPanel()
    {
        GameOverPanel.SetActive(!GameOverPanel.activeInHierarchy);
    }
}
