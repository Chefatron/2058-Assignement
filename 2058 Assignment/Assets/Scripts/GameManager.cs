using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerAttributes playerAttributes;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttributes.playerHP <= 0)
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}
