using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnDeath : StateManager<PawnUnit>
{
    public override void Enter(PawnUnit parentClass)
    {

    }

    public override void Execute(PawnUnit parentClass)
    {
        Object.Destroy(parentClass.gameObject);
    }

    public override void Exit(PawnUnit parentClass)
    {

    }
}
