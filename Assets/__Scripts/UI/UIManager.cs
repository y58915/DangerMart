using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected GameObject HUD;
    [SerializeField] protected GameObject ShoppingListPanel;
    [SerializeField] protected GameObject InventoryPanel;
    [SerializeField] protected GameObject PausePanel;
    [SerializeField] protected GameObject CashRegister;
    [SerializeField] protected GameObject GameOverPanel;

    [SerializeField] protected GameObject WinPanel;

    [SerializeField] protected Button PauseButton;

    [SerializeField] private GameObject tutorialUI;

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

        LevelController.instance.winEvent.AddListener(ToggleWinPanel);
        ShoppingListPanel.SetActive(true);
        InventoryPanel.SetActive(true);
        PausePanel.SetActive(false);
        CashRegister.SetActive(false);
        GameOverPanel.SetActive(false);
        WinPanel.SetActive(false);
        LevelController.instance.winEvent.AddListener(EnableMedals);
        LevelController.instance.gameOverEvent.AddListener(EnableMedals);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TogglePausePanel()
    {
        if (GameOverPanel.activeInHierarchy) return;

        PausePanel.SetActive(!PausePanel.activeInHierarchy);
        if (tutorialUI != null)
            tutorialUI.SetActive(!tutorialUI.activeInHierarchy);

        LevelController.instance.SetPause(PausePanel.activeInHierarchy);
    }

    protected void EnableMedals()
    {
    }


    public void ToggleGameOverPanel()
    {
        GameOverPanel.SetActive(!GameOverPanel.activeInHierarchy);
    }

    public void ToggleWinPanel()
    {
        WinPanel.SetActive(!WinPanel.activeInHierarchy);
    }
}
