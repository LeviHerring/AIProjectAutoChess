using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class PawnUnit : MonoBehaviour
{
    //Our state
    private StateManager<PawnUnit> currentState;

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
        if (currentState != null)
        {
            currentState.Exit(this); // Call Exit on the current state
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(this); // Call Enter on the new state
        }
    }

    //public members
    //These values can be monitored and editted by our "states"
    public int health;
    public int damage;
    public float speed;
    public float fightSpeed; 


    public void Move()
    {
        //stuff

    }

    public IEnumerator Fighting()
    {
        Debug.Log("Attack"); 
        yield return new WaitForSeconds(fightSpeed); 
    }    
}
