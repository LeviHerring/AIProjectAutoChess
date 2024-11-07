using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Patrolling : StateManager<PawnUnit>
{
    public override void Execute(PawnUnit parentClass)
    {
        Debug.Log("Mining for gold...");

        

        if (parentClass.m_Gold >= 6)
        {
            parentClass.ChangeState(new BankingGold());
        }

    }


    public void Walking()
    {
        Debug.Log("Rigidbody velocity moving");
        parentClass.m_Gold++;
    }
}
