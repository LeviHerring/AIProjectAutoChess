using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    public static Mouse Instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] LayerMask layer;
    public int playerHealth; 

    public GameObject prefab;
    public int Money;
    float timer = 0;
    public int cost;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances of the manager
        }
    }

    private void Start()
    {
        timer = 0; 
    }

    private void Update()
    {
        // Ensure the application is focused
        if (!Application.isFocused)
        {
            return;
        }

        // Validate mouse position
        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 ||
            Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        timer += Time.deltaTime; 
       Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
       if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layer))
        {
            transform.position = raycastHit.point; 
        }

       if(timer >= 5)
        {
            AddMoney(20);
            timer = 0; 
        }

        MouseInput(); 
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (prefab == null)
            {
                return;
            }

            // Perform a raycast to determine the world position for spawning
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layer))
            {
                if (Money >= cost) // Ensure enough money
                {
                    // Instantiate the prefab at the hit point, slightly above the ground
                    Instantiate(prefab, raycastHit.point + Vector3.up * 2.5f, Quaternion.identity);

                    // Set team information on the instantiated prefab
                    UnitBase prefabBase = prefab.GetComponent<UnitBase>();
                    if (prefabBase != null)
                    {
                        prefabBase.Team = PlayerTypes.Team.Player;
                    }

                    // Deduct the cost
                    SubMoney(cost);
                }
            }
        }
    }

    public void AddMoney(int newMoney)
    {
        Money += newMoney; 
    }

    public void SubMoney(int newMoney)
    {
        Money -= newMoney; 
    }
}
