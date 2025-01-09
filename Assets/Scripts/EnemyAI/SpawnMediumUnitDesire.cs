using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMediumUnitDesire : EnemyDesires
{
    private int cost; // The base cost of the basic unit
    private EnemyMouse.UnitType unitType; // The type of unit to spawn (basic, medium, rare)

    public SpawnMediumUnitDesire(int unitCost) : base("Spawn Basic Unit") // Pass the state to the base constructor
    {
        cost = unitCost;
        unitType = EnemyMouse.UnitType.MediumPawn; // Default unit type (basic pawn)
    }

    public override void CalculateDesire(EnemyMouse ai)
    {
        if (ai.GetMoney() >= 150) // Assume medium unit costs 150
        {
            DesireVal = 0.7f; // Base desire
            if (ai.enemyHealth < 50) // Favor medium units when health is moderate
            {
                DesireVal += 0.2f;
            }
        }
        else
        {
            DesireVal = 0.2f; // Low desire if not enough money
        }

        // Add slight randomness
        DesireVal += Random.Range(-0.05f, 0.05f);
    }

    public EnemyMouse.UnitType GetUnitType()
    {
        return unitType;
    }
}
