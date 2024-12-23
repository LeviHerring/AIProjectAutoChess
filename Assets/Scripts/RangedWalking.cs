using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWalking : StateManager<RangedUnit>
{
    public override void Execute(RangedUnit parentClass)
    {
        Debug.Log("Patrolling");

        parentClass.Invoke("Move", parentClass.speed);


        if (parentClass.health >= 6)
        {
            parentClass.ChangeState(new RangedFighting());
        }

    }

}
