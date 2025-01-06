using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviourBase
{
    public float WanderRadius = 10f;    // Radius of the wander circle
    public float WanderDistance = 10f; // Distance of the wander circle in front of the agent
    public float WanderJitter = 1f;    // Amount of random jitter applied per second

    private Vector3 WanderTarget = Vector3.zero; // The current target on the wander circle
    private PawnSteering pawnSteering;

    void Start()
    {
        pawnSteering = GetComponent<PawnSteering>();

        if (pawnSteering == null)
        {
            Debug.LogError("PawnSteering component not found on this GameObject!");
        }
        // Initialize the wander target on the circle
        float initialAngle = Random.Range(0.0f, Mathf.PI * 2);
        WanderTarget = new Vector3(Mathf.Cos(initialAngle), 0, Mathf.Sin(initialAngle)) * WanderRadius;
    }

    public override Vector3 Calculate()
    {
        // Apply jitter to the angle
        float jitterThisFrame = WanderJitter * Time.deltaTime;
        float jitterX = Random.Range(-1f, 1f) * jitterThisFrame;
        float jitterZ = Random.Range(-1f, 1f) * jitterThisFrame;

        // Add jitter to the wander target and constrain it to the circle
        WanderTarget += new Vector3(jitterX, 0, jitterZ);
        WanderTarget = WanderTarget.normalized * WanderRadius;

        // Move the circle to be in front of the agent
        Vector3 targetLocal = WanderTarget + transform.forward * WanderDistance;

        // Convert the local target to world space
        Vector3 targetWorld = transform.position + targetLocal;

        // Calculate the steering force
        Vector3 steeringForce = targetWorld - transform.position;

        // Clamp the steering force to MaxForce
        return Vector3.ClampMagnitude(steeringForce, pawnSteering.MaxForce);
    }
}
