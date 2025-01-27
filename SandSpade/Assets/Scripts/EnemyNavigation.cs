using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    public float enemySpeed = 5f;
    private Rigidbody2D rb;
    
    public GameObject target;
    private bool flip = false;

    void Start()
    {
      rb = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        flip = false;
        //rb.velocity = Vector2.MoveTowards(transform.position, targetpos, 0.0f);

        /*if(flip == false){
            rb.velocity = Vector2.right * enemySpeed;
        }*/
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        transform.localScale = new Vector2 (-1, 1);
        flip = true; 
        Debug.Log("Flip is " + flip);
       
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        flip = false;
        Debug.Log("Flip is " + flip);
    }

}
