using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    PawnUnit pawn;
    PawnUnit enemy;
    PlayerTypes.Team team;
    int damage;

    void Start()
    {
        pawn = GetComponentInParent<PawnUnit>();
        team = pawn.Team;
        damage = pawn.damage;
    }

    void Update()
    {
        // Optionally, update logic for the hitbox (if needed)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<UnitBase>())
        {
            if (other.gameObject.GetComponent<UnitBase>().Team == team)
            {
                Debug.Log("Hit Same Team");
            }
            else
            {
                Debug.Log("Hit Opposite Team");
                other.gameObject.GetComponent<UnitBase>().health -= damage;
            }
        }
    }
}