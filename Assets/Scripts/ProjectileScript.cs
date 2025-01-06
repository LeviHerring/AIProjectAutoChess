using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public int damage;
    public PlayerTypes.Team team; 
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.right * 5;

        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<UnitBase>())
        {
            if (collision.gameObject.GetComponent<UnitBase>().Team == team)
            {
                Debug.Log("Hit Same");
            }
            else
            {
                Debug.Log("Hit opposite"); 
                collision.gameObject.GetComponent<UnitBase>().health -= damage; 
                Destroy(gameObject); 
            }
        }
    }
}
