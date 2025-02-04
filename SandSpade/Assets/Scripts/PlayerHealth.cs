using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    PlayerController player;
    public DuckScript duck;
 
    public int lives = 3; // Player starts with 3 lives
    void Start()
    {
        player = GetComponent<PlayerController>();
        duck = GetComponent<DuckScript>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Lord help me");
            return;
        }
        else if (collision.gameObject.CompareTag("Enemy")) // Check if colliding with an enemy
        {
            LoseLife();
        }
        else if (collision.gameObject.CompareTag("Buried Item")) // Check if colliding with a Buried item
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        lives--; // Reduce lives by 1
        Debug.Log("Player hit! Lives remaining: " + lives);

        if (lives <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        player.knightroAnim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject); // Destroy player when lives reach 0
        Debug.Log("Player is out of lives!");

        SceneManager.LoadScene("GameOver");
    }
}
