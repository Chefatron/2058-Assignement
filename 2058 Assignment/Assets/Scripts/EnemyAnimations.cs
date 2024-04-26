using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the animator
        enemyAnimator = GetComponentInChildren<Animator>();
    }

    public void setRun(bool state)
    {
        // Sets the running parameter to the fed in state
        enemyAnimator.SetBool("Running", state);
    }

    public void setJump(bool state)
    {
        // Sets the jump parameter to the fed in state
        enemyAnimator.SetBool("Jumping", state);
    }

    public void setDirection(string direction)
    {
        // Checks the current state and reverses it
        if (direction == "Right")
        {
            // Sets the left parameter to false
            enemyAnimator.SetBool("Left", false);

            // Sets the right parameter to true
            enemyAnimator.SetBool("Right", true);
        }
        else if (direction == "Left")
        {
            // Sets the right parameter to false
            enemyAnimator.SetBool("Right", false);

            // Sets the left parameter to true
            enemyAnimator.SetBool("Left", true);
        }
    }

    public void setAttacking(bool state) 
    {
        // Sets the attacking parameter to the fed in state
        enemyAnimator.SetBool("Attacking", state);
    }
}
