using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBasicUnitDesire : EnemyDesires
{
    private int cost; // The base cost of the basic unit
    private EnemyMouse.UnitType unitType; // The type of unit to spawn (basic, medium, rare)

    public SpawnBasicUnitDesire(int unitCost) : base("Spawn Basic Unit") // Pass the state to the base constructor
    {
        cost = unitCost;
        unitType = EnemyMouse.UnitType.BasicPawn; // Default unit type (basic pawn)
    }

    public override void CalculateDesire(EnemyMouse ai)
    {
        if (ai.GetMoney() >= 50) // Assume basic unit costs 50
        {
            DesireVal = 0.5f; // Base desire
            if (ai.enemyHealth > 80) // Favor basic units when AI is healthy
            {
                DesireVal += 0.2f;
            }
        }
        else
        {
            DesireVal = 0.1f; // Low desire if not enough money
        }

        // Add slight randomness
        DesireVal += Random.Range(-0.05f, 0.05f);
    }

    public EnemyMouse.UnitType GetUnitType()
    {
        return unitType;
    }
}