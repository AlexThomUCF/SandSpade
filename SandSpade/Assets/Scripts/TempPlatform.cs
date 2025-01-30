using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlatform : MonoBehaviour
{
    public float lifetime = 5f; // Time before platform disappears

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after `lifetime` seconds
    }
}
