using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    GameObject pausePanel;

    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel = GameObject.Find("Pause Panel");

        pausePanel.SetActive(false);
    }

    void OnPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;

            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;

            pausePanel.SetActive(false);   
        }
    }
}
