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
        if(player.transform.localEulerAngles.y != 0 || player.transform.localEulerAngles.y  != 180 || player.transform.localEulerAngles.z != 90 || player.transform.localEulerAngles.y != -90){
             LaunchAttack();}
    }

    void LaunchAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.movementDirection, pumpDistance,attackLayer);
        Debug.DrawRay(transform.position, player.movementDirection * pumpDistance, Color.green);
    }
}

