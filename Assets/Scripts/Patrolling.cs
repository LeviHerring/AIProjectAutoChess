using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : StateManager<PawnUnit>
{
    public override void Execute(PawnUnit parentClass)
    {
        Debug.Log("Patrolling");

        parentClass.Invoke("Move", parentClass.speed);


        if (parentClass.health >= 6)
        {
            parentClass.ChangeState(new Fighting());
        }

    }


}
