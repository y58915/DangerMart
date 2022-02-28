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

    [SerializeField]
    private Item wildCardRef;

    private CollectionArea targetCollectionArea;
    private bool inputEnabled = true;
    private float originalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = playerNav.speed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PowerUp>() != null)
        {
            switch(other.gameObject.GetComponent<PowerUp>().powerupItem.type)
            {
                case PowerupItem.PowerUpAbility.SpeedBoost:
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
        print("You got a wildcard");
        Inventory.instance.addItemEvent.Invoke(wildCardRef);
    }

    private void IncreaseEnergy()
    {
        // TODO: Implement once energy bar gets added 
    }
    
}
