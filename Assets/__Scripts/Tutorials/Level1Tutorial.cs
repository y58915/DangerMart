using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Tutorial : MonoBehaviour
{
    [SerializeField]
    private Level1_UI levelUI;

    [SerializeField]
    private GameObject movementTutorial;

    [SerializeField]
    private GameObject collectionTutorial;

    [SerializeField]
    private GameObject registerTutorial;

    [SerializeField]
    private GameObject trashTutorial;

    [SerializeField]
    private GameObject scoreTutorial;

    [SerializeField]
    private CollectionArea_Tutorial watermelonArea;

    [SerializeField]
    private CollectionArea_Tutorial pineappleArea;

    private BoxCollider movementVolume;

    // Start is called before the first frame update
    void Start()
    {
        movementVolume = GetComponent<BoxCollider>();

        movementTutorial.SetActive(true);
        collectionTutorial.SetActive(false);
        registerTutorial.SetActive(false);
        trashTutorial.SetActive(false);
        scoreTutorial.SetActive(false);
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
            Debug.Log("Deactivating movement tutorial");

            movementVolume.enabled = false;
            Debug.Log("Deativating trigger volume");

            collectionTutorial.SetActive(true);
            Debug.Log("Activating collection tutorial");

            if (levelUI != null)
                Debug.Log("The level UI exists");
            else
                Debug.Log("The level UI DOES NOT exist");

            levelUI.ActivateInventoryPanel();

            Debug.Log("Activating inventory panel");

            watermelonArea.canCollect = true;
            Debug.Log("Activating watermelon collection area");
        }
    }

    public void CollectionToRegister()
    {
        collectionTutorial.SetActive(false);
        registerTutorial.SetActive(true);
        levelUI.ActiveListPanel();
    }

    public void RegisterToTrash()
    {
        registerTutorial.SetActive(false);
        trashTutorial.SetActive(true);
    }

    public void TrashToScore()
    {
        trashTutorial.SetActive(false);
        scoreTutorial.SetActive(true);

        levelUI.ActivateScorePanel();

        pineappleArea.canCollect = true;
    }

    public void FinishTutorial()
    {
        scoreTutorial.SetActive(false);
    }
}
