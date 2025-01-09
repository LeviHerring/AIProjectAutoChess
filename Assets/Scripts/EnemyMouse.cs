using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class EnemyMouse : MonoBehaviour
{
    public static EnemyMouse instance;

    private EnemyStateManager<EnemyMouse> currentState;

    public int enemyHealth = 100;
    public int money = 200;
    public float timer;
    public float gameTime; 
    public Transform topRight;
    public Transform bottomLeft;
    public Transform rangedLeft;

    public TextMeshProUGUI health; 
    public TextMeshProUGUI moneyText; 
    
    

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

        timer = 0;
        gameTime = 0; 
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        gameTime += Time.deltaTime; 
        // Dynamic state transitions based on AI health
        if (enemyHealth < 30 && !(currentState is EnemyDefensiveState))
        {
            ChangeState(new EnemyDefensiveState());
        }
        else if (enemyHealth >= 30 && !(currentState is EnemyEconomicState))
        {
            ChangeState(new EnemyEconomicState());
        }


        if (timer >= 5)
        {
            AddMoney(20);
            timer = 0;
            StartCoroutine(CheckUnitCount());
        }

        // Execute the current state's logic
        currentState?.Execute(this);

        health.text = enemyHealth.ToString();
        moneyText.text = money.ToString(); 
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
        float xpos;
        float zpos; 
        if (unitPrefabs.ContainsKey(unitType))
        {
            GameObject unitPrefab = unitPrefabs[unitType];

            if (unitPrefab != null)
            {
                if(money >= unitPrefab.GetComponent<UnitBase>().Attributes.Price)
                {
                    if(unitPrefab.GetComponent<UnitBase>().Attributes.UnitType == EnemyMouse.UnitType.BasicRanged || unitPrefab.GetComponent<UnitBase>().Attributes.UnitType == EnemyMouse.UnitType.MediumRanged || unitPrefab.GetComponent<UnitBase>().Attributes.UnitType == EnemyMouse.UnitType.RareRanged)
                    {
                        xpos = Random.Range(bottomLeft.position.x, topRight.position.x);
                        zpos = Random.Range(rangedLeft.position.z, topRight.position.z);

                        Instantiate(unitPrefab, new Vector3(xpos, 2.0f, zpos), Quaternion.identity); // Spawn unit at AI's position
                    }
                    else if(unitPrefab.GetComponent<UnitBase>().Attributes.UnitType == EnemyMouse.UnitType.BasicPawn || unitPrefab.GetComponent<UnitBase>().Attributes.UnitType == EnemyMouse.UnitType.MediumPawn || unitPrefab.GetComponent<UnitBase>().Attributes.UnitType == EnemyMouse.UnitType.RarePawn)
                    {
                        xpos = Random.Range(bottomLeft.position.x, topRight.position.x);
                        zpos = Random.Range(bottomLeft.position.z, topRight.position.z);
                        Instantiate(unitPrefab, new Vector3(xpos, 2.0f, zpos), Quaternion.identity);
                    }
                   
                    unitPrefab.GetComponent<UnitBase>().Team = PlayerTypes.Team.Enemy; 
                    money -= unitPrefab.GetComponent<UnitBase>().Attributes.Price;
                    Debug.Log($"Spawning unit: {unitType}");
                }
               else
                {
                    return; 
                }
            }
            else
            {
                Debug.LogError($"Prefab for {unitType} is null. Check the Resource path or prefab assignment.");
            }
        }
        else
        {
            Debug.LogWarning($"No prefab found for unit type: {unitType}");
        }
    }


    private IEnumerator CheckUnitCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // Check every 10 seconds

            // If no units of the AI's team are found
            if (!UnitManager.Instance.HasUnitsOfTeam(PlayerTypes.Team.Enemy) && gameTime > 100)
            {
                enemyHealth -= 10; // Deduct 10 health
                Debug.Log($"AI has no units! Health reduced to {enemyHealth}");

                // Check if the AI is out of health
                if (enemyHealth <= 0)
                {
                    Debug.Log("AI has lost the game!");
                    // Implement your game-over logic here
                }
            }
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