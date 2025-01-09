using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnUnit : UnitBase
{
    // Our state manager for handling state transitions
    private StateManager<PawnUnit> currentState;
    StateManager<PawnUnit> pState;
    public GameObject hitbox;  // The hitbox object

    void Start()
    {
        UnitManager.Instance.RegisterUnit(this);
        ApplyAttributes();
        ChangeState(new Walking());
        // Log PawnUnit stats to debug initialization
        Debug.Log($"Pawn Unit Initialized: Health {health}, Damage {damage}, Team {Team}");
    }

    // This is called every frame by Unity
    void Update()
    {
        pState.Execute(this);  // Execute the current state logic
    }

    // Use this method to change states
    public void ChangeState(StateManager<PawnUnit> newState)
    {
        Debug.Log($"Changing state from {pState?.GetType().Name} to {newState.GetType().Name}");
        pState?.Exit(this); // Exit the current state
        pState = newState;
        pState?.Enter(this);
    }

    // Fighting coroutine to manage attack logic
    public IEnumerator Fighting()
    {
        Debug.Log("Attack");

        // Ensure the PawnUnit is still alive before continuing
        if (this == null || CurrentTarget == null)
        {
            yield break; // Exit coroutine if the object is destroyed
        }

        // Get the first (and only) hitbox
        if (hitbox != null) // Ensure the hitbox exists
        {
            hitbox.SetActive(true); // Activate the hitbox

            // Calculate direction to the current target
            Vector3 targetDirection = (CurrentTarget.transform.position - transform.position).normalized;

            // Position the hitbox much further in front of the character along the target direction
            // Increase this multiplier to move the hitbox further in front
            Vector3 hitboxPosition = transform.position + targetDirection * 6.0f; // Adjust the multiplier to set the distance in front

            // Set the position of the hitbox
            hitbox.transform.position = hitboxPosition;

            // Log the position for debugging
            Debug.Log($"Hitbox positioned at: {hitbox.transform.position}");

            // Rotate the hitbox to face the target
            hitbox.transform.LookAt(CurrentTarget.transform.position);

            // Optionally, if the hitbox needs a specific orientation (e.g., offset by 90 degrees)
            // hitbox.transform.Rotate(0, 90, 0); // Adjust the rotation if necessary
        }

        // Wait for the duration of the attack
        yield return new WaitForSeconds(1f);

        // After attack duration, deactivate the hitbox
        if (hitbox != null)
        {
            hitbox.SetActive(false); // Deactivate the hitbox
        }

        yield return new WaitForSeconds(fightSpeed); // Wait before the next attack
    }
}