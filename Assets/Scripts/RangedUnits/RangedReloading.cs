using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedReloading : StateManager<RangedUnit>
{
    public override void Enter(RangedUnit parentClass)
    {
        Debug.Log("Entering Reloading state.");
    }

    public override void Execute(RangedUnit parentClass)
    {
        if (!parentClass.isReloading)
        {
            parentClass.StartCoroutine(parentClass.Reload());
        }

        // Transition back to fighting if the clip is fully reloaded
        if (parentClass.bullets >= parentClass.clipSize)
        {
            parentClass.ChangeState(new RangedFighting());
        }

        if(parentClass.health <= 0)
        {
            parentClass.ChangeState(new RangedDeath()); 
        }

        if (parentClass.CurrentTarget != null)
        {
            if (parentClass.CurrentTarget.health <= 0)
            {
                Mouse.Instance.AddMoney(parentClass.CurrentTarget.price);
                parentClass.ChangeState(new RangedWalking());
                parentClass.CurrentTarget = null; 
            }
        }
          
    }

    public override void Exit(RangedUnit parentClass)
    {
        parentClass.isReloading = false;
        Debug.Log("Exiting Reloading state.");
    }
}
