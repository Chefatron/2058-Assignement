using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamBehaviour : MonoBehaviour
{
    [SerializeField] PlayerAttributes playerAttributes;

    CinemachineVirtualCamera cam;

    Transform player;

    Transform aim;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;

            aim = GameObject.Find("Aim Object").transform;
        }

        if (playerAttributes.isSpecial == false)
        {
            cam.Follow = player;
        }
        else 
        {
            cam.Follow = aim;
        }
    }
}
