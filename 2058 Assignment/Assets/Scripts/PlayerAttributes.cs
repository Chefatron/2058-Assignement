using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PlayerAttributes : ScriptableObject
{
    public int playerHP;

    public float playerSpeedMultiplier;

    public Vector3 playerPosition;

    public bool isSpecial;

    public bool hasKey;

    public bool needKey;

    public void resetValues()
    {
        playerHP = 10;

        playerSpeedMultiplier = 1;

        playerPosition = Vector3.zero;

        isSpecial = false;

        hasKey = false;

        needKey = false;
    }
}
