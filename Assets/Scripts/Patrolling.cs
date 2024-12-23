using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : StateManager<PawnUnit>
{
    public override void Enter(PawnUnit parentClass)
    {
        Debug.Log("Entering Patrolling state.");
        // Initialization logic for the Patrolling state
    }

    public override void Execute(PawnUnit parentClass)
    {
        Debug.Log("Executing Patrolling state.");
        parentClass.Move(); // Call the Move method during patrolling
    }

    public override void Exit(PawnUnit parentClass)
    {
        Debug.Log("Exiting Patrolling state.");
        // Cleanup logic for the Patrolling state
    }


}
