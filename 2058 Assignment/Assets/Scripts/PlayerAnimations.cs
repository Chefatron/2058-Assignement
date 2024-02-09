using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the animator
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRun(bool state)
    {
        // Sets the running parameter to the fed in state
        playerAnimator.SetBool("Running", state);
    }

    public void setJump(bool state)
    {
        // Sets the jump parameter to the fed in state
        playerAnimator.SetBool("Jumping", state);
    }

    public void setCrouch(bool state)
    {
        playerAnimator.SetBool("Crouching", state);
    }

    public void setDirection(string direction)
    {
        // Checks the current state and reverses it
        if (direction == "Right")
        {
            // Sets the left parameter to false
            playerAnimator.SetBool("Left", false);

            // Sets the right parameter to true
            playerAnimator.SetBool("Right", true);
        }
        else if (direction == "Left")
        {
            // Sets the right parameter to false
            playerAnimator.SetBool("Right", false);

            // Sets the left parameter to true
            playerAnimator.SetBool("Left", true);
        }
    }
}
