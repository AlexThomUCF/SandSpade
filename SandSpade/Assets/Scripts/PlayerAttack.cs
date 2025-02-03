using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public PlayerController player;
    public float pumpDistance = 1.0f;
    public LayerMask attackLayer;
    public Animator weaponAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.DrawRay(transform.position, player.movementDirection * pumpDistance, Color.green);    
        if(Input.GetKey(KeyCode.Z))
        {
            player.knightroAnim.SetBool("isAttacking", true);
            weaponAnim.SetBool("shootPump", true);
            LaunchAttack();
            Debug.Log("Threw attack");
        }
    }

    void LaunchAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.movementDirection, pumpDistance,attackLayer);
        Debug.DrawRay(transform.position, player.movementDirection * pumpDistance, Color.green);
    }
}

