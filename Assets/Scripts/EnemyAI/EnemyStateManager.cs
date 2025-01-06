using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateManager<T>
{
    public abstract void Enter(T ai);
    public abstract void Execute(T ai);
    public abstract void Exit(T ai);

    // Add this method for utility-driven behavior
    public virtual List<EnemyDesires> GetDesires(T ai)
    {
        return new List<EnemyDesires>(); // Default empty list
    }
}
