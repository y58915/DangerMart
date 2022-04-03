using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Tutorial : MonoBehaviour
{
    [SerializeField]
    private Level1_UI levelUI;

    [SerializeField]
    private GameObject movementTutorial;

    public GameObject collectionTutorial;

    [SerializeField]
    private CollectionArea_Tutorial watermelonArea;

    private BoxCollider movementVolume;

    // Start is called before the first frame update
    void Start()
    {
        movementVolume = GetComponent<BoxCollider>();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterControl>() != null)
        {
            movementTutorial.SetActive(false);
            movementVolume.enabled = false;

            collectionTutorial.SetActive(true);
            levelUI.ActivateInventoryPanel();

            watermelonArea.canCollect = true;
        }
    }
}
