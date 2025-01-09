using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : StateManager<PawnUnit>
{
    private Seek seekBehavior;
    public override void Enter(PawnUnit parentClass)
    {
        Debug.Log("Entering Patrolling state.");
        seekBehavior = parentClass.GetComponent<Seek>();
        if (seekBehavior == null)
        {
            seekBehavior = parentClass.gameObject.AddComponent<Seek>();
        }

        // Initialize vehicle reference in Seek
        if (seekBehavior.vehicle == null)
        {
            seekBehavior.vehicle = parentClass.GetComponent<PawnSteering>();
            if (seekBehavior.vehicle == null)
            {
                Debug.LogError("PawnSteering component not found! Ensure it is attached to the GameObject.");
                return;
            }
        }

        // Set speed and target position
        seekBehavior.vehicle.MaxSpeed = parentClass.speed;
        if (seekBehavior != null)
        {
            if(parentClass.CurrentTarget != null)
            {
                seekBehavior.SetTarget(new Vector3(parentClass.CurrentTarget.transform.position.x, parentClass.transform.position.y, parentClass.CurrentTarget.transform.position.z));
            }
           
        }
        // Initialization logic for the Patrolling state

    }

    public override void Execute(PawnUnit parentClass)
    {
        Debug.Log("Executing Patrolling state.");

        if (parentClass.health <= 0)
        {
            parentClass.ChangeState(new PawnDeath());
        }

        if (seekBehavior != null)
        {
            if (parentClass.CurrentTarget != null)
            {
                seekBehavior.SetTarget(new Vector3(parentClass.CurrentTarget.transform.position.x, parentClass.transform.position.y, parentClass.CurrentTarget.transform.position.z));
            }

        }

        if(parentClass.CurrentTarget != null)
        {
            if (Vector3.Distance(parentClass.transform.position, parentClass.CurrentTarget.transform.position) < 10)
            {
                seekBehavior.vehicle.MaxSpeed = 0;
                parentClass.ChangeState(new Fighting());
            }
        }
        else if(parentClass.CurrentTarget == null)
        {
            parentClass.ChangeState(new Walking()); 
        }
       
    }

    public override void Exit(PawnUnit parentClass)
    {
        Debug.Log("Exiting Patrolling state.");
        seekBehavior.vehicle.RemoveBehavior(seekBehavior);
        Object.Destroy(seekBehavior);
        // Cleanup logic for the Patrolling state
    }


}
