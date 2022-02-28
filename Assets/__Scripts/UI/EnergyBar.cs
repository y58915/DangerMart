using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class EnergyBar : MonoBehaviour
{
    private float energyBarCount = 0;
    [HideInInspector] public UnityEvent<float,float> UpdateEnergyBar;

    [HideInInspector] public UnityEvent UseEnergy;

    public float maximumNeeded = 3;

    public static EnergyBar instance;
    

    void Start()
    {
        ShoppingListManager.instance.ShoppingListCompleteEvent.AddListener(AddEnergy);
        UseEnergy.AddListener(CharacterControl.instance.UseEnergy);
    }


    private void Update()
    {
        if (energyBarCount == maximumNeeded)
        {   
            UseEnergy.Invoke();
            energyBarCount = 0;
            UpdateEnergyBar.Invoke(energyBarCount, maximumNeeded);
        }
    }

    private void AddEnergy(ShoppingList list)
    {
        if (energyBarCount < 3)
        {
            energyBarCount += 1;
            UpdateEnergyBar.Invoke(energyBarCount,maximumNeeded);
        }
    }




}
