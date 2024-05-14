using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerAttributes playerAttributes;

    [SerializeField] Image healthBar;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerAttributes.playerHP / 10);

        // Sets the health bar fill amount to the the player HP 
        healthBar.fillAmount = playerAttributes.playerHP / 10f;
    }
}
