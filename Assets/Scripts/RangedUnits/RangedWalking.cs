using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWalking : StateManager<RangedUnit>
{
    private Seek seekBehavior;

    public override void Enter(RangedUnit parentClass)
    {
        Debug.Log("Entering Walking state.");

        // Get or add the Seek component
        seekBehavior = parentClass.GetComponent<Seek>();
        if (seekBehavior == null)
        {
            seekBehavior = parentClass.gameObject.AddComponent<Seek>();
        }

        // Initialize vehicle reference in Seek
        if (seekBehavior.vehicle == null)
        {
            seekBehavior.vehicle = parentClass.GetComponent<PawnSteering>();
            if (seekBehavior.vehicle == null)
            {
                Debug.LogError("PawnSteering component not found! Ensure it is attached to the GameObject.");
                return;
            }
        }

        // Set speed and target position
        seekBehavior.vehicle.MaxSpeed = parentClass.speed;
        seekBehavior.SetTarget(parentClass.transform.position + new Vector3(10, 0, 0)); // Example: 10 units forward
    }

    public override void Execute(RangedUnit parentClass)
    {
        Debug.Log("Walking...");

        // Find the closest enemy
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
            parentClass.CurrentTarget = closestEnemy; 

            // Seek to the closest enemy's position
            if (seekBehavior != null)
            {
                seekBehavior.SetTarget(new Vector3(closestEnemy.transform.position.x, parentClass.transform.position.y, parentClass.transform.position.z));
            }
        }

        // If the seek target has been reached, transition to fighting state
        if (seekBehavior != null && seekBehavior.HasReachedTarget)
        {
            Debug.Log("Reached the target.");
            parentClass.ChangeState(new RangedFighting());
        }

        if (parentClass.health <= 0)
        {
            parentClass.ChangeState(new RangedDeath());
        }
    }

    public override void Exit(RangedUnit parentClass)
    {
        if (seekBehavior != null)
        {
            seekBehavior.vehicle.MaxSpeed = 0;
            Object.Destroy(seekBehavior); // Remove Seek component if no longer needed

        }
        Debug.Log("Exiting Walking state.");
    }
}