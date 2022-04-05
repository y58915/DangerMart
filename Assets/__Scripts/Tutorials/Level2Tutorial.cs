using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject timerTutorial;

    private BoxCollider timerVolume;

    // Start is called before the first frame update
    void Start()
    {
        timerTutorial.SetActive(true);

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
            timerVolume.enabled = false;
        }
    }

}
