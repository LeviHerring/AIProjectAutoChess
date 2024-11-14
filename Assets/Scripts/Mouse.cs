using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] LayerMask layer;

    public GameObject prefab; 

    private void Update()
    {
       Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
       if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layer))
        {
            transform.position = raycastHit.point; 
        }

        MouseInput(); 
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefab, new Vector3(Input.mousePosition.x, 2.5f, Input.mousePosition.z), Quaternion.identity); 
        }
    }
}
