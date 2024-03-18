using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaladinAblities : MonoBehaviour
{
    // Used to tell which stage of the attack combo the player is in
    int attackStage;

    // Used to tell if the player is currently in a combo
    bool inCombo;

    // A float that will be used to count the time between inputs to see if the player continues their combo or restarts
    float attackBuffer;

    // The script that handles the player animations
    PlayerAnimations animations;

    // The trigger box in front of the player that determins thier attack range
    BoxCollider rangeBox;

    // A list that will be used to run through all of the game objects in the attack range when the player attacks
    List <GameObject> objectsInRange = new List <GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Sets the defualt of stage 0 or not attacking
        attackStage = 0;

        // Gets the trigger from the player
        rangeBox = GetComponent<BoxCollider>();

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

                attackStage = 0;

                animations.setAttacking(attackStage);
            }
        }
    }

    // Called when the player presses the attack input (mouse 1 or maybe right trigger)
    void OnAttack()
    {
        // If statement checks the attack stage and does the corresponding attack funcitons
        if (attackStage == 0) 
        {
            for (int i = 0; i < objectsInRange.Count; i++) 
            {
                // Calls the small hit function from the isAttackable function
                objectsInRange[i].GetComponent<isAttackable>().smallHit(-gameObject.GetComponent<Transform>().up);
            }

            // Sets the timed attack buffer
            attackBuffer = 2;

            // Now the combo has started the status must be set to isAttacking
            inCombo = true;
        }
        else if (attackStage == 1 && inCombo == true) 
        {
            for (int i = 0; i < objectsInRange.Count; i++)
            {
                // Calls the small hit function from the isAttackable function
                objectsInRange[i].GetComponent<isAttackable>().smallHit(gameObject.GetComponent<Transform>().forward);                
            }

            // Sets the timed attack buffer
            attackBuffer = 2;
        }
        else if (attackStage == 2 && inCombo == true)
        {
            for (int i = 0; i < objectsInRange.Count; i++)
            {
                // Calls the large hit function from the isAttackable function
                objectsInRange[i].GetComponent<isAttackable>().largeHit(gameObject.GetComponent<Transform>().up);
            }

            // Ends the combo
            inCombo = false;
        }

        // Increments the attack stage or resets it based on position in combo
        if (attackStage < 2)
        {
            attackStage++;
        }
        else
        {
            attackStage = 0;
        }

        // Updates animations based on combo stage
        animations.setAttacking(attackStage);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the object is actually an attackable object
        if (other.gameObject.GetComponent<isAttackable>() == true)
        {
            // Adds the game object in the range trigger to the list
            objectsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Removes the object that left the range from the list
        objectsInRange.Remove(other.gameObject);
    }
}
