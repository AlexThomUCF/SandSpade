using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    public string enemyTag = "Enemy";
    public string nextLevelName = "Level_Two";
    public string winSceneName = "win";
    public string playSceneName = "play";
    public string gameOverSceneName = "gameover";
    private bool isLevelTwo = false;

    void Awake()
    {
        // Destroys duplicate GameManagers
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep the GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Ensure player doesn't spawn if one already exists
        if (FindObjectOfType<PlayerController>() == null)
        {
            if (SceneManager.GetActiveScene().name != nextLevelName && SceneManager.GetActiveScene().name != winSceneName
                && SceneManager.GetActiveScene().name != playSceneName && SceneManager.GetActiveScene().name != gameOverSceneName)
            {
                SpawnPlayer();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Universal escape key
        {
            Application.Quit();
        }

        // Don't check for enemies outside of level one and level 2
        if (SceneManager.GetActiveScene().name != winSceneName && SceneManager.GetActiveScene().name != playSceneName && SceneManager.GetActiveScene().name != gameOverSceneName)
        {
            CheckForEnemies();
        }
    }

    void SpawnPlayer() // Spawns the player
    {
        Vector3 spawnPosition = new Vector3(0f, 3.74f, 0f);
        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }

    void CheckForEnemies()
    {
        // Finds anything with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // If no enemies are left, load the next level or win scene
        if (enemies.Length == 0)
        {
            if (SceneManager.GetActiveScene().name == "Level_Two")
            {
                LoadWinScene();
            }
            else
            {
                LoadNextLevel();
            }
        }
    }

    void LoadNextLevel()
    {
        isLevelTwo = true;
        SceneManager.LoadScene(nextLevelName); // Load Level two
    }

    void LoadWinScene()
    {
        SceneManager.LoadScene(winSceneName); // Load Win scene
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Spawns player again after restart but only if an instance of the player doesnt already exist
        if ((scene.name == nextLevelName || scene.name == "Level_One") && FindObjectOfType<PlayerController>() == null)
        {
            SpawnPlayer();
        }
    }
}
