using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power Up Generator", menuName = "PowerUpGenerator")]
public class PowerUpGenerator : ScriptableObject
{
    public List<PowerupItem> powerupItemList;
    public int MAX_NUMBER_OF_POWERUPS = 2;

    private int numberOfPowerups = 0;
    private int xPos = -2;
    private int zPos = -1;


    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(CreatePowerup());
    }

    private IEnumerator CreatePowerup() 
    {
        int index = Random.Range(0, powerupItemList.Count);
        PowerupItem powerupItem = powerupItemList[index];
        while (numberOfPowerups < MAX_NUMBER_OF_POWERUPS) {
            xPos = -Random.Range(2, 4);
            zPos = -Random.Range(5, 6);
            Instantiate(powerupItem.itemModelPrefab, new Vector3(xPos, 0.5f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(6f);
            numberOfPowerups += 1;
        }
    }

    public void DecrementNumberOfPowerUps() {
        numberOfPowerups -= 1;
    }
}

