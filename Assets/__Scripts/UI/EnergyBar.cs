using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class EnergyBar : MonoBehaviour
{
    private float energyBarCount = 0;
    [HideInInspector] public UnityEvent<float,float> UpdateEnergyBar;

    [HideInInspector] public UnityEvent UseEnergyBar;

    public float maximumNeeded = 3;

    public static EnergyBar instance;
    

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        ShoppingListManager.instance.ShoppingListCompleteEvent.AddListener(AddEnergy);
        UseEnergyBar.AddListener(CharacterControl.instance.UseEnergy);
    }


    private void Update()
    {
        if (energyBarCount == maximumNeeded)
        {   
            UseEnergyBar.Invoke();
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
