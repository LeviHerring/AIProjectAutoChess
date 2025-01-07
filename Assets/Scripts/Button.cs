using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Button : MonoBehaviour
{
    public GameObject prefab;
    Mouse mouse;
    public int cost; 

    public void Start()
    {
        mouse = FindObjectOfType<Mouse>(); 
    }

    public void PickObject()
    {
        mouse.prefab = prefab;
        mouse.cost = prefab.GetComponent<UnitBase>().Attributes.Price; 
    }

}
