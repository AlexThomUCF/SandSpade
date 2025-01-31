using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriedItem : MonoBehaviour
{
    public float fallSpeed = 5f;
    private Rigidbody2D rb;
    private bool isFalling = false;
    public LayerMask sandLayer;
    public float raycastDistance = 1.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // Keep the Buried Item still at the start
    }

    void Update()
    {
        if (!isFalling && !IsGroundBelow()) // Only fall if no ground is below
        {
            StartFalling();
        }
    }

    bool IsGroundBelow()
    {
        // Raycast directly downward to check for sand
        Vector2 rayStart = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, raycastDistance, sandLayer);

        if (hit.collider != null)
        {
            Debug.DrawRay(rayStart, Vector2.down * raycastDistance, Color.green); // Ray for me to see
            Debug.Log("Ground detected below: " + hit.collider.gameObject.name);
            return true; // There is ground below
        }
        else
        {
            Debug.DrawRay(rayStart, Vector2.down * raycastDistance, Color.red); // Ray for me to see
            Debug.Log("No ground below. Rock will fall.");
            return false; // No ground detected
        }
    }

    void StartFalling()
    {
        Debug.Log("Rock starting to fall!");
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic; // Enable physics for falling
        rb.velocity = Vector2.down * fallSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & sandLayer) != 0) // Check if it hit the Sand layer
        {
            Debug.Log("Rock hit the sand. Destroying rock.");
            Destroy(gameObject);
        }
    }
}
