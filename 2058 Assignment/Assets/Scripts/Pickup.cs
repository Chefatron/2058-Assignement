using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // For telling that the player has a key
    [SerializeField] PlayerAttributes playerAttributes;

    // For playing the sound when the player gets the key
    [SerializeField] AudioManager audioManager;

    // Checks if the player collides with the key and adds it to their attributes if so
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            audioManager.playSound("Get_Key");

            playerAttributes.hasKey = true;

            Destroy(gameObject);
        }
    }
}
