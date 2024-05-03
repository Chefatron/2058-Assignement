using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // The player
    GameObject player;

    // The nav mesh component for moving the enemy
    NavMeshAgent enemy;

    // Changes the animation based on the state
    EnemyAnimations animations;

    // Used to keep track of what actions the enemy should be taking
    // 0 = idle
    // 1 = chasing player
    // 2 = attacking
    // 3 = knocked
    public int state;

    // A value to be fed with a sin wave to make the enemy move back and forth
    float positionOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player
        player = GameObject.Find("Player");

        // Get the nav agent
        enemy = GetComponent<NavMeshAgent>();

        // Get the animations
        animations = GetComponentInChildren<EnemyAnimations>();

        // Sets the defualt state to idle
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            positionOffset = Mathf.Sin(Time.time);

            enemy.destination = new Vector3(transform.position.x + positionOffset, transform.position.y, transform.position.z);
        }
        else if (state == 2)
        {
            enemy.destination = player.transform.position;
        }


    }
}
