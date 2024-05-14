using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Used to tell when the player is dead
    [SerializeField] PlayerAttributes playerAttributes;

    // Used get various important data from the level
    [SerializeField] DungeonData dungeonData;

    // The enemy to spawn
    [SerializeField] List<GameObject> enemies;

    // Used to spawn the player in the right place
    [SerializeField] GameObject player;

    // Exit door of the level
    [SerializeField] BoxCollider exit;

    // Used for setting the target of the camera to the spawned player
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    // Used to space out enemy spawns
    float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        // Sets defualt spawn interval
        spawnInterval = 1f;

        // Spawns player in point grabbed from dungeon data and sets the camera to follow it
        virtualCamera.Follow = Instantiate(player, dungeonData.playerSpawn, Quaternion.identity).transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if player is killed
        if (playerAttributes.playerHP <= 0)
        {
            playerAttributes.resetValues();

            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Checks spawn timer and spawns and enemy if it runs out, then resets it
        if (spawnInterval > 0)
        {
            spawnInterval = spawnInterval - Time.deltaTime;
        }
        else
        {
            spawnEnemy();

            spawnInterval = Random.Range(1f, 10f);
        }
    }

    void spawnEnemy()
    {
        // Spawns an enemy at a random one of the spawn points
        Instantiate(enemies[Random.Range(0, enemies.Count)], dungeonData.spawnPoints[Random.Range(0, dungeonData.spawnPoints.Count)], Quaternion.identity);
    }

    // Is called when loading an new level 
    public void nextScene()
    {
        LoadScene(Random.Range(2, SceneManager.sceneCount));
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
