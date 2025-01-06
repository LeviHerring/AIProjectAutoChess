using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : SteeringBehaviourBase
{
    public Vector3[] waypoints;
    public float arrivalThreshold = 1.0f;
    public bool loopPath = true;

    private int currentWaypointIndex = 0;
    private PawnSteering pawnSteering;

    void Start()
    {
        pawnSteering = GetComponent<PawnSteering>();

        if (pawnSteering == null)
        {
            Debug.LogError("PawnSteering component not found on this GameObject!");
        }

        if (waypoints.Length > 0)
        {
            currentWaypointIndex = 0;
        }
        else
        {
            Debug.LogError("No waypoints assigned!");
        }
    }

    public override Vector3 Calculate()
    {
        if (waypoints.Length == 0) return Vector3.zero;

        Vector3 currentWaypoint = waypoints[currentWaypointIndex];
        Vector3 toWaypoint = currentWaypoint - transform.position;

        // Check if the agent is close enough to the waypoint
        if (toWaypoint.magnitude < arrivalThreshold)
        {
            SetNextWaypoint();
        }

        return toWaypoint.normalized * pawnSteering.MaxForce; // Scale by the steering weight or MaxForce
    }

    void SetNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length - 1)
        {
            currentWaypointIndex++;
        }
        else if (loopPath)
        {
            currentWaypointIndex = 0;
        }
        else
        {
            Debug.Log("Reached the last waypoint.");
        }
    }
}