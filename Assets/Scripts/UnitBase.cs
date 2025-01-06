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
    public UnitBase CurrentTarget;
    public int price; 

    private void Start()
    {
        UnitManager.Instance.RegisterUnit(this); // Register the unit
    }

    private void OnDestroy()
    {
        UnitManager.Instance.UnregisterUnit(this); // Unregister the unit
    }
}
