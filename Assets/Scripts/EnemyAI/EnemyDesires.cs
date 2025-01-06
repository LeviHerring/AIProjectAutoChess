using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemyDesires
{
    public string State { get; private set; } // Description of the desire
    public float DesireVal { get; protected set; } // Utility value of the desire

    protected EnemyDesires(string state)
    {
        State = state;
        DesireVal = 0; // Default value
    }

    public abstract void CalculateDesire(EnemyMouse ai); // Calculate utility value
}