using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    PawnUnit pawn;
    PawnUnit enemy;
    PlayerTypes.Team team;
    int damage; 
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponentInParent<PawnUnit>(); 
        team = pawn.Team;
        damage = pawn.damage; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<UnitBase>())
        {
            if (other.gameObject.GetComponent<UnitBase>().Team == team)
            {
                Debug.Log("Hit Same");
            }
            else
            {
                Debug.Log("Hit opposite");
                other.gameObject.GetComponent<UnitBase>().health -= damage;
            }
        }
    }
}
