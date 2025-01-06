using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBasicUnitDesire : EnemyDesires
{
    private int unitCost;

    public SpawnBasicUnitDesire(int cost) : base("Spawn Basic Unit")
    {
        unitCost = cost;
    }

    public override void CalculateDesire(EnemyMouse ai)
    {
        // Desire increases if the AI has enough money and needs more units
        DesireVal = ai.GetMoney() >= unitCost ? ai.GetMoney() / 10f : 0; // Example scaling
    }
}