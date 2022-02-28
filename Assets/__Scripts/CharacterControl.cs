using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Analytics;
using System.Linq;

public class CharacterControl : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent playerNav;
    public Animator playerAnim;
    public GameObject targetDestination;
    public LayerMask groundLayerMask;
    public LayerMask collectionLayerMask;
    public float distance = 0f;
    public int mouse_clicks = 0;

    [SerializeField]
    private Item wildCardRef;

    private CollectionArea targetCollectionArea;
    private bool inputEnabled = true;
    private float originalSpeed;
    private Vector3 last = Vector3.zero;
    private InputAction leftMouseClick;
    private bool invulernable = false;

    public float invulernablityTime = 5f;
    public static CharacterControl instance;

    private void Awake()
    {
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.performed += ctx => LeftMouseClicked();
        leftMouseClick.Enable();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void LeftMouseClicked()
    {
        mouse_clicks += 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = playerNav.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (last != Vector3.zero)
        {
            distance += Vector3.Distance(last, gameObject.transform.position);
        } 
        UpdateAnimations();
        last = gameObject.transform.position;
    }

    public void UpdateAnimations()
    {
        //Change animator variables
        if (playerNav.velocity.sqrMagnitude < 0.5f)
        {
            playerAnim.SetBool("isWalking", false);
        }
        else
        {
            playerAnim.SetBool("isWalking", true);
        }
    }

    public void OnMove()
    {
        //Debug.Log("Move to: " + Mouse.current.position.ReadValue());

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint, 1000f, collectionLayerMask) && inputEnabled)
        {
            // Set clicked on collection area as destination
            targetCollectionArea = hitPoint.collider.gameObject.GetComponent<CollectionArea>();

            // Move in fron of collection area to collect item
            targetDestination.transform.position = targetCollectionArea.collectionPosition.position;
            playerNav.SetDestination(targetCollectionArea.collectionPosition.position);

            // Activate collection trigger
            targetCollectionArea.collectionTrigger.enabled = true;
        }
        else if (Physics.Raycast(ray, out hitPoint, 1000f, groundLayerMask) && inputEnabled)
        {
            // Move to point on floor 
            targetDestination.transform.position = hitPoint.point;
            playerNav.SetDestination(hitPoint.point);
        }

        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (invulernable)
            return;
        
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            switch(collision.gameObject.GetComponent<EnemyController>().enemyType)
            {
                case EnemyController.EnemyType.Stun:
                    Stun();
                    break;
                case EnemyController.EnemyType.SlowDown:
                    SlowDown();
                    break;
                case EnemyController.EnemyType.Steal:
                    LoseItem();
                    break;
                default:
                    break;
            }
            AnalyticsResult analyticsResult_Enemies = Analytics.CustomEvent("Enemy Hit", new Dictionary<string, object> { { "Enemy", collision.gameObject.GetComponent<EnemyController>().enemyType } });
            Debug.Log("Enemy Hit: " + analyticsResult_Enemies);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PowerUp>() != null)
        {
            switch(other.gameObject.GetComponent<PowerUp>().powerupItem.type)
            {
                case PowerupItem.PowerUpAbility.SpeedBoost:
                    if (invulernable)
                        return;
                    SpeedUp();
                    break;
                case PowerupItem.PowerUpAbility.WildCard:
                    AddWildCard();
                    break;
                case PowerupItem.PowerUpAbility.EnergyDrink:
                    IncreaseEnergy();
                    break;
                default:
                    break;
            }
        }
    }

    private void Stun()
    {
        playerNav.isStopped = true;
        inputEnabled = false;

        StartCoroutine(StunTime(5.0f));
    }

    private IEnumerator StunTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerNav.isStopped = false;
        inputEnabled = true;
    }

    private void SlowDown()
    {
        playerNav.speed = originalSpeed * 0.25f;

        StartCoroutine(SpeedChange(5.0f));
    }

    private void SpeedUp()
    {
        playerNav.speed = originalSpeed * 2.0f;

        StartCoroutine(SpeedChange(5.0f));
    }

    private IEnumerator SpeedChange(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerNav.speed = originalSpeed;
    }

    private void LoseItem()
    {
        List<Item> items = new List<Item>(Inventory.instance.container);
        int index = Random.Range(0, items.Count);

        Inventory.instance.RemoveItem(items[index]);
    }

    private void AddWildCard()
    {
        Inventory.instance.addItemEvent.Invoke(wildCardRef);
    }

    private void IncreaseEnergy()
    {
        // ADD FUNCTIONALITY LATER 
    }

    public void UseEnergy()
    {
        invulernable = true;
        playerNav.speed = originalSpeed * 2.0f;
        StartCoroutine("RemoveEnergyBuff");
    }

    IEnumerator RemoveEnergyBuff()
    {
        yield return new WaitForSeconds(invulernablityTime);
        invulernable = false;
        playerNav.speed = originalSpeed;
    }
    
}
