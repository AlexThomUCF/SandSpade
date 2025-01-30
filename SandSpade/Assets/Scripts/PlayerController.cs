using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 10f;
    private Rigidbody2D rb; 
    private float digTimer = 0.0f;
    private float waitTime = 2.0f;
    private bool canDig; 

    private Vector2 movementDirection;
    private float digDistance = 2.0f;

    public LayerMask layerMask; 
    private RaycastHit2D hit;
    
     

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
       /* if(Input.GetKeyDown(KeyCode.S)){
            Dig();
        }
        */
    
    }
    
    void FixedUpdate()
    {
        rb.velocity = movementDirection * speed;

        hit = Physics2D.Raycast(transform.position, movementDirection, digDistance); // sends raycast from player position, pointing wherever the player is moving 

        Debug.DrawRay(transform.position, movementDirection * digDistance, Color.red);
        
        if(hit.collider != null && hit.collider.gameObject.layer == layerMask) //Doesnt run bc  hit.collider.gameObject.layer == layerMask, If left out player object gets destroyed by Dig();
        {
            Debug.Log("Hit doesn't = null");
            Dig();
        }
    }

    void Dig()
    {
        Debug.Log("Is Destroying");
        Destroy(hit.collider); // if you leave this and delete && statement above, deletes whole collider for tilemap. 
        
    }

    void OnCollisionStay2D(Collision2D coll) // if player is on ground for more than 2 seconds, they can dig,
    {
        if(coll.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Hit Ground");
            
            digTimer += Time.deltaTime;

            if(digTimer >= waitTime)
            {
                //Debug.Log("Timer is at " + digTimer);
                canDig = true;
            }
        }

    }

    void OnCollisionExit2D(Collision2D coll)
    {
        digTimer = 0.0f;
        canDig = false;
        Debug.Log("Dig reset");
    }

}
