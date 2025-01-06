using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : StateManager<PawnUnit>
{
    public override void Enter(PawnUnit parentClass)
    {
        Debug.Log("Entering Attacking state.");
        // Setup logic for the Attacking state
    }

    public override void Execute(PawnUnit parentClass)
    {
        Debug.Log("Executing Attacking state.");
        parentClass.StartCoroutine(parentClass.Fighting());

        if(parentClass.health <= 0)
        {
            parentClass.ChangeState(new PawnDeath());
        }
    }

    public override void Exit(PawnUnit parentClass)
    {
        Debug.Log("Exiting Attacking state.");
        // Cleanup logic for the Attacking state
    }
}

