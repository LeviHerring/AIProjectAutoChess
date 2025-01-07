using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitAttributes
{
    public int Health;
    public int Damage;
    public float Speed;
    public float FightSpeed;
    public int Price;
    public EnemyMouse.UnitType UnitType;

    public UnitAttributes(int health, int damage, float speed, float fightSpeed, int price, EnemyMouse.UnitType unitType)
    {
        Health = health;
        Damage = damage;
        Speed = speed;
        FightSpeed = fightSpeed;
        Price = price;
        UnitType = unitType;
    }
}
