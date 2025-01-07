using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSteering : MonoBehaviour
{
    /////////////////////
    // Updated Values
    /////////////////////
    public Vector3 Velocity; // The agent's current velocity

    // "Constant" values for tuning
    public float Mass = 1f;          // The agent's mass affects acceleration
    public float MaxSpeed = 5f;      // The maximum speed of the agent
    public float MaxForce = 100f;     // The maximum steering force the agent can apply
    public float MaxTurnRate = 100f; // Maximum turn rate (optional)

    // List of steering behaviors with weights
    private List<SteeringBehaviorEntry> behaviors = new List<SteeringBehaviorEntry>();

    // Add a steering behavior and its weight
    public void AddBehavior(SteeringBehaviourBase behavior, float weight)
    {
        behaviors.Add(new SteeringBehaviorEntry(behavior, weight));
    }

    // Remove a steering behavior
    public void RemoveBehavior(SteeringBehaviourBase behavior)
    {
        behaviors.RemoveAll(entry => entry.Behavior == behavior);
    }

    private void Start()
    {
        // Optionally initialize steering behaviors if you prefer setting them up in code
        foreach (var behavior in GetComponents<SteeringBehaviourBase>())
        {
            AddBehavior(behavior, 1f); // Default weight is 1
        }
    }

    private void Update()
    {
        Vector3 steeringForce = CalculateSteeringForce();

        // Calculate acceleration
        Vector3 acceleration = steeringForce / Mass;

        // Update velocity
        Velocity += acceleration * Time.deltaTime;

        // Clamp velocity to MaxSpeed
        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);

        // Update position and orientation
        if (Velocity != Vector3.zero)
        {
            transform.position += Velocity * Time.deltaTime;

            // Update forward direction
            transform.forward = Velocity.normalized;
        }
    }

    private Vector3 CalculateSteeringForce()
    {
        Vector3 totalForce = Vector3.zero;


        foreach (var entry in behaviors)
        {
            if (entry.Behavior == null || !entry.Behavior.enabled)
            {
                continue; // Skip destroyed or disabled behaviors
            }

            Vector3 force = entry.Behavior.Calculate() * entry.Weight;

            if (entry.Behavior is ObstacleAvoidance && force.magnitude > 0.1f)
            {
                // Prioritize avoidance and temporarily suppress other behaviors
                return Vector3.ClampMagnitude(force, MaxForce);
            }

            if (totalForce.magnitude + force.magnitude > MaxForce)
            {
                force = Vector3.ClampMagnitude(force, MaxForce - totalForce.magnitude);
            }

            totalForce += force;

            if (totalForce.magnitude >= MaxForce)
            {
                break;
            }
        }

        return totalForce;
    }

    // Debug visualizer for accumulated forces
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Velocity);

        Gizmos.color = Color.red;
        Vector3 steeringForce = CalculateSteeringForce();
        Gizmos.DrawLine(transform.position, transform.position + steeringForce);
    }

    // Inner class to hold a behavior and its weight
    private class SteeringBehaviorEntry
    {
        public SteeringBehaviourBase Behavior { get; }
        public float Weight { get; set; }

        public SteeringBehaviorEntry(SteeringBehaviourBase behavior, float weight)
        {
            Behavior = behavior;
            Weight = weight;
        }
    }

}
