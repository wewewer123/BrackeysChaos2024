using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    public Transform[] waypoints; // number of empty game objects that represent the path in straight lines
    public float triggerRadius = 0.1f;
    public float speed = 5f; // enemy speed
    public bool testReversing = false;

    private bool reversing = false;
    public bool stopped = false;


    public int currentWaypointIndex = 0;
    Transform targetWaypoint; // set new target waypoint          

    void Start()
    {
        targetWaypoint = waypoints[currentWaypointIndex];
    }
    void FixedUpdate()
    {
        Move(targetWaypoint);
        if (IsNearWaypoint())
        {
            targetWaypoint = waypoints[NextWaypointIndex()];
        }
        if (testReversing)
        {
            Reverse();
            testReversing = false;
        }
    }

    void Move(Transform targetWaypoint)
    {
        if (!stopped)
        {
            Vector3 direction = (targetWaypoint.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime; // move enemy
        }
    }



    private bool IsNearWaypoint()
    {
        if (Vector3.Distance(transform.position, targetWaypoint.position) < triggerRadius) // if near target waypoint
        {
            return true;
        }
        return false;
    }

    private int NextWaypointIndex()
    {
        if (!reversing)
        {
            if (currentWaypointIndex < waypoints.Length -1)
            {
                currentWaypointIndex++;
                return currentWaypointIndex;
            }
            Stop();
        }
        else
        {
            if (currentWaypointIndex > 0)
            {
                currentWaypointIndex--;
                return currentWaypointIndex;
            }

        }
        return currentWaypointIndex;
    }
    private void Stop()
    {
        stopped = true;
    }

    public void Reverse()
    {
        reversing = !reversing;
        targetWaypoint = waypoints[NextWaypointIndex()];
    }

    void OnDrawGizmos() // this is so we can visualise the path
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }

}