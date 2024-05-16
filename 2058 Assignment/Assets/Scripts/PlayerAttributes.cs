using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PlayerAttributes : ScriptableObject
{
    // Player health
    public int playerHP;

    // Used to adjust the movment speed of player when attacking
    public float playerSpeedMultiplier;

    // Used by enemies to set the players positon to their ai destinatio
    public Vector3 playerPosition;

    // USed to tell if the player is healing by various scripts
    public bool isSpecial;

    // Used by the door to tell if they have key
    public bool hasKey;

    // Used by the need key sprite to tell if the player is close to a door
    public bool needKey;

    // Used by game manager to shake screen for however long the timer is set to
    public float shakeTimer;

    public void resetValues()
    {
        playerHP = 10;

        playerSpeedMultiplier = 1;

        playerPosition = Vector3.zero;

        isSpecial = false;

        hasKey = false;

        needKey = false;

        shakeTimer = 0f;
    }
}
