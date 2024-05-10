using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class isAttackable : MonoBehaviour
{
    // The rigidbody of the attacked object
    Rigidbody objectRB;

    // Used to check if the object has been hit or not so it doesn't get hit lots of times
    public bool wasHit;

    // Used to disable the nav mesh agent on being hit so the enemies that have this script can be knocked up or back
    EnemyAI enemy;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the RB
        objectRB = GetComponent<Rigidbody>();

        // Sets default to was not hit
        wasHit = false;

        // Gets agent if it has one
        if (gameObject.GetComponent<EnemyAI>())
        {
            enemy = gameObject.GetComponent<EnemyAI>();
        }
    }

    private void Update()
    {

    }

    // Used for smaller hits in combos does a little bit of damage and knockback
    public void smallHit(Vector3 knockbackDirection)
    {
        if (wasHit == false)
        {
            objectRB.velocity = Vector3.zero;

            objectRB.AddForce(knockbackDirection, ForceMode.Impulse);

            enemy.knock(2f);
        }  
    }

    // Used for final or heavy hits in combos does a good amount of damage and knockback
    public void largeHit(Vector3 knockbackDirection) 
    {
        if (wasHit == false) 
        {
            objectRB.velocity = Vector3.zero;

            objectRB.AddForce(knockbackDirection * 10f, ForceMode.Impulse);

            enemy.knock(4f);
        }  
    }
}
