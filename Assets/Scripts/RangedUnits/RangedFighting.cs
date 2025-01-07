using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedFighting : StateManager<RangedUnit>
{
    public override void Enter(RangedUnit parentClass)
    {
        Debug.Log("Entering Fighting state.");
    }

    public override void Execute(RangedUnit parentClass)
    {
        if (parentClass.bullets > 0)
        {
            if (!parentClass.isShooting)
            {
                parentClass.StartCoroutine(parentClass.Fighting());
            }
        }
        else
        {
            // Transition to reloading when out of bullets
            parentClass.ChangeState(new RangedReloading());
        }

        if (parentClass.health <= 0)
        {
            parentClass.ChangeState(new RangedDeath());
        }

        if(parentClass.CurrentTarget != null)
        {
            if (parentClass.CurrentTarget.health <= 0)
            {
                if(parentClass.CurrentTarget.Team != PlayerTypes.Team.Player)
                {
                    Mouse.Instance.AddMoney(parentClass.CurrentTarget.priceOnDeath);
                }
                else if(parentClass.CurrentTarget.Team != PlayerTypes.Team.Enemy)
                {

                }
                parentClass.ChangeState(new RangedWalking());
                parentClass.CurrentTarget = null;
            }
        }
      
    }

    public override void Exit(RangedUnit parentClass)
    {
        parentClass.isShooting = !parentClass.isShooting; 
        Debug.Log("Exiting Fighting state.");
    }
}
