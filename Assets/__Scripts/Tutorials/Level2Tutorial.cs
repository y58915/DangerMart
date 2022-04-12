using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject timerTutorial;

    [SerializeField]
    private GameObject enemyTutorial;

    private BoxCollider timerVolume;

    // Start is called before the first frame update
    void Start()
    {
        timerTutorial.SetActive(true);
        enemyTutorial.SetActive(false);

        timerVolume = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterControl>() != null)
        {
            timerTutorial.SetActive(false);
            enemyTutorial.SetActive(true);
            timerVolume.enabled = false;

        }
    }

    public void CloseTutorial()
    {
        enemyTutorial.SetActive(false);
    }
}
