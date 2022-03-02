using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class EnergyBar : MonoBehaviour
{
    private int energyBarCount = 0;
    [HideInInspector] public UnityEvent<int> UpdateEnergyBar;

    [HideInInspector] public UnityEvent UseEnergyBar;

    public int maximumNeeded = 3;

    public static EnergyBar instance;

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

    void Start()
    {
        ShoppingListManager.instance.ShoppingListCompleteEvent.AddListener(AddEnergy);
        UseEnergyBar.AddListener(CharacterControl.instance.UseEnergy);
    }


    private void Update()
    {
    }

    private void AddEnergy(ShoppingList list)
    {
        if (energyBarCount < 3)
        {
            energyBarCount += 1;
            UpdateEnergyBar.Invoke(energyBarCount);
        }

        if (energyBarCount == maximumNeeded)
        {
            StartCoroutine(ActivateEnergy());
        }
    }

    IEnumerator ActivateEnergy()
    {
        yield return new WaitForSeconds(1);

        UseEnergyBar.Invoke();
        energyBarCount = 0;
        UpdateEnergyBar.Invoke(energyBarCount);
    }


}
