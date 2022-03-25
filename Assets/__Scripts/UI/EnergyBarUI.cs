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
    [SerializeField]private Color notFilled;
    
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
            case 1: firstBar.color = Color.white; secondBar.color = notFilled; thirdBar.color = notFilled; break;
            case 2: firstBar.color = Color.white; secondBar.color = Color.white; thirdBar.color = notFilled; break;
            case 3: firstBar.color = Color.white; secondBar.color = Color.white; thirdBar.color = Color.white; break;
            default: firstBar.color = notFilled; secondBar.color = notFilled; thirdBar.color = notFilled; break;
        }
    }
}
