using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviourBase
{
    public float WanderRadius = 10f;    // Radius of the wander circle
    public float WanderDistance = 10f; // Distance of the wander circle in front of the agent
    public float WanderJitter = 50f;   // Amount of random jitter applied per second
    public float Weight = 1.0f;        // Lower weight compared to Seek

    private Vector3 WanderTarget = Vector3.zero;
    public PawnSteering vehicle;

    void Start()
    {
        // Initialize wander target
        if(vehicle == null)
        {
            vehicle = GetComponent<PawnSteering>(); 
        }
        float initialAngle = Random.Range(0.0f, Mathf.PI * 2);
        WanderTarget = new Vector3(Mathf.Cos(initialAngle), 0, Mathf.Sin(initialAngle)) * WanderRadius;
    }

    public override Vector3 Calculate()
    {
        float jitterThisFrame = WanderJitter * Time.deltaTime;
        float jitterX = Random.Range(-1f, 1f) * jitterThisFrame;
        float jitterZ = Random.Range(-1f, 1f) * jitterThisFrame;

        // Apply jitter and constrain to circle
        WanderTarget += new Vector3(jitterX, 0, jitterZ);
        WanderTarget = WanderTarget.normalized * WanderRadius;

        // Move the circle forward and calculate target position
        Vector3 targetLocal = WanderTarget + transform.forward * WanderDistance;
        Vector3 targetWorld = transform.position + targetLocal;

        // Calculate steering force
        Vector3 steeringForce = targetWorld - transform.position;
        return Vector3.ClampMagnitude(steeringForce, GetComponent<PawnSteering>().MaxForce);
    }
}