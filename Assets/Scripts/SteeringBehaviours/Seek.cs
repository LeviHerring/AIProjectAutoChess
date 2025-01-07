using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviourBase
{
    public PawnSteering vehicle; // Reference to the PawnSteering script
    public Vector3 SeekTargetPos; // Target position for the agent to seek
    public float StoppingDistance = 0.1f; // Threshold for considering the target reached

    void Awake()
    {
        vehicle = GetComponent<PawnSteering>();
        if (vehicle == null)
        {
            Debug.LogError("PawnSteering component not found on the GameObject!");
        }
        vehicle.AddBehavior(this, 2f);
    }

    public override Vector3 Calculate()
    {
        if (vehicle == null)
        {
            Debug.LogError("Vehicle is null in Seek script!");
            return Vector3.zero;
        }

        // Calculate the desired velocity towards the target
        Vector3 DesiredVelocity = (SeekTargetPos - transform.position).normalized * vehicle.MaxSpeed;

        // Return the steering force (desired velocity minus current velocity)
        return DesiredVelocity - vehicle.Velocity;
    }

    public void SetTarget(Vector3 targetPos)
    {
        SeekTargetPos = targetPos;
    }

    public bool HasReachedTarget
    {
        get
        {
            // Check if the agent is within the stopping distance of the target
            return Vector3.Distance(transform.position, SeekTargetPos) <= StoppingDistance;
        }
    }

    // Optional debug visualizer
    void OnDrawGizmos()
    {
        if (SeekTargetPos != Vector3.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, SeekTargetPos);
            Gizmos.DrawSphere(SeekTargetPos, 0.2f);
        }
    }
}