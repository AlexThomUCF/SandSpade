using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    public float enemySpeed = 3f;
    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool movingVertically = false; // Tracks if enemy is moving vertically
    private bool canMoveVertically = true; // Cooldown control

    public float detectionDistance = 2f;
    public float verticalMoveDelay = 0.3f;
    public float verticalCooldownTime = 5f;
    private float lastVerticalMoveTime = 0f;

    public LayerMask sandLayer;

    private Vector2 verticalDirection; // Tracks current vertical direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        if (movingVertically)
        {
            ContinueVerticalMove();
        }
        else
        {
            MoveHorizontally();
            DetectPaths();
        }
    }

    void MoveHorizontally()
    {
        rb.velocity = movingRight ? Vector2.right * enemySpeed : Vector2.left * enemySpeed;
    }

    void DetectPaths() // Detects if there is a vertical path
    {
        bool pathAbove = !Physics2D.Raycast(transform.position, Vector2.up, detectionDistance, sandLayer);
        bool pathBelow = !Physics2D.Raycast(transform.position, Vector2.down, detectionDistance, sandLayer);

        if (canMoveVertically && (pathAbove || pathBelow) && Time.time > lastVerticalMoveTime + verticalCooldownTime)
        {
            StartCoroutine(DelayedVerticalMove(pathAbove, pathBelow));
        }
    }

    IEnumerator DelayedVerticalMove(bool pathAbove, bool pathBelow)
    {
        canMoveVertically = false;
        yield return new WaitForSeconds(verticalMoveDelay); // Delays vertical movement

        if (pathAbove)
        {
            verticalDirection = Vector2.up;
        }
        else if (pathBelow)
        {
            verticalDirection = Vector2.down;
        }
        else
        {
            canMoveVertically = true;
            yield break; // No path up or down, do nothing
        }

        movingVertically = true; // Start moving vertically
        lastVerticalMoveTime = Time.time;
    }

    void ContinueVerticalMove() // Enemy continues to move vertically until it detects a wall
    {
        rb.velocity = verticalDirection * enemySpeed;

        // Checks for a wall in the current direction
        bool wallAhead = Physics2D.Raycast(transform.position, verticalDirection, detectionDistance, sandLayer);
        Debug.DrawRay(transform.position, verticalDirection * detectionDistance, Color.yellow);

        if (wallAhead)
        {
            movingVertically = false;
            canMoveVertically = true; // Reset vertical movement cooldown
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!movingVertically)
        {
            movingRight = !movingRight;
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
