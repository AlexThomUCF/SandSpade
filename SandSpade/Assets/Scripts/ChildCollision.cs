using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{
    private DuckScript duckScript;
    

    void Start()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("Child Collided with Enemy");

            if(duckScript.duckAnimationThing != null)
            {
                duckScript.duckAnimationThing.SetTrigger("Die");
            }
            
        }
        
    }
}
