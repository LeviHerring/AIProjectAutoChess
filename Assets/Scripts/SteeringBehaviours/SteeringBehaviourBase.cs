using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class. Steering Behaviours will inherit from this (see Wander/Seek)
/// </summary>
[RequireComponent(typeof(PawnSteering))]
public abstract class SteeringBehaviourBase : MonoBehaviour
{
    //Needs to be overidden in child classes
    public abstract Vector3 Calculate();

}
