using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    public float enemySpeed = 2f;
    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool movingVertically = false; // Tracks if enemy is moving vertically
    private bool canMoveVertically = true; // Cooldown control

    public float detectionDistance = 2f;
    public float verticalMoveDelay = 0.3f;
    public float verticalCooldownTime = 5f;
    private float lastVerticalMoveTime = 0f;

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
        bool pathAbove = !CheckForTag(Vector2.up);
        bool pathBelow = !CheckForTag(Vector2.down);

        if (canMoveVertically && (pathAbove || pathBelow) && Time.time > lastVerticalMoveTime + verticalCooldownTime)
        {
            StartCoroutine(DelayedVerticalMove(pathAbove, pathBelow));
        }
    }

    bool CheckForTag(Vector2 direction)
    {
        // Get the box collider
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null) return false; // Fail-safe check

        // Get the bounds of the collider
        Vector2 boundsMin = boxCollider.bounds.min; // Bottom-left corner
        Vector2 boundsMax = boxCollider.bounds.max; // Top-right corner

        // Determine the two corner positions based on movement direction
        Vector2 corner1, corner2;
        if (direction == Vector2.up) // Moving up, check top corners
        {
            corner1 = new Vector2(boundsMin.x, boundsMax.y); // Top-left
            corner2 = new Vector2(boundsMax.x, boundsMax.y); // Top-right
        }
        else if (direction == Vector2.down) // Moving down, check bottom corners
        {
            corner1 = new Vector2(boundsMin.x, boundsMin.y); // Bottom-left
            corner2 = new Vector2(boundsMax.x, boundsMin.y); // Bottom-right
        }
        else // Horizontal check (not needed in your case, but keeping it structured)
        {
            corner1 = transform.position;
            corner2 = transform.position;
        }

        // Cast two rays
        RaycastHit2D hit1 = Physics2D.Raycast(corner1, direction, detectionDistance);
        RaycastHit2D hit2 = Physics2D.Raycast(corner2, direction, detectionDistance);

        // Debugging
        Debug.DrawRay(corner1, direction * detectionDistance, Color.red);
        Debug.DrawRay(corner2, direction * detectionDistance, Color.blue);

        // Check if either ray hits something tagged "sand"
        if ((hit1.collider != null && hit1.collider.CompareTag("Sand")) ||
            (hit2.collider != null && hit2.collider.CompareTag("Sand")))
        {
            return true;
        }

        return false;
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
        bool wallAhead = CheckForTag(verticalDirection);
        Debug.DrawRay(transform.position, verticalDirection * detectionDistance, Color.yellow);

        if (wallAhead)
        {
            movingVertically = false;
            canMoveVertically = true; // Reset vertical movement cooldown

            // Small nudge to get away from the wall
            transform.position += (Vector3)(movingRight ? Vector2.left : Vector2.right) * 0.1f;

            // Restart horizontal movement
            MoveHorizontally();
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
