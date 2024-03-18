using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isAttackable : MonoBehaviour
{
    // The rigidbody of the attacked object
    Rigidbody objectRB;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the RB
        objectRB = GetComponent<Rigidbody>();
    }

    // Used for smaller hits in combos does a little bit of damage and knockback
    public void smallHit(Vector3 knockbackDirection)
    {
        objectRB.AddForce(knockbackDirection, ForceMode.Impulse);
    }

    // Used for final or heavy hits in combos does a good amount of damage and knockback
    public void largeHit(Vector3 knockbackDirection) 
    {
        objectRB.AddForce(knockbackDirection * 10f, ForceMode.Impulse);
    }
}
