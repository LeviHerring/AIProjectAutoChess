using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviourBase
{
    public PawnSteering vehicle; // Ensure this is public or set via code

    void Awake()
    {
        vehicle = GetComponent<PawnSteering>();
        if (vehicle == null)
        {
            Debug.LogError("PawnSteering component not found on the GameObject!");
        }
    }

    public Vector3 SeekTargetPos;

    public override Vector3 Calculate()
    {
        if (vehicle == null)
        {
            Debug.LogError("Vehicle is null in Seek script!");
            return Vector3.zero;
        }

        Vector3 DesiredVelocity = (SeekTargetPos - transform.position).normalized * vehicle.MaxSpeed;
        return (DesiredVelocity - vehicle.Velocity);
    }

    public void SetTarget(Vector3 targetPos)
    {
        SeekTargetPos = targetPos;
    }

    public bool HasReachedTarget => Vector3.Distance(transform.position, SeekTargetPos) < 0.1f; // Example threshold
}
