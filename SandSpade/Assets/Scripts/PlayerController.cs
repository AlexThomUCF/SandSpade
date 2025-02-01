using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb; 

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

                    
        if (movementDirection.x < 0) // Moving left
         {
           FlipPlayer(-1, 1); // Flip left, keep upright
         }
        else if (movementDirection.x > 0) // Moving right
         {
           FlipPlayer(1, 1); // Flip right, keep upright
         }

         if (movementDirection.y > 0) // Moving up
         {
           FlipPlayer(1, 1); // Keep facing right, flip vertically
         }
        else if (movementDirection.y < 0) // Moving down
         {
          FlipPlayer(1, -1); // Keep facing right, flip vertically
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
                Vector3 adjustedHitPoint = (Vector3)hit.point + (Vector3)(movementDirection * 0.1f);
                tilePosition = tilemap.WorldToCell(adjustedHitPoint);
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
            //Play dig animation here? or make seperate script and bool.
            Debug.Log("Tile removed at: " + tilePosition);


        }
        else
        {
            Debug.Log("No tile found at: " + tilePosition);
        }
        
    }

    /*void OnCollisionStay2D(Collision2D coll) // if player is on ground for more than 2 seconds, they can dig,
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
    }*/

    void FlipPlayer(int directionX, int directionY)
{
    Vector3 localScale = transform.localScale;

    // Flip along the X-axis (left or right)
    localScale.x = directionX;

    // Flip along the Y-axis (up or down)
    localScale.y = directionY;

    // Apply the new scale to the player
    transform.localScale = localScale;
}

}
