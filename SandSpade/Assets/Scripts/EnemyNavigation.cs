using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    public float enemySpeed = 5f; // Speed of the enemy
    private Rigidbody2D rb; // Ridgidbody reference
    private bool movingRight = true; // Tracks the enemy's direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Move the enemy based on direction
        if (movingRight)
        {
            rb.velocity = Vector2.right * enemySpeed;
        }
        else
        {
            rb.velocity = Vector2.left * enemySpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Flip direction
        movingRight = !movingRight;

        // Flip sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        Debug.Log("Flipped direction. Moving right: " + movingRight);
    }
}
