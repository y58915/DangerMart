using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUpList;
    [SerializeField] private int MAX_NUMBER_OF_POWERUPS_ON_MAP = 5;
    [SerializeField] private int AVERAGE_SPAWNING_TIME = 15;

    private int MAP_LEFT_BOARDER = -10;
    private int MAP_RIGHT_BOARDER = 3;
    private int MAP_LOWER_BOARDER = -6;
    private int MAP_UPPER_BOARDER = 2;
    

    private int numberOfPowerups = 0;
    private Vector3 position;

    private float OBSTACLE_CHECK_RADIUS = 0.49f;
    private bool isSpawning = false;

    void Start()
    {
    }

    void Update() 
    {
        // Start a new spawning coroutine when last one is complete
        if (isSpawning == false) {
            isSpawning = true;
            StartCoroutine(CreatePowerup());
        }
    }

    private IEnumerator CreatePowerup() 
    {
        while (numberOfPowerups < MAX_NUMBER_OF_POWERUPS_ON_MAP) {
            
            // Wait 15 - 20 seconds before creating next powerup
            yield return new WaitForSeconds(AVERAGE_SPAWNING_TIME+Random.Range(-3, 4));

            int index = Random.Range(0, powerUpList.Count);
            GameObject powerUp = powerUpList[index];
            
            bool validPosition = false;
            
            // Generate a valid position that does not collide with any other 3D objects on the map
            while (!validPosition) {
                position = new Vector3(Random.Range(MAP_LEFT_BOARDER, MAP_RIGHT_BOARDER+1), 0.5f, Random.Range(MAP_LOWER_BOARDER, MAP_UPPER_BOARDER+1));
                Collider[] colliders = Physics.OverlapSphere(position, OBSTACLE_CHECK_RADIUS);

                if (colliders.Length == 0)
                    validPosition = true;
            }
            
            Instantiate(powerUp, position, Quaternion.identity);

            numberOfPowerups += 1;
        }

        // Terminate the current coroutine
        isSpawning = false;
    }

    public void DecrementNumberOfPowerUps() {
        numberOfPowerups -= 1;
    }
}

