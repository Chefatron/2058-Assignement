using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCollision : MonoBehaviour
{
    PaladinAblities paladin;

    // Start is called before the first frame update
    void Start()
    {
        paladin = GetComponentInParent<PaladinAblities>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
