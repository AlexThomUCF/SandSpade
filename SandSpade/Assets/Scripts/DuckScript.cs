using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckScript : MonoBehaviour
{
    public Animator duckAnimationThing;
    public Rigidbody2D duckRB;

    // Start is called before the first frame update
    void Start()
    {
        duckAnimationThing = GetComponent<Animator>();
        duckRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DuckAnimation()
    {
        StartCoroutine(DuckCycle());
    }

    IEnumerator DuckCycle()
    {
        duckAnimationThing.SetBool("enemyHit", true);
        Debug.Log("Enemy is now hit");
        yield return new WaitForSeconds(1.5f);
        duckAnimationThing.SetBool("enemyIsDead", true);
    }

    /* void DuckWalk()
    {
        if(enemy.enemySpeed != 0)
        {

        }
    }*/

}
