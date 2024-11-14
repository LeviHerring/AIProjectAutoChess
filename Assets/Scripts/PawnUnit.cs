using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnUnit : MonoBehaviour
{
    //Our state
    

    StateManager<PawnUnit> pState;
    
   

    void Start()
    {
        pState = new Patrolling();
        health = 0;
    }

    //This is called by our console application periodically
    void Update()
    {
        pState.Execute(this);
    }


    //Use this method to change states, so the old state is correctly disposed of
    public void ChangeState(StateManager<PawnUnit> newState)
    {
        pState = newState;
    }

    //public members
    //These values can be monitored and editted by our "states"
    public int health;
    public int damage;
    public float speed; 


    public void Move()
    {
        Debug.Log("Moving forward/to enemy");
        transform.position = transform.position + new Vector3(1, 0, 0 );
        health++; 
    }
}
