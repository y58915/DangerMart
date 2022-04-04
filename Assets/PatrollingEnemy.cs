using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemy : MonoBehaviour
{
    // Set type of enemy 
    public enum EnemyType { Stun, SlowDown, Steal };
    public EnemyType enemyType = EnemyType.Stun;

    NavMeshAgent enemy;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        waypointIndex = 0;
        UpdateDestination();
        // Debug.LogFormat("Start: Position => " + transform.position);
        // Debug.LogFormat("Start: Target => " + target);
        // Debug.LogFormat("Start: Index => " + waypointIndex);

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.LogFormat("Updating Distance" + Vector3.Distance(transform.position, target));
        // Debug.LogFormat("Updating Index" + waypointIndex);

        if (Vector3.Distance(transform.position, target) < 1)
        {
            // Debug.LogFormat("Update: Position => " + transform.position);
            // Debug.LogFormat("Update: Target => " + target);

            IterateWaypointIndex();
            // Debug.LogFormat("Update: Index => " + waypointIndex);
            // Debug.LogFormat("Iterate Waypoint Index" + waypointIndex);

            UpdateDestination();
        }
    }

    void UpdateDestination() 
    {
       target = waypoints[waypointIndex].position;
       enemy.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex ++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
