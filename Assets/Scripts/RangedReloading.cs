using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedReloading : StateManager<RangedUnit>
{
    public override void Execute(RangedUnit parentClass)
    {

        if (parentClass.bullets <= 0 && parentClass.bullets != parentClass.clipSize)
        {
            parentClass.StartCoroutine(parentClass.Reload());
        }
        else 
        {
            parentClass.ChangeState(new RangedFighting());
        }
    }
}
