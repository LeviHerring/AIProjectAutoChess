using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSteering : MonoBehaviour
{

    /////////////////////
    //Updated Values
    /////////////////////
    /// <summary>
    /// This is applied to the current position every frame
    /// </summary>
    public Vector3 Velocity;

    //Position, Heading and Side can be accessed from the transform component with transform.position, transform.forward and transform.right respectively

    //"Constant" values, they are public so we can adjust them through the editor

    //Represents the weight of an object, will effect its acceleration
    public float Mass = 1;

    //The maximum speed this agent can move per second
    public float MaxSpeed = 1;

    //The thrust this agent can produce
    public float MaxForce = 1;

    //We use this to determine how fast the agent can turn, but just ignore it for, we won't be using it
    public float MaxTurnRate = 1.0f;


    // Update is called once per frame
    void Update()
    {
        Vector3 SteeringForce = Vector3.zero;

        //Get all steering behaviours attached to this object and add their calculated steering force onto this SteeringForce
        //YOU NEED TO DO THAT
        SteeringBehaviourBase[] children = GetComponents<SteeringBehaviourBase>();
        foreach (SteeringBehaviourBase child in children)
        {
            SteeringForce += child.Calculate();
        }

        Vector3 Acceleration = SteeringForce / Mass;

        Velocity += Acceleration * Time.deltaTime;

        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);

        if (Velocity != Vector3.zero)
        {
            transform.position += Velocity * Time.deltaTime;

            transform.forward = Velocity.normalized;
        }

        //transform.right should update on its own once we update the transform.forward
    }
}
