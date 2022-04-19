using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_UI : UIManager
{
    private void Start()
    {
        PauseButton.onClick.AddListener(TogglePausePanel);
        LevelController.instance.winEvent.AddListener(ToggleWinPanel);

        ShoppingListPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        PausePanel.SetActive(false);
        //CashRegister.SetActive(false);
        WinPanel.SetActive(false);
        HUD.transform.Find("Score").gameObject.SetActive(false);
        HUD.transform.Find("Timer").gameObject.SetActive(false);
        HUD.transform.Find("EnergyBar").gameObject.SetActive(false);
        HUD.transform.Find("Debuff").gameObject.SetActive(false);
    }

    public void ActivateInventoryPanel()
    {
        InventoryPanel.SetActive(true);
    }

    public void ActiveListPanel()
    {
        ShoppingListPanel.SetActive(true);
    }

    public void ActivateScorePanel()
    {
        HUD.transform.Find("Score").gameObject.SetActive(true);
    }
}
