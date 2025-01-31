using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public PlayerController player;
    public float pumpDistance = 3.0f;
    public LayerMask attackLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            LaunchAttack();
        }
    }

    void LaunchAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, pumpDistance,attackLayer);
        Debug.DrawRay(transform.position, Vector2.right * pumpDistance, Color.green);
    }
}

