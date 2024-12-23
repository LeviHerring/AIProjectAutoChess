using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

public class RangedUnit : MonoBehaviour
{
    //Our state


    StateManager<RangedUnit> pState;



    void Start()
    {
        pState = new RangedFighting();
        health = 100;
    }

    //This is called by our console application periodically
    void Update()
    {
        pState.Execute(this);
    }


    //Use this method to change states, so the old state is correctly disposed of
    public void ChangeState(StateManager<RangedUnit> newState)
    {
        pState = newState;
    }

    //public members
    //These values can be monitored and editted by our "states"
    public int health;
    public int damage;
    public float speed;
    public float fightSpeed;
    public int clipSize;
    public int bullets; 
    //public GameObject bullet; 

    public void Move()
    {
        UnityEngine.Debug.Log("Moving forward/to enemy");
        transform.position = transform.position + new Vector3(1, 0, 0);
        health++;
    }

    public IEnumerator Fighting()
    {
        UnityEngine.Debug.Log("Attack");
        bullets--; 
        UnityEngine.Debug.Log("Clip size is " + clipSize.ToString());
        yield return new WaitForSeconds(fightSpeed);
    }

    public IEnumerator Reload()
    {
        bullets++;
        UnityEngine.Debug.Log("Clip size is " + clipSize.ToString());
        yield return new WaitForSeconds(speed);
    }
}
