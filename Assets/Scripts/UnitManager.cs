using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance; // Singleton instance

    // List of all units (both RangedUnit and PawnUnit, or any UnitBase-derived class)
    public List<UnitBase> Units = new List<UnitBase>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances of the manager
        }
    }

    // Register a unit to the list
    public void RegisterUnit(UnitBase unit)
    {
        if (!Units.Contains(unit))
        {
            Units.Add(unit);
        }
    }

    // Unregister a unit from the list (e.g., when destroyed or removed from the game)
    public void UnregisterUnit(UnitBase unit)
    {
        if (Units.Contains(unit))
        {
            Units.Remove(unit);
        }
    }

    // Find enemies based on the team's alignment
    public List<UnitBase> GetEnemies(PlayerTypes.Team team)
    {
        List<UnitBase> enemies = new List<UnitBase>();
        foreach (UnitBase unit in Units)
        {
            if (unit.Team != team)
            {
                enemies.Add(unit);
            }
        }
        return enemies;
    }
}