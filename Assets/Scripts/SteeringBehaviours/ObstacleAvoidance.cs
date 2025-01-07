using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : SteeringBehaviourBase
{
    public float RayLength = 10f;  // Length of the rays
    public float RayAngle = 45f;  // Angle for side whiskers
    public float Weight = 10.0f;   // Higher weight to give avoidance priority

    public PawnSteering vehicle;

    void Awake()
    {
        vehicle = GetComponent<PawnSteering>();
        if (vehicle == null)
        {
            Debug.LogError("PawnSteering component not found on the GameObject!");
        }
    }

    public override Vector3 Calculate()
    {
        Vector3 avoidanceForce = Vector3.zero;

        // Forward ray
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, RayLength))
        {
            avoidanceForce += GetAvoidanceForce(hit);
        }

        // Left whisker ray
        Vector3 leftDirection = Quaternion.Euler(0, -RayAngle, 0) * transform.forward;
        if (Physics.Raycast(transform.position, leftDirection, out hit, RayLength))
        {
            avoidanceForce += GetAvoidanceForce(hit);
        }

        // Right whisker ray
        Vector3 rightDirection = Quaternion.Euler(0, RayAngle, 0) * transform.forward;
        if (Physics.Raycast(transform.position, rightDirection, out hit, RayLength))
        {
            avoidanceForce += GetAvoidanceForce(hit);
        }

        // Clamp the force to the maximum allowed by the vehicle
        return Vector3.ClampMagnitude(avoidanceForce, vehicle.MaxForce);
    }

    private Vector3 GetAvoidanceForce(RaycastHit hit)
    {
        // Calculate the avoidance force based on the wall's normal and penetration distance
        Vector3 wallNormal = hit.normal;
        float penetrationDistance = RayLength - hit.distance; // Closer obstacles have higher priority
        return wallNormal * (1f / penetrationDistance); // Inverse distance weighting for stronger repulsion
    }
}