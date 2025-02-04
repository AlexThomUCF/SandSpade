using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEscapes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Check if the colliding object is tagged as "Enemy"
        {
            SceneManager.LoadScene("gameover"); // Load the Game Over scene
        }
    }
}
