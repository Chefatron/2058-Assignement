using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] PlayerAttributes playerAttributes;

    // Checks if the player collides with the key and adds it to their attributes if so
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            playerAttributes.hasKey = true;

            Destroy(gameObject);
        }
    }
}
