using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public PlayerController player;
    public float pumpDistance = 1.0f;
    public LayerMask attackLayer;
    public Animator weaponAnim;
    public RaycastHit2D hit;
    private EnemyNavigation enemy;
    public float waitTime = 2f;
    private float timer = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()  
    {     
        timer += Time.deltaTime;

        if(Input.GetKey(KeyCode.Z) && timer >= waitTime) // If player attacks 
        {
            player.knightroAnim.SetBool("isAttacking", true);
            weaponAnim.SetBool("shootPump", true);
            LaunchAttack();
            Debug.Log("Threw attack");
            StartCoroutine(StopAttack());
            timer = 0f;
        }
    }

    void LaunchAttack()
    {
        hit = Physics2D.Raycast(transform.position, player.transform.right, pumpDistance,attackLayer);
        Debug.DrawRay(transform.position, player.transform.right * pumpDistance, Color.cyan);
        
        
        if (hit.collider != null && hit.collider.CompareTag("Enemy")) // Check if the object hit has the "Enemy" tag
        {
             enemy = hit.collider.GetComponent<EnemyNavigation>(); // Get the Enemy script attached to the enemy

            if (enemy != null) // Make sure the enemy script exists
            {
                Debug.Log("Time to die");
                enemy.duckAnim.SetTrigger("Death"); // Trigger the death animation
                Destroy(hit.collider.gameObject, 1.0f); // Destroy enemy after 1 second
            }
        }
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(0.5f);
        player.knightroAnim.SetBool("isAttacking", false);
        weaponAnim.SetBool("shootPump", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            //weaponAnim.speed = 0f;
            Debug.Log("Paused");
        }
        
    }

}

