using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Restarts the game for the player
    public void replayGame()
    {
        LoadScene(Random.Range(2, 6));
    }

    // Loads the loading scene and starts loading the desired scene
    public void LoadScene(int sceneIndex)
    {
        // Loads the loading scene
        SceneManager.LoadSceneAsync(1);

        // Starts the loading of the indexed scene at the same time 
        StartCoroutine(loadAsyncScene(sceneIndex));
    }

    // Used to load the desired scene while the loading scene plays
    IEnumerator loadAsyncScene(int sceneIndex)
    {
        // Loads the scene in the background
        AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(sceneIndex);

        // Checks if the scene is done loading
        while (asyncLoading != null)
        {
            yield return null;
        }
    }

    // Quits to main menu
    public void quitToMenu()
    {
        LoadScene(0);
    }
}
