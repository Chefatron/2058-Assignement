using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] PlayerAttributes playerAttributes;

    // The particles that play when the door is opened
    ParticleSystem breakParticles;

    // The animation to play when the door is opening
    Animation openAnimation;

    // Start is called before the first frame update
    void Start()
    {
        // Get the particles
        breakParticles = GetComponent<ParticleSystem>();

        // Get the animation
        openAnimation = GetComponentInParent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerAttributes.hasKey == true && !other.isTrigger)
        {
            // Plays the particles
            breakParticles.Play();

            // Plays the animation
            openAnimation.Play();

            // Shakes the camera for the door opening process
            playerAttributes.shakeTimer = 3f;

            // Destorys the door after 3 seconds
            Invoke("openDoor", 3f);
        }
        else if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            playerAttributes.needKey = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            playerAttributes.needKey = false;
        }
    }

    void openDoor()
    {
        // Removes the key
        playerAttributes.hasKey = false;

        Destroy(gameObject);
    }
}
