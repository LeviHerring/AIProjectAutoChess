using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedFighting : StateManager<RangedUnit>
{
    public override void Execute(RangedUnit parentClass)
    {

        if (parentClass.health > 0)
        {
            parentClass.StartCoroutine(parentClass.Fighting());

            if (parentClass.bullets <= 0)
            {
                parentClass.ChangeState(new RangedReloading());
            }
        }
      
    }
}
