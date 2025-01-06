using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouse : MonoBehaviour
{
    public static EnemyMouse instance;

    private EnemyStateManager<EnemyMouse> currentState;

    public int enemyHealth = 100;
    private int money = 200;

    // Dictionary to hold the prefabs for different unit types
    public Dictionary<UnitType, GameObject> unitPrefabs = new Dictionary<UnitType, GameObject>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize the unitPrefabs dictionary with your unit prefabs
        unitPrefabs[UnitType.BasicRanged] = Resources.Load<GameObject>("Prefabs/BasicRanged");
        unitPrefabs[UnitType.BasicPawn] = Resources.Load<GameObject>("Prefabs/BasicPawn");
        unitPrefabs[UnitType.MediumRanged] = Resources.Load<GameObject>("Prefabs/MediumRanged");
        unitPrefabs[UnitType.MediumPawn] = Resources.Load<GameObject>("Prefabs/MediumPawn");
        unitPrefabs[UnitType.RareRanged] = Resources.Load<GameObject>("Prefabs/RareRanged");
        unitPrefabs[UnitType.RarePawn] = Resources.Load<GameObject>("Prefabs/RarePawn");

        // Start in economic state
        ChangeState(new EnemyEconomicState());
    }

    void Update()
    {
        // Dynamic state transitions based on AI health
        if (enemyHealth < 30 && !(currentState is EnemyDefensiveState))
        {
            ChangeState(new EnemyDefensiveState());
        }
        else if (enemyHealth >= 30 && !(currentState is EnemyEconomicState))
        {
            ChangeState(new EnemyEconomicState());
        }

        // Execute the current state's logic
        currentState?.Execute(this);
    }

    // State transition logic
    public void ChangeState(EnemyStateManager<EnemyMouse> newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    // Retrieve AI's current money
    public int GetMoney()
    {
        return money;
    }

    // Add money to the AI
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log($"AI money increased to: {money}");
    }

    // Subtract money from the AI
    public void SubtractMoney(int amount)
    {
        money -= amount;
        Debug.Log($"AI money decreased to: {money}");
    }

    // Spawn a unit based on UnitType
    public void SpawnUnit(UnitType unitType)
    {
        if (unitPrefabs.ContainsKey(unitType))
        {
            GameObject unitPrefab = unitPrefabs[unitType];
            Instantiate(unitPrefab, transform.position, Quaternion.identity); // Spawn unit at AI's position
            Debug.Log($"Spawning unit: {unitType}");
        }
        else
        {
            Debug.LogWarning($"No prefab found for unit type: {unitType}");
        }
    }
    public enum UnitType
    {
        BasicRanged,
        BasicPawn,
        MediumRanged,
        MediumPawn,
        RareRanged,
        RarePawn
        // Add more as needed
    }
}