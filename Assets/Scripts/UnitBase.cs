using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public PlayerTypes.Team Team;
    public bool IsAlive = true;
    public int health;
    public int damage;
    public float speed;
    public float fightSpeed;
    public int priceOnDeath;
    public UnitBase CurrentTarget; 

    public UnitAttributes Attributes; // Reference to UnitAttributes

    private void Start()
    {
        UnitManager.Instance.RegisterUnit(this); // Register the unit
        ApplyAttributes(); // Apply the attributes
    }

    public void ApplyAttributes()
    {
        if (Attributes == null)
        {
            Debug.LogError($"No attributes assigned to unit {gameObject.name}");
            return;
        }

        // Use attributes to initialize behavior-related properties
        IsAlive = true;
        health = Attributes.Health;
        damage = Attributes.Damage;
        speed = Attributes.Speed;
        fightSpeed = Attributes.FightSpeed;
        priceOnDeath = Attributes.Price; 
    }

    private void OnDestroy()
    {
        UnitManager.Instance.UnregisterUnit(this); // Unregister the unit
    }
}
