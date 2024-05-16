using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PaladinAblities : MonoBehaviour
{
    // Get the player attriutes scriptable object to edit the speed multiplier while attacking
    [SerializeField] PlayerAttributes playerAttributes;

    // Used to tell which stage of the attack combo the player is in
    public int attackStage;

    // Used to tell if the player is currently in a combo
    bool inCombo;

    // Used to tell if the player is in cooldown
    bool inCooldown;

    // A float that will be used to count the time between inputs to see if the player continues their combo or restarts
    float attackBuffer;

    // Used to force the player to wait a little bit before they can attack again
    float attackCooldown;

    // The script that handles the player animations
    PlayerAnimations animations;

    // Used to tell if the user is currently holding the special button
    bool isSpecial;

    // Used to space out the healing the player gets from the special
    float specialInterval;

    // Used to tell if the player clicked the attack button while in a cooldown to do thier attack after the the cooldown ends
    bool isQueued;

    // Particles for the special ability
    ParticleSystem healParticles;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the defualt of stage 0 or not attacking
        attackStage = 0;

        // Gets the animation script
        animations = GetComponent<PlayerAnimations>();

        // Sets defualt speed multi
        playerAttributes.playerSpeedMultiplier = 1f;

        // Sets defualt special state
        isSpecial = false;

        // Sets defualt special timer
        specialInterval = 2f;

        // Get particle system
        healParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the player is attacking
        if (inCombo)
        {
            // Checks if the attack buffer is higher than zero and counts it down like a timer if so
            if (attackBuffer > 0)
            {
                attackBuffer = attackBuffer - Time.deltaTime;
            }
            else
            {
                attackBuffer = 0;

                // Since the buffer ran out the combo must be cancelled
                inCombo = false;

                // Resets the stage as the player exited the combo
                attackStage = 0;

                // Gives the player back their speed
                playerAttributes.playerSpeedMultiplier = 1f;

                // Updates animations based on combo stage
                animations.setAttacking(attackStage);
            }
        }

        // Checks if the player is on a cool down
        if (inCooldown)
        {
            // Checks if the cool down still has value and decrements it if so, allows another attack when it runs out 
            if (attackCooldown > 0)
            {
                attackCooldown = attackCooldown - Time.deltaTime;
            }
            else if (attackStage == 3)
            {
                // Sets the attack to the final stage of four for the final up hit of the spin attack
                attackStage = 4;
                attackCooldown = 1f;
            }
            else if (attackStage == 4)
            {
                // Sets the cool down number to exact zero just in case
                attackCooldown = 0;

                // Resets attack variables so a new attack can begin
                inCooldown = false;
                attackStage = 0;

                // Gives the player back their speed
                playerAttributes.playerSpeedMultiplier = 1f;

                // Updates animations based on combo stage
                animations.setAttacking(attackStage);
            }
            else
            {
                // Sets the cool down number to exact zero just in case
                attackCooldown = 0;

                // Resets the cooldown variable so a new attack can start
                inCooldown = false;
            }
        }

        if (isSpecial && specialInterval > 0)
        {
            specialInterval = specialInterval - Time.deltaTime; 
        }
        else if (isSpecial && playerAttributes.playerHP < 10)
        {
            playerAttributes.playerHP++;

            specialInterval = 2f;
        }
    }

    // Called when the player presses the attack input (mouse 1 or maybe right trigger)
    void OnAttack()
    {
        // Checks if the player isn't in cooldown
        if (inCooldown == false)
        {
            // Checks if the attack stage is on the final stage right now and resets it if so
            if (attackStage == 4)
            {
                attackStage = 0;
            }

            // Sets the player speed to slower
            playerAttributes.playerSpeedMultiplier = 0.1f;

            // If statement checks the attack stage and does the corresponding attack funcitons
            if (attackStage == 0) // Swing
            {
                // Sets the timed attack buffer
                attackBuffer = 1.5f;

                // Now the combo has started the status must be set to isAttacking
                inCombo = true;

                // Increments the attack stage
                attackStage++;

                // Sets a cooldown so the player can't move to the next stage of attack until it runs out
                inCooldown = true;

                // Gives a small countdown since the swing animation is short
                attackCooldown = 0.5f;
            }
            else if (attackStage == 1 && inCombo == true) // Jab
            {
                // Sets the timed attack buffer
                attackBuffer = 1.5f;

                // Increments the attack stage
                attackStage++;

                // Sets a cooldown so the player can't move to the next stage of attack until it runs out
                inCooldown = true;

                // Gives a small countdown since the jab animation is short
                attackCooldown = 0.5f;
            }
            else if (attackStage == 2 && inCombo == true) // Spin
            {
                // Sets no time for the attack buffer as its the end of the attack
                attackBuffer = 0f;

                // Ends the combo
                inCombo = false;

                // Increments the attack stage
                attackStage++;

                // Puts the player in cooldown
                inCooldown = true;

                // Sets the cooldown for the next attack
                attackCooldown = 1f;
            }

            isQueued = false;

            // Updates animations based on combo stage
            animations.setAttacking(attackStage);
        }
        else if (isQueued == false)
        {
            Invoke("OnAttack", attackCooldown + 0.01f);

            isQueued = true;
        }
    }

    // is called on press and release
    void OnSpecial()
    {
        // Sets is special to opposite of current state
        isSpecial = !isSpecial;

        // Updates attributes for reading globally
        playerAttributes.isSpecial = isSpecial;

        // Updated animations
        animations.setSpecial(isSpecial);

        // Checks the state set and updates both the particles and timer
        if (isSpecial == false)
        {
            specialInterval = 2f;

            healParticles.Stop();
        }
        else 
        { 
            healParticles.Play();
        }
    }
}
