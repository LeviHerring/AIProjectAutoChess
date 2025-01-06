using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMoneyDesire : EnemyDesires
{
    public SaveMoneyDesire() : base("Save Money") { }

    public override void CalculateDesire(EnemyMouse ai)
    {
        // Higher desire to save money when AI health is low
        DesireVal = Mathf.Clamp(100f - ai.enemyHealth, 0f, 100f); // Example: scale with health
    }
}
