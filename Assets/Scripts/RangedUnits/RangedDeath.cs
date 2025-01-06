using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDeath : StateManager<RangedUnit>
{
    public override void Enter(RangedUnit parentClass)
    {

    }

    public override void Execute(RangedUnit parentClass)
    {
        Object.Destroy(parentClass.gameObject); 
    }

    public override void Exit(RangedUnit parentClass)
    {

    }

}
