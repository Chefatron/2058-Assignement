using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Used for getting a variety of into from the player that the enemy needs to know how to act
    [SerializeField] PlayerAttributes player;

    // The nav mesh component for moving the enemy
    NavMeshAgent enemy;

    // Changes the animation based on the state
    EnemyAnimations animations;

    // Used to keep track of what actions the enemy should be taking
    // 0 = idle // Walking back and forth
    // 1 = chasing player
    // 2 = attacking 
    // 3 = knocked // Navmesh ai disabled
    public int state;

    // A value to be fed with a sin wave to make the enemy move back and forth
    float positionOffset;

    // Used to know for how long an enemy with this script should be incapacitated
    float recoveryTimer;

    // The visible object of the enemy, used to make the enemy snap face left and right
    Transform mesh;

    // Used to space out the enemies attacks
    float attackinterval;

    // Self explanatory
    [SerializeField] float enemyHealth;

    // How much damage the enemy does
    [SerializeField] int enemyDamage;

    // Speed of the enemy
    [SerializeField] int enemySpeed;

    // Couldn't think of a better name but it basically controls how far their idle paths branch from origin
    [SerializeField] float idleStretch;

    // Start is called before the first frame update
    void Start()
    {
        // Get the nav agent
        enemy = GetComponent<NavMeshAgent>();

        // Set the enemy speed based on the editor value
        enemy.speed = enemySpeed;

        // Get the animations
        animations = GetComponentInChildren<EnemyAnimations>();

        // Sets the defualt state to idle
        state = 0;
        
        // Used for having the same snappy left and right turning the player without affecting the dynamic movement of the nav agent
        mesh = gameObject.GetComponentInChildren<Transform>();
        
        // Set defualt of 2 seconds
        attackinterval = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            // Changes the offset to the enemies position based on a sin wave
            positionOffset = Mathf.Sin(Time.time * idleStretch);

            // Updates the enemy destination
            enemy.destination = new Vector3(transform.position.x + positionOffset, transform.position.y, transform.position.z);
        }
        else if (state == 1)
        {
            // Sets the target to the players position
            enemy.destination = player.playerPosition;

            // Checks if the enemy is within a certain distance of the player
            if (enemy.remainingDistance < 3f)
            {
                state = 2;

                enemy.isStopped = true;
            }
        }
        else if (state == 2)
        {
            // Checks if the player gets a certain distance away from the enemy
            if (Vector3.Distance(enemy.transform.position, player.playerPosition) > 4f) 
            {
                state = 1;

                enemy.isStopped = false;

                attackinterval = 2f;
            }

            // Checks if attack interval has reached zero and damages the player if so resetting the timer
            if (attackinterval <= 0)
            {
                player.playerHP -= enemyDamage;

                animations.setAttacking(true);

                attackinterval = 2f;
            }
            else
            {
                attackinterval = attackinterval - Time.deltaTime;

                animations.setAttacking(false);
            }

        }
        else if (state == 3)
        {
            // Ticks down the timer
            recoveryTimer = recoveryTimer - Time.deltaTime;

            // Checks if the timer has run out
            if (recoveryTimer < 0)
            {
                // Resets timer
                recoveryTimer = 0f;

                // Sets state to idle
                state = 0;

                // Re-enables the AI
                enemy.enabled = true;
            }
        }

        // Checks if the enemy is moving and sets the animatiion based on it
        if (enemy.velocity.magnitude != 0 && state < 2)
        {
            animations.setRun(true);
        }
        else
        {
            animations.setRun(false);
        }

        // Rotates the mesh
        mesh.rotation = (Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(enemy.destination.x - mesh.position.x), 0)));

        // Sets left and right animations based on the destinations position
        if (enemy.destination.x > transform.position.x && enemy.destination.x < transform.position.x + 2)
        {
            animations.setDirection("Right");
        }
        else if (enemy.destination.x < transform.position.x && enemy.destination.x > transform.position.x - 2)
        {
            animations.setDirection("Left");
        }
    }

    public void knock(float time)
    {
        // Disables the nav so the enemy can be knocked
        enemy.enabled = false;

        // Sets the timer to the fed time
        recoveryTimer = time;

        // Tells the AI its in state 3
        state = 3;

        // Disables all animations
        animations.falseAll();
    }

    public void damage(float damage)
    {
        enemyHealth = enemyHealth - damage;

        if (enemyHealth <= 0) 
        {
            kill();
        }
    }

    void kill()
    {
        enemy.enabled = false;

        Destroy(gameObject);
    }
}
