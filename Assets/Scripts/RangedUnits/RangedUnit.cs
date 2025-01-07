using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RangedUnit : UnitBase
{
    // State Management
    private StateManager<RangedUnit> pState;

    public int clipSize = 10;           // Maximum bullets
    public int bullets = 10;            // Current bullets

    public bool isReloading = false;    // Is the unit reloading?
    public bool isShooting = false;
    public GameObject projectilePrefab; 

    void Start()
    {
        UnitManager.Instance.RegisterUnit(this);
        ApplyAttributes();
        ChangeState(new RangedWalking());

    }

    void Update()
    {
        // Execute the current state's behavior
        pState?.Execute(this);
    }

    public void ChangeState(StateManager<RangedUnit> newState)
    {
        Debug.Log($"Changing state from {pState?.GetType().Name} to {newState.GetType().Name}");
        pState?.Exit(this); // Exit the current state
        pState = newState;
        pState?.Enter(this); // Enter the new state
    }

    public IEnumerator Reload()
    {
        if (!isReloading)
        {
            isReloading = true;
            Debug.Log("Reloading started.");

            while (bullets < clipSize)
            {
                bullets++;
                Debug.Log($"Reloading... Bullets: {bullets}/{clipSize}");
                yield return new WaitForSeconds(speed);
            }

            isReloading = false;
            Debug.Log("Reloading complete.");
        }
        
    }

    public IEnumerator Fighting()
    {
        if (!isShooting)
        {
            isShooting = true; // Start shooting
            while (bullets > 0)
            {
                if (projectilePrefab == null)
                {
                    Debug.LogError("Projectile Prefab is not assigned.");
                }

                // Calculate the spawn position (1 unit in front of the shooter)
                Vector3 spawnPosition = transform.position + transform.forward;

                // Instantiate the projectile
                GameObject projectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation);

                // Assign the team to the projectile
                ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
                if (projectileScript != null)
                {
                    projectileScript.team = this.Team; // Assuming `this.team` is defined in `UnitBase`
                    projectileScript.damage = damage; 
                }
                else
                {
                    Debug.LogError("Projectile prefab is missing a ProjectileScript component.");
                }
                bullets--;
                Debug.Log("Firing! Bullets left: " + bullets);
                yield return new WaitForSeconds(fightSpeed);
            }
            isShooting = false; // Shooting finished
        }
    }

    public void DeleteComponent(Component component)
    {
        Destroy(component);
    }
}