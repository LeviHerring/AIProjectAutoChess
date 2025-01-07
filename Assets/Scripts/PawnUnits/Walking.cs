using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : StateManager<PawnUnit>
{
    private Wander wander;
    private ObstacleAvoidance obstacle;

    public override void Enter(PawnUnit parentClass)
    {
        Debug.Log("Entering Walking state.");

        // Ensure Wander behavior is added if not already
        wander = parentClass.GetComponent<Wander>();
        if (wander == null)
        {
            wander = parentClass.gameObject.AddComponent<Wander>();
        }

        // Ensure ObstacleAvoidance behavior is added if not already
        obstacle = parentClass.GetComponent<ObstacleAvoidance>();
        if (obstacle == null)
        {
            obstacle = parentClass.gameObject.AddComponent<ObstacleAvoidance>();
        }

        // Initialize vehicle reference in Wander and ObstacleAvoidance
        if (wander.vehicle == null)
        {
            wander.vehicle = parentClass.GetComponent<PawnSteering>();
            if (wander.vehicle == null)
            {
                Debug.LogError("PawnSteering component not found! Ensure it is attached to the GameObject.");
                return;
            }
        }

        if (obstacle.vehicle == null)
        {
            obstacle.vehicle = parentClass.GetComponent<PawnSteering>();
            if (obstacle.vehicle == null)
            {
                Debug.LogError("PawnSteering component not found! Ensure it is attached to the GameObject.");
                return;
            }
        }

        // Set speed for Wander behavior
        wander.vehicle.MaxSpeed = parentClass.speed;

        // Add Wander and ObstacleAvoidance behaviors to the vehicle
        wander.vehicle.AddBehavior(wander, 1f);
        wander.vehicle.AddBehavior(obstacle, 0.5f);  // Add obstacle avoidance with lower priority
    }

    public override void Execute(PawnUnit parentClass)
    {
        Debug.Log("Executing Walking state.");

        // Check if there are any enemies
        List<UnitBase> enemies = UnitManager.Instance.GetEnemies(parentClass.Team);
        if (enemies.Count > 0)
        {
            UnitBase closestEnemy = enemies[0];
            float closestDistance = Vector3.Distance(parentClass.transform.position, closestEnemy.transform.position);

            foreach (var enemy in enemies)
            {
                float distance = Vector3.Distance(parentClass.transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }
            }

            // Set the closest enemy as the target and change state to Patrolling
            parentClass.CurrentTarget = closestEnemy;
            parentClass.ChangeState(new Patrolling());  // Transition to Patrolling state to start seeking the target
        }

        // If health is <= 0, change state to PawnDeath
        if (parentClass.health <= 0)
        {
            parentClass.ChangeState(new PawnDeath());
        }
    }

    public override void Exit(PawnUnit parentClass)
    {
        Debug.Log("Exiting Walking state.");

        // Clean up and remove Wander and ObstacleAvoidance behaviors
        if (wander != null)
        {
            wander.vehicle.RemoveBehavior(wander);
            Object.Destroy(wander);  // Optionally destroy the Wander component if not needed anymore
        }

        if (obstacle != null)
        {
            obstacle.vehicle.RemoveBehavior(obstacle);
            Object.Destroy(obstacle);  // Optionally destroy the ObstacleAvoidance component if not needed anymore
        }
    }
}