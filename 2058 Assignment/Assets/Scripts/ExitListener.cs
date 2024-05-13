using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitListener : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.nextScene();
        }
    }
}
