using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyEconomicState : EnemyStateManager<EnemyMouse>
{
    private float spawnCooldown = 5f; // Cooldown in seconds
    private float lastSpawnTime = -5f; // Ensure the first spawn happens immediately

    public override void Enter(EnemyMouse ai)
    {
        Debug.Log("Entering Economic State");
    }

    public override void Execute(EnemyMouse ai)
    {
        var desires = GetDesires(ai);

        if (desires.Count > 0)
        {
            // Weighted random selection
            float totalWeight = desires.Sum(d => d.DesireVal);
            float randomValue = Random.Range(0, totalWeight);

            float cumulative = 0;
            foreach (var desire in desires)
            {
                cumulative += desire.DesireVal;
                if (randomValue <= cumulative)
                {
                    Debug.Log($"Selected desire: {desire.State}");
                    if (desire is SpawnBasicUnitDesire)
                    {
                        ai.SpawnUnit(EnemyMouse.UnitType.BasicPawn);
                    }
                    else if (desire is SpawnMediumUnitDesire)
                    {
                        ai.SpawnUnit(EnemyMouse.UnitType.MediumPawn);
                    }
                    else if (desire is SpawnRareUnitDesire)
                    {
                        ai.SpawnUnit(EnemyMouse.UnitType.RarePawn);
                    }
                    break;
                }
            }
        }
    }

    public override void Exit(EnemyMouse ai)
    {
        Debug.Log("Exiting Economic State");
    }

    public override List<EnemyDesires> GetDesires(EnemyMouse ai)
    {
        List<EnemyDesires> desires = new List<EnemyDesires>
    {
        new SpawnBasicUnitDesire(15),   // Assume cost 50
        new SpawnMediumUnitDesire(50), // Assume cost 150
        new SpawnRareUnitDesire(100)   // Assume cost 300
    };

        foreach (var desire in desires)
        {
            desire.CalculateDesire(ai); // Calculate the desire values
        }

        // Sort desires by value, descending (highest desire first)
        desires.Sort((a, b) => b.DesireVal.CompareTo(a.DesireVal));

        return desires;
    }


    private void PerformUtilityAction(EnemyMouse ai)
    {
        List<EnemyDesires> desires = GetDesires(ai);
        desires.Sort((d1, d2) => d2.DesireVal.CompareTo(d1.DesireVal)); // Sort by utility

        EnemyDesires bestDesire = desires[0]; // Get highest desire value
        Debug.Log($"Performing action: {bestDesire.State}");

        // Check if enough time has passed to spawn a unit
        if (Time.time - lastSpawnTime > spawnCooldown)
        {
            if (bestDesire is SpawnBasicUnitDesire)
            {
                EnemyMouse.UnitType unitToSpawn = (bestDesire as SpawnBasicUnitDesire).GetUnitType();
                ai.SpawnUnit(unitToSpawn); // Spawn unit
                lastSpawnTime = Time.time; // Update spawn time
            }
        }
    }
}