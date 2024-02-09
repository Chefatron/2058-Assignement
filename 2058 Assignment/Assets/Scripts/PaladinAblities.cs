using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAblities : MonoBehaviour
{
    // Used to tell which stage of the attack combo the player is in
    int attackStage;

    // Used to tell if the player is currently in a combo
    bool isAttacking;

    // The script that handles the player animations
    PlayerAnimations animations;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the defualt of stage 0 or not attacking
        attackStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAttack()
    {
        attackStage++;

        
    }
}
