using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    public Transform[] waypoints; // number of empty game objects that represent the path in straight lines
    public float speed = 5f; // enemy speed
    public bool reversing = false;

    public int currentWaypointIndex = 0;
    Transform targetWaypoint; // set new target waypoint          

    void Start()
    {
        targetWaypoint = waypoints[currentWaypointIndex];
    }
    void FixedUpdate()
    {
        if (currentWaypointIndex >= 0 && currentWaypointIndex < waypoints.Length) // if the waypoint reached is not the last
        {
            Move(targetWaypoint);
            CheckWaypointDistance();
        } 
    }

    void OnDrawGizmos() // this is so we can visualise the path
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
    void Move(Transform targetWaypoint)
    {
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime; // move enemy
    }

    void ChangeDirection()
    {
        if (!reversing)
        {
            currentWaypointIndex--;
        }
        else
        {
            currentWaypointIndex++;
        }
        reversing = !reversing;

    }

    void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f) // if near target waypoint
        {
            if (!reversing && currentWaypointIndex < waypoints.Length)
            {
                currentWaypointIndex++; // shift target
            }else if (currentWaypointIndex > 0)
            {
                currentWaypointIndex--;
            }
            targetWaypoint = waypoints[currentWaypointIndex];
        }
    }
}