using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnergyBarUI : MonoBehaviour
{

    [SerializeField]private Image firstBar;
    [SerializeField]private Image secondBar;
    [SerializeField]private Image thirdBar;
    
    // Start is called before the first frame update
    void Start()
    {
        EnergyBar.instance.UpdateEnergyBar.AddListener(UpdateUI);
        UpdateUI(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI(int i)
    {
        switch (i)
        {
            case 1: firstBar.color = Color.yellow; secondBar.color = Color.white; thirdBar.color = Color.white; break;
            case 2: firstBar.color = Color.yellow; secondBar.color = new Color(1, 0.6f, 0, 1); thirdBar.color = Color.white; break;
            case 3: firstBar.color = Color.yellow; secondBar.color = new Color(1, 0.6f, 0, 1); thirdBar.color = Color.red; break;
            default: firstBar.color = Color.white; secondBar.color = Color.white; thirdBar.color = Color.white; break;
        }
    }
}
