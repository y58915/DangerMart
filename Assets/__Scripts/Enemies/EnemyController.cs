using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    // Set type of enemy 
    public enum EnemyType { Stun, SlowDown, Steal };
    public EnemyType enemyType = EnemyType.Stun;

    public NavMeshAgent playerNav;

    // Borders of the floor
    public float LEFT_BORDER;
    public float RIGHT_BORDER;
    public float LOWER_BORDER;
    public float UPPER_BORDER;

    private float translationalSpeed = 0.2f;
    private float rotationalSpeed = 360.0f;

    private bool isWandering = false;
    private bool isWalking = false;
    private bool isTurning = false; 


    private void Start()
    {
    }

    private void Update()
    {
        if (isWandering == false) {
            
            isWandering = true;
            
            StartCoroutine(Wander());
        }

        if (isWalking) {            
            transform.position += transform.forward * Time.fixedDeltaTime * translationalSpeed;

            // enemy should not cross the border
            if (transform.position.z > UPPER_BORDER || transform.position.z < LOWER_BORDER ||
                    transform.position.x > RIGHT_BORDER || transform.position.x < LEFT_BORDER) {
                
                Debug.LogFormat("Crossed border!!");
                ReturnToFloor();

            }
        }
        if (isTurning) {
            transform.Rotate(transform.up * Time.fixedDeltaTime * rotationalSpeed);
        }
    }

    private void ReturnToFloor() {
        
        float rotationY= transform.rotation.eulerAngles.y;
        Debug.LogFormat("rotation Y " + rotationY);

        // once reachinging the border, the enemy reflects off it and returns to the map
        if (transform.position.z > UPPER_BORDER || transform.position.z < LOWER_BORDER) {
            // for upper and lower borders, theta -> 180 - theta, or rotate it by 180 - 2*theta degrees
            transform.Rotate(transform.up * (180.0f-2*rotationY));

        }
        else if (transform.position.x > RIGHT_BORDER || transform.position.x < LEFT_BORDER) {
            // for left and right borders, theta -> -theta, or rotate it by -2*theta degrees
            transform.Rotate(transform.up * (-2*rotationY));
        }
        

        transform.position += transform.forward * Time.fixedDeltaTime * translationalSpeed;
    }
    

    private IEnumerator Wander() {
    
        // Wandering starts with Walking for a random number seconds
        isWalking = true;
        yield return new WaitForSeconds(Random.Range(2, 10));
        isWalking = false; 


        // Followed by Turn left or Right
        isTurning = true;
        yield return new WaitForSeconds(Random.Range(2, 5)*0.1f);
        isTurning = false;

        if (isWandering) {
        }
        

        isWandering = false;
    }


}
