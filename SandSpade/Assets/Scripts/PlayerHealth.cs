using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    PlayerController player;
    public int lives = 3; // Player starts with 3 lives

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Buried Item"))
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        lives--; // Reduce lives by 1
        Debug.Log("Player hit! Lives remaining: " + lives);

        // Hide life icons based on remaining lives
        if (lives == 2)
            GameObject.FindWithTag("life3").SetActive(false);
        else if (lives == 1)
            GameObject.FindWithTag("life2").SetActive(false);
        else if (lives == 0)
            GameObject.FindWithTag("life1").SetActive(false);

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