using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefensiveUnitDesire : EnemyDesires
{
    private int unitCost;

    public SpawnDefensiveUnitDesire(int cost) : base("Spawn Defensive Unit")
    {
        unitCost = cost;
    }

    public override void CalculateDesire(EnemyMouse ai)
    {
        // Desire increases when health is low and money is sufficient
        DesireVal = (ai.GetMoney() >= unitCost && ai.enemyHealth < 50) ? 100f : 0f;
    }
}