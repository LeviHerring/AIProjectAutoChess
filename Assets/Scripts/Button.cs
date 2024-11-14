using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject prefab;
    Mouse mouse; 
    public void Start()
    {
        mouse = FindObjectOfType<Mouse>(); 
    }

    public void PickObject()
    {
        mouse.prefab = prefab;  
    }

}
