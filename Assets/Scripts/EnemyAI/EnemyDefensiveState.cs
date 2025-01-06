using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefensiveState : EnemyStateManager<EnemyMouse>
{
    public override void Enter(EnemyMouse ai)
    {
        Debug.Log("Entering Defensive State");
    }

    public override void Execute(EnemyMouse ai)
    {
        Debug.Log("Focusing on defensive decisions.");
        PerformUtilityAction(ai);
    }

    public override void Exit(EnemyMouse ai)
    {
        Debug.Log("Exiting Defensive State");
    }

    public override List<EnemyDesires> GetDesires(EnemyMouse ai)
    {
        List<EnemyDesires> desires = new List<EnemyDesires>
        {
            new SpawnDefensiveUnitDesire(70), // Example cost for defensive unit
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

        // Execute the action based on the best desire
        if (bestDesire is SpawnDefensiveUnitDesire)
        {
            // You can now spawn different unit types dynamically
            ai.SpawnUnit(EnemyMouse.UnitType.MediumRanged); // Example unit
        }
    }
}
