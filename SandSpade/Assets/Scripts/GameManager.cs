using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation); // Spawns the player on the spawn point
    }
}
