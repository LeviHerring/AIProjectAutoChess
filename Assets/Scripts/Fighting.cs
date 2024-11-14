using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : StateManager<PawnUnit>
{
    public override void Execute(PawnUnit parentClass)
    {
        parentClass.damage += parentClass.health;
        parentClass.health = 0;

        Debug.Log("Banking Gold.... have" + parentClass.damage + "in inventory");
        parentClass.ChangeState(new Patrolling());
    }
}

