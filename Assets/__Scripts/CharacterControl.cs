using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Analytics;

public class CharacterControl : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent playerNav;
    public Animator playerAnim;
    public GameObject targetDestination;
    public LayerMask groundLayerMask;
    public LayerMask collectionLayerMask;

    // Start is called before the first frame update
    void Start()
    {
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

        if (Physics.Raycast(ray, out hitPoint, 1000f, collectionLayerMask))
        {
            // Move in fron of collection area to collect item
            targetDestination.transform.position = hitPoint.collider.gameObject.GetComponent<CollectionArea>().collectionPosition.position;
            playerNav.SetDestination(hitPoint.collider.gameObject.GetComponent<CollectionArea>().collectionPosition.position);
        }
        else if (Physics.Raycast(ray, out hitPoint, 1000f, groundLayerMask))
        {
            // Move to point on floor 
            targetDestination.transform.position = hitPoint.point;
            playerNav.SetDestination(hitPoint.point);
        }

        
    }

    public void OnTriggerEnter(Collider other) 
    {
        var itemOnShelf = other.GetComponent<ItemOnShelf> ();
        if (itemOnShelf) {
            Inventory.instance.AddItem(itemOnShelf.item, 1);
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "Collected Item",
            new Dictionary<string, object>
            {
                {
                    "Item", other.gameObject.name
                }
            });
            Debug.Log("Item result: " + analyticsResult);
            Destroy(other.gameObject);
        }
    }
}
