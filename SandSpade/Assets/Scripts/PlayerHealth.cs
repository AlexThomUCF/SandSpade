using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    PlayerController player;
    public int lives = 3; // Player starts with 3 lives
    public List<RawImage> lifeIcons; // Use RawImage instead of Image

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

        if (lifeIcons.Count > 0) 
        {
            lifeIcons[lifeIcons.Count - 1].gameObject.SetActive(false); // Hide the last life icon
            lifeIcons.RemoveAt(lifeIcons.Count - 1); // Remove reference
        }

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