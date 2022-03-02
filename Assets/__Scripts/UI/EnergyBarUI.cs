using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnergyBarUI : MonoBehaviour
{

    private Slider energyBar;
    
    // Start is called before the first frame update
    void Start()
    {
        //energyBar = this.GetComponent<Slider>();
        //EnergyBar.instance.UpdateEnergyBar.AddListener(UpdateEnergyBarSlider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateEnergyBarSlider(float listCompeleted, float maximumNeeded)
    {
        //energyBar.value = listCompeleted / maximumNeeded;
    }
}
