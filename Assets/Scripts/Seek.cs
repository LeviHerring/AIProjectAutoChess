using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviourBase
{
    public Vector3 SeekTargetPos;
    public override Vector3 Calculate()
    {
        PawnSteering vehicle = GetComponent<PawnSteering>();
        Vector3 DesiredVelocity = (SeekTargetPos - transform.position).normalized * vehicle.MaxSpeed;

        return (DesiredVelocity - vehicle.Velocity);
    }
}
