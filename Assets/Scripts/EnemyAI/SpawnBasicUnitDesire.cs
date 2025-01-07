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
        // Example conditions for determining unit type
        if (ai.GetMoney() >= 150)
        {
            // If the AI has enough money, consider spawning a "medium" unit
            unitType = EnemyMouse.UnitType.MediumPawn;
            DesireVal = 0.75f; // Increase desire value for medium unit
        }
        else if (ai.GetMoney() >= 200)
        {
            // If the AI has even more money, consider spawning a "rare" unit
            unitType = EnemyMouse.UnitType.RarePawn;
            DesireVal = 1.0f; // Max desire value for rare unit
        }
        else
        {
            // Default to basic unit if money is lower
            unitType = EnemyMouse.UnitType.BasicPawn;
            DesireVal = 0.5f; // Lower desire for basic unit
        }

        // Adjust the desire value based on other conditions (optional)
        if (ai.enemyHealth < 30)
        {
            DesireVal += 0.2f; // More desire for spawning units if health is low
        }
    }

    public EnemyMouse.UnitType GetUnitType()
    {
        return unitType;
    }
}