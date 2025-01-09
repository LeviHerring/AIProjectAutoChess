using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : StateManager<PawnUnit>
{
    int enemyCost; 
    public override void Enter(PawnUnit parentClass)
    {
        Debug.Log("Entering Attacking state.");
        enemyCost = parentClass.CurrentTarget.priceOnDeath; 
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

        if(parentClass.CurrentTarget == null)
        {
            if(parentClass.Team == PlayerTypes.Team.Player)
            {
                Mouse.Instance.AddMoney(enemyCost);
            }
            else if (parentClass.Team == PlayerTypes.Team.Enemy)
            {
                EnemyMouse.instance.AddMoney(enemyCost);
            }
            
            parentClass.ChangeState(new Walking()); 
        }
    }

    public override void Exit(PawnUnit parentClass)
    {
        parentClass.isLockedOn = false; 
        enemyCost = 0; 
        Debug.Log("Exiting Attacking state.");
        // Cleanup logic for the Attacking state
    }
}

