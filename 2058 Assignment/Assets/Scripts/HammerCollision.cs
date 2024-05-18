using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class HammerCollision : MonoBehaviour
{
    // The script that takes in the user for input and does the animation control for attacking
    PaladinAblities paladinAttacks;

    // The player attributes for a camera shake when attacking
    [SerializeField] PlayerAttributes playerAttributes;

    // To play the hammer hit sound
    [SerializeField] AudioManager audioManager;

    // The collider surrounding the hammer
    BoxCollider hammerCollider;

    // Saves the current stage so I can compare it to the attack stage in the other script and see if it changes
    int currentStage;

    // List of objects in collision
    List<isAttackable> objectsInRange = new List<isAttackable>();

    // Used to play a particle on connect with an enemy
    ParticleSystem hitParticles;

    // Start is called before the first frame update
    void Start()
    {
        paladinAttacks = GetComponentInParent<PaladinAblities>();

        currentStage = paladinAttacks.attackStage;
        
        hammerCollider = GetComponent<BoxCollider>();

        hitParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (objectsInRange.Count > 0) 
        { 
            // Checks if the attack stage updated so all of the 
            if (currentStage != paladinAttacks.attackStage)
            {
                for (int i = 0; i < objectsInRange.Count; i++)
                {
                    objectsInRange[i].wasHit = false;

                    if (objectsInRange[i] == null) 
                    { 
                        objectsInRange.RemoveAt(i);
                    }
                }

                currentStage = paladinAttacks.attackStage;
            }

            if (currentStage == 1)
            {
                for(int i = 0; i < objectsInRange.Count; i++) 
                {
                    if (objectsInRange[i].wasHit == false)
                    {
                        objectsInRange[i].largeHit(transform.forward);

                        playerAttributes.shakeTimer = 0.3f;

                        objectsInRange[i].wasHit = true;
                    }
                }
            }
            else if (currentStage == 2)
            {
                for (int i = 0; i < objectsInRange.Count; i++)
                {
                    if (objectsInRange[i].wasHit == false)
                    {
                        objectsInRange[i].mediumHit(transform.up);

                        playerAttributes.shakeTimer = 0.2f;

                        objectsInRange[i].wasHit = true;
                    }
                }
            }
            else if (currentStage == 3)
            {
                for (int i = 0; i < objectsInRange.Count; i++)
                {
                    objectsInRange[i].smallHit(transform.forward + (transform.up * 0.5f));
                }
            }
            else if (currentStage == 4)
            {
                for (int i = 0; i < objectsInRange.Count; i++)
                {
                    if (objectsInRange[i].wasHit == false)
                    {
                        objectsInRange[i].largeHit(paladinAttacks.transform.up);

                        playerAttributes.shakeTimer = 0.3f;

                        objectsInRange[i].wasHit = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the object is actually an attackable object
        if (other.gameObject.GetComponent<isAttackable>() == true)
        {
            // Adds the game object in the range trigger to the list
            objectsInRange.Add(other.GetComponent<isAttackable>());
        }
        else if (other.gameObject.CompareTag("Architecture") && paladinAttacks.attackStage == 1 && other.transform.position.y < transform.position.y) // Checks if the hammer hits the ground to do the dust effect
        {
            hitParticles.Play();

            audioManager.playSound("Hammer_Hit");

            playerAttributes.shakeTimer = 0.3f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if the object is actually an attackable object
        if (other.gameObject.GetComponent<isAttackable>() == true)
        {
            // Sets it so the object was no longer "hit"
            other.GetComponent<isAttackable>().wasHit = false;

            // Remove the game object in the range trigger to the list
            objectsInRange.Remove(other.GetComponent<isAttackable>());
        }
    }
}
