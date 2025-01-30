using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    float speed = 10f;
    private Rigidbody2D rb; 
    private float digTimer = 0.0f;
    private float waitTime = 2.0f;
    private bool canDig = false; 

    private Vector2 movementDirection;
    private float digDistance = .75f;

    public LayerMask layerMask; 
    private RaycastHit2D hit;
    private Tilemap tilemap;
    private Vector3Int tilePosition;
    
     

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tilemap = FindObjectOfType<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

         
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            FlipPlayer(-1); // Flip sprite to face left
        }
        if (Input.GetKeyDown(KeyCode.D)) // Player moves right
        {
            FlipPlayer(1); // Flip sprite to face right
        }

    
    }

    
    
    void FixedUpdate()
    {
        rb.velocity = movementDirection * speed;

        movementDirection = movementDirection.normalized;

        if (movementDirection != Vector2.zero) // if player is moving 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, digDistance, layerMask); // sends raycast from player position, pointing wherever the player is moving 

            Debug.DrawRay(transform.position, movementDirection * digDistance, Color.red);
        
            if(hit.collider != null)
             {
                tilePosition = tilemap.WorldToCell(hit.point);

                Debug.Log("Hit doesn't = null");
                Dig();
             }
        }

    }

    void Dig()
    {
        Debug.Log("Is Destroying");
        if(tilemap.HasTile(tilePosition))
        {
            tilemap.SetTile(tilePosition, null);
            tilemap.RefreshAllTiles();
            //Play dig animation here?
            Debug.Log("Tile removed at: " + tilePosition);


        }
        else
        {
            Debug.Log("No tile found at: " + tilePosition);
        }
        
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

    void FlipPlayer(int direction)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = direction; // Set the X scale to 1 (right) or -1 (left)
        transform.localScale = localScale;
    }

}
