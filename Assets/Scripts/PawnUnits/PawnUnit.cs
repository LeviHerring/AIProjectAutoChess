using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class PawnUnit : UnitBase
{
    //Our state
    private StateManager<PawnUnit> currentState;

    StateManager<PawnUnit> pState;
    public GameObject hitbox;



    void Start()
    {
        UnitManager.Instance.RegisterUnit(this);
        ApplyAttributes();
        ChangeState(new Walking());
        //health = 0;
    }

    //This is called by our console application periodically
    void Update()
    {
        pState.Execute(this);

    }


    //Use this method to change states, so the old state is correctly disposed of
    public void ChangeState(StateManager<PawnUnit> newState)
    {
        Debug.Log($"Changing state from {pState?.GetType().Name} to {newState.GetType().Name}");
        pState?.Exit(this); // Exit the current state
        pState = newState;
        pState?.Enter(this);
    }

    //public members
    //These values can be monitored and editted by our "states"

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
            hitbox.SetActive(true);

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

        yield return new WaitForSeconds(1f);

        // After attack duration, deactivate the hitbox
        if (hitbox != null)
        {
            hitbox.SetActive(false);
        }

        yield return new WaitForSeconds(fightSpeed);
    }
}
