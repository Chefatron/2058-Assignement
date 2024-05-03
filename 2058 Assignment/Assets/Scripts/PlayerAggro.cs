using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAggro : MonoBehaviour
{
    // A big box around the player that any enemies who enter become aggro'd
    BoxCollider aggroBox;

    // Start is called before the first frame update
    void Start()
    {
        aggroBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAI>())
        {
            other.GetComponent<EnemyAI>().state = 2;
        }
    }
}
