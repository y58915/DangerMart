using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject energyTutorialOne;

    [SerializeField]
    private GameObject energyTutorialTwo;

    [SerializeField]
    private GameObject powerUpTutorial;

    // Start is called before the first frame update
    void Start()
    {
        energyTutorialOne.SetActive(true);
        energyTutorialTwo.SetActive(true);
        powerUpTutorial.SetActive(false);
    }

    public void CloseTutorial(GameObject tutorial)
    {
        tutorial.SetActive(false);
    }

    public void EnergyToPowerUp()
    {
        energyTutorialTwo.SetActive(false);
        powerUpTutorial.SetActive(true);
    }
}
