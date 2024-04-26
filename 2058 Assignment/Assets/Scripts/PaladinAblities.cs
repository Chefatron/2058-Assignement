using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PaladinAblities : MonoBehaviour
{

    //// Used to tell which stage of the attack combo the player is in
    //int attackStage;

    //// Used to tell if the player is currently in a combo
    //bool inCombo;

    //// Used to tell if the player is in cooldown
    //bool inCooldown;

    //// Used to check if the player is currently in the spin attack
    //bool inSpin;

    //// Used to know how much time the spin attack should be run for
    //float spinTimer;

    //// A float that will be used to count the time between inputs to see if the player continues their combo or restarts
    //float attackBuffer;

    //// Used to force the player to wait a little bit before they can attack again
    //float attackCooldown;

    //// The script that handles the player animations
    //PlayerAnimations animations;

    //// The trigger box in front of the player that determins thier attack range
    //BoxCollider rangeBox;

    //// A list that will be used to run through all of the game objects in the attack range when the player attacks
    //List <GameObject> objectsInRange = new List <GameObject>();

    //// Start is called before the first frame update
    //void Start()
    //{
    //    // Sets the defualt of stage 0 or not attacking
    //    attackStage = 0;

    //    // Gets the trigger from the player
    //    rangeBox = GetComponent<BoxCollider>();

    //    // Gets the animation script
    //    animations = GetComponent<PlayerAnimations>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Checks if the player is attacking
    //    if (inCombo)
    //    {
    //        // Checks if the attack buffer is higher than zero and counts it down like a timer if so
    //        if (attackBuffer > 0) 
    //        {
    //            attackBuffer = attackBuffer - Time.deltaTime;
    //        }
    //        else
    //        {
    //            attackBuffer = 0;

    //            // Since the buffer ran out the combo must be cancelled
    //            inCombo = false;

    //            // Resets the stage as the player exited the combo
    //            attackStage = 0;

    //            // Updates animations based on combo stage
    //            animations.setAttacking(attackStage);
    //        }
    //    }

    //    // Checks if the player is on a cool down
    //    if (inCooldown)
    //    {
    //        // Checks if the cool down still has value and decrements it if so, allows another attack when it runs out 
    //        if (attackCooldown > 0)
    //        {
    //            attackCooldown = attackCooldown - Time.deltaTime;
    //        }
    //        else if (attackStage == 3)
    //        {

    //            // Sets the cool down number to exact zero just in case
    //            attackCooldown = 0;

    //            // Resets attack variables so a new attack can begin
    //            inCooldown = false;
    //            attackStage = 0;

    //            // Updates animations based on combo stage
    //            animations.setAttacking(attackStage);
    //        }
    //        else
    //        {
    //            // Sets the cool down number to exact zero just in case
    //            attackCooldown = 0;

    //            // Resets the cooldown variable so a new attack can start
    //            inCooldown = false;
    //        }
    //    }

    //    // Checks if the spin attack should currently be happening
    //    if (inSpin)
    //    {
    //        // Manages the spin timer and does the correct damage and knockback calls
    //        if (spinTimer > 0) 
    //        {
    //            for (int i = 0; i < objectsInRange.Count; i++)
    //            {
    //                // Calls the small hit function from the isAttackable function
    //                objectsInRange[i].GetComponent<isAttackable>().smallHit(gameObject.GetComponent<Transform>().forward + (gameObject.GetComponent<Transform>().up * 0.5f) * 0.1f);
    //            }

    //            spinTimer = spinTimer - Time.deltaTime;
    //        }
    //        else
    //        {
    //            for (int i = 0; i < objectsInRange.Count; i++)
    //            {
    //                // Calls the small hit function from the isAttackable function
    //                objectsInRange[i].GetComponent<isAttackable>().largeHit(gameObject.GetComponent<Transform>().up);
    //            }

    //            // Disables spin attack
    //            inSpin = false;
    //        }
    //    }
    //}

    //// Called when the player presses the attack input (mouse 1 or maybe right trigger)
    //void OnAttack()
    //{
    //    // Checks if the player isn't in cooldown
    //    if (inCooldown == false)
    //    {
    //        // Checks if the attack stage is on the final stage right now and resets it if so
    //        if (attackStage == 3)
    //        {
    //            attackStage = 0;
    //        }

    //        // If statement checks the attack stage and does the corresponding attack funcitons
    //        if (attackStage == 0)
    //        {
    //            for (int i = 0; i < objectsInRange.Count; i++)
    //            {
    //                // Calls the small hit function from the isAttackable function
    //                objectsInRange[i].GetComponent<isAttackable>().largeHit(-gameObject.GetComponent<Transform>().up);
    //            }

    //            // Sets the timed attack buffer
    //            attackBuffer = 2f;

    //            // Now the combo has started the status must be set to isAttacking
    //            inCombo = true;

    //            // Increments the attack stage
    //            attackStage++;

    //            // Sets a cooldown so the player can't move to the next stage of attack until it runs out
    //            inCooldown = true;

    //            // Gives a small countdown since the swing animation is short
    //            attackCooldown = 0.25f;
    //        }
    //        else if (attackStage == 1 && inCombo == true)
    //        {
    //            for (int i = 0; i < objectsInRange.Count; i++)
    //            {
    //                // Calls the small hit function from the isAttackable function
    //                objectsInRange[i].GetComponent<isAttackable>().smallHit(gameObject.GetComponent<Transform>().forward);
    //            }

    //            // Sets the timed attack buffer
    //            attackBuffer = 2f;

    //            // Increments the attack stage
    //            attackStage++;

    //            // Sets a cooldown so the player can't move to the next stage of attack until it runs out
    //            inCooldown = true;

    //            // Gives a small countdown since the jab animation is short
    //            attackCooldown = 0.5f;
    //        }
    //        else if (attackStage == 2 && inCombo == true)
    //        {
    //            // Sets the in spin condition to true so the funny little hits from the spin can be calculated
    //            inSpin = true;

    //            // Sets the attack time as in how long the hammer spins basically
    //            spinTimer = 1f;

    //            // Ends the combo
    //            inCombo = false;

    //            // Increments the attack stage
    //            attackStage++;

    //            // Puts the player in cooldown
    //            inCooldown = true;

    //            // Sets the cooldown for the next attack
    //            attackCooldown = 1f;
    //        }

    //        // Updates animations based on combo stage
    //        animations.setAttacking(attackStage);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Checks if the object is actually an attackable object
    //    if (other.gameObject.GetComponent<isAttackable>() == true)
    //    {
    //        // Adds the game object in the range trigger to the list
    //        objectsInRange.Add(other.gameObject);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    // Removes the object that left the range from the list
    //    objectsInRange.Remove(other.gameObject);
    //}

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

    // Start is called before the first frame update
    void Start()
    {
        // Sets the defualt of stage 0 or not attacking
        attackStage = 0;

        // Gets the animation script
        animations = GetComponent<PlayerAnimations>();
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
                attackStage = 4;
                attackCooldown = 0.5f;
            }
            else if (attackStage == 4)
            {
                // Sets the cool down number to exact zero just in case
                attackCooldown = 0;

                // Resets attack variables so a new attack can begin
                inCooldown = false;
                attackStage = 0;

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
    }

    // Called when the player presses the attack input (mouse 1 or maybe right trigger)
    void OnAttack()
    {
        // Checks if the player isn't in cooldown
        if (inCooldown == false)
        {
            // Checks if the attack stage is on the final stage right now and resets it if so
            if (attackStage == 3)
            {
                attackStage = 0;
            }

            // If statement checks the attack stage and does the corresponding attack funcitons
            if (attackStage == 0) // Swing
            {
                // Sets the timed attack buffer
                attackBuffer = 2f;

                // Now the combo has started the status must be set to isAttacking
                inCombo = true;

                // Increments the attack stage
                attackStage++;

                // Sets a cooldown so the player can't move to the next stage of attack until it runs out
                inCooldown = true;

                // Gives a small countdown since the swing animation is short
                attackCooldown = 0.25f;
            }
            else if (attackStage == 1 && inCombo == true) // Jab
            {
                // Sets the timed attack buffer
                attackBuffer = 2f;

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

            // Updates animations based on combo stage
            animations.setAttacking(attackStage);
        }
    }
}
