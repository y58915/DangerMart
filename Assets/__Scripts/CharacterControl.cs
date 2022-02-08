using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent playerNav;
    public Animator playerAnim;
    public GameObject targetDestination;

    // Start is called before the first frame update
    void Start()
    {
        if( Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                targetDestination.transform.position = hitPoint.point;
                playerNav.SetDestination(hitPoint.point);
            }
        }

        //Change animator variables
        if (playerNav.velocity != Vector3.zero)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else if (playerAnim.velocity == Vector3.zero)
        {
            playerAnim.SetBool("isWalking", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
