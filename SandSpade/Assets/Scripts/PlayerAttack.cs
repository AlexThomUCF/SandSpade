using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public PlayerController player;
    public float pumpDistance = .1f;
    public LayerMask attackLayer;
    public Animator weaponAnim;
    public RaycastHit2D hit;
    private EnemyNavigation enemy;
    public float waitTime = 1f;
    private float timer = 0f;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip attackSound;
    public bool soundCanPlay = false;
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()  
    {     
        timer += Time.deltaTime;

        if(Input.GetKey(KeyCode.Z) && timer >= waitTime) // If player attacks 
        {
            soundCanPlay = true;
            PlayAudio();
            player.knightroAnim.SetBool("isAttacking", true);
            weaponAnim.SetBool("shootPump", true);
            StartCoroutine(LaunchAttack());
            Debug.Log("Threw attack");
            StartCoroutine(StopAttack());
            timer = 0f;
        }
        soundCanPlay = false;
    }

    IEnumerator LaunchAttack()
{
    float duration = 1.0f; // Attack duration
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        hit = Physics2D.Raycast(transform.position, player.transform.right, pumpDistance, attackLayer);
        Debug.DrawRay(transform.position, player.transform.right * pumpDistance, Color.green);

        if (hit.collider != null && hit.collider.CompareTag("Enemy")) // Check if the object hit has the "Enemy" tag
        {
            enemy = hit.collider.GetComponent<EnemyNavigation>(); // Get the Enemy script attached to the enemy

            if (enemy != null) // Make sure the enemy script exists
            {
                Animator enemyAnimator = hit.collider.GetComponent<Animator>(); //hit gets enemy animator
                if (enemyAnimator != null)
                {
                    Debug.Log("Playing animation");
                    enemy.enemySpeed = 0f;
                    enemyAnimator.SetBool("duckHit", true);
                    player.knightroAnim.SetBool("attackSuccess", true);
                    StartCoroutine(PauseAttack());
                    
                }
                StartCoroutine(enemy.EnemyPop());
                player.knightroAnim.SetBool("attackSuccess", false);
                Destroy(hit.collider.gameObject, 2.0f); // Destroy enemy after 2 sec
            }   
        }

        elapsedTime += Time.deltaTime;
        yield return null; // Wait for next frame
    }
}


    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(0.5f);
        player.knightroAnim.SetBool("isAttacking", false);
        weaponAnim.SetBool("shootPump", false);
    }

    IEnumerator PauseAttack()
    {
        Debug.Log("Paused");
        yield return new WaitForSeconds(1.90f);
        player.knightroAnim.speed = 1.0f;
    }

    public void PlayAudio()
    {
        audioSource2.PlayOneShot(attackSound);
    }



}

