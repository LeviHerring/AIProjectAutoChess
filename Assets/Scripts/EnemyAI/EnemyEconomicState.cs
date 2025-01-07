using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEconomicState : EnemyStateManager<EnemyMouse>
{
    public override void Enter(EnemyMouse ai)
    {
        Debug.Log("Entering Economic State");
    }

    public override void Execute(EnemyMouse ai)
    {
        Debug.Log("Focusing on economic decisions.");
        PerformUtilityAction(ai);
    }

    public override void Exit(EnemyMouse ai)
    {
        Debug.Log("Exiting Economic State");
    }

    public override List<EnemyDesires> GetDesires(EnemyMouse ai)
    {
        List<EnemyDesires> desires = new List<EnemyDesires>
        {
            new SpawnBasicUnitDesire(50), // Example cost for basic unit
            new SaveMoneyDesire()
        };

        foreach (EnemyDesires desire in desires)
        {
            desire.CalculateDesire(ai); // Calculate desire value
        }

        return desires;
    }

    private void PerformUtilityAction(EnemyMouse ai)
    {
        List<EnemyDesires> desires = GetDesires(ai);
        desires.Sort((d1, d2) => d2.DesireVal.CompareTo(d1.DesireVal)); // Sort by utility

        EnemyDesires bestDesire = desires[0]; // Get highest desire value
        Debug.Log($"Performing action: {bestDesire.State}");

        if (bestDesire is SpawnBasicUnitDesire)
        {
            // Spawn the determined unit type dynamically
            EnemyMouse.UnitType unitToSpawn = (bestDesire as SpawnBasicUnitDesire).GetUnitType();
            ai.SpawnUnit(unitToSpawn); // Use the unitType returned from the desire
        }
    }
}