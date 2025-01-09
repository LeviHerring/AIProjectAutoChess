using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRareUnitDesire : EnemyDesires
{
    private int cost; // The base cost of the basic unit
    private EnemyMouse.UnitType unitType; // The type of unit to spawn (basic, medium, rare)

    public SpawnRareUnitDesire(int unitCost) : base("Spawn Basic Unit") // Pass the state to the base constructor
    {
        cost = unitCost;
        unitType = EnemyMouse.UnitType.MediumPawn; // Default unit type (basic pawn)
    }

    public override void CalculateDesire(EnemyMouse ai)
    {
        if (ai.GetMoney() >= 300) // Assume rare unit costs 300
        {
            DesireVal = 0.9f; // Base desire
            if (ai.enemyHealth < 30) // Favor rare units when AI is in danger
            {
                DesireVal += 0.3f;
            }
        }
        else
        {
            DesireVal = 0.3f; // Low desire if not enough money
        }

        // Add slight randomness
        DesireVal += Random.Range(-0.05f, 0.05f);
    }


    public EnemyMouse.UnitType GetUnitType()
    {
        return unitType;
    }
}
