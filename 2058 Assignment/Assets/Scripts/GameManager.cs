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

    // Used to space out enemy spawns
    float spawnInterval;

    // The camera that follows the player
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    // THe camera
    [SerializeField] Camera mainCam;

    // Used to adjust the shake of the camera
    CinemachineBasicMultiChannelPerlin noise;

    // Used to get the bounding area that the camera can see
    Plane[] planes = new Plane[6];

    // Used to temporarily hold the current enemy being spawned for some checks
    GameObject tempEnemy;

    // Used to check the level thats gonna be loaded so it doesn't load the same one twice
    int sceneToLoad;

    // The audio manager scriptable object with all the data for playing the music
    [SerializeField] AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // Sets defualt spawn interval
        spawnInterval = 1f;

        // Spawns player in point grabbed from dungeon data and sets the camera to follow it
        virtualCamera.Follow = Instantiate(player, dungeonData.playerSpawn, Quaternion.identity).transform;

        // Get the noise component from the cinemachine camera
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Reset both key varaaibles
        playerAttributes.needKey = false;
        playerAttributes.hasKey = false;

        // Set the res of the screen for the low quality look
        Screen.SetResolution(480, 270, true);

        // Instantiates the audio instance obejct
        audioManager.CreateManager();

        // Plays the music
        audioManager.playSound("Music");

        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if player is killed
        if (playerAttributes.playerHP <= 0)
        {
            playerAttributes.resetValues();

            LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }

        // Checks spawn timer and spawns and enemy if it runs out, then resets it
        if (spawnInterval > 0)
        {
            spawnInterval = spawnInterval - Time.deltaTime;
        }
        else
        {
            spawnEnemy();

            spawnInterval = Random.Range(1f, 5f);
        }

        // Checks if the shaketimer is higher than 0 and lowers it while shaking the camera if so
        if (playerAttributes.shakeTimer > 0)
        {
            noise.m_AmplitudeGain = 1f;
            noise.m_FrequencyGain = 1f;

            playerAttributes.shakeTimer = playerAttributes.shakeTimer - Time.deltaTime;
        }
        else
        {
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;

            playerAttributes.shakeTimer = 0f;
        }
    }

    void spawnEnemy()
    {
        // Spawns an enemy at a random one of the spawn points
        tempEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], dungeonData.spawnPoints[Random.Range(0, dungeonData.spawnPoints.Count)], Quaternion.identity);

        planes = GeometryUtility.CalculateFrustumPlanes(mainCam);

        // Checks if the enemy is visible when being spawned
        if (GeometryUtility.TestPlanesAABB(planes, tempEnemy.GetComponent<CapsuleCollider>().bounds))
        {
            // Destroys the enemy
            Destroy(tempEnemy);

            // Nulls out the variable
            tempEnemy = null;

            // Spawns another
            spawnEnemy();
        }
    }

    // Is called when loading an new level 
    public void nextScene()
    {
        // Removes any key from the player
        playerAttributes.hasKey = false;

        sceneToLoad = Random.Range(2, 6);
        
        // Checks if scene to load matched the current scene and either adjusts it by 1 up or down depending or just loads the scene if not
        if (sceneToLoad == SceneManager.GetActiveScene().buildIndex && sceneToLoad != 2)
        {
            LoadScene(sceneToLoad - 1);
        }
        else if (sceneToLoad == SceneManager.GetActiveScene().buildIndex)
        {
            LoadScene(sceneToLoad + 1);
        }
        else
        {
            LoadScene(sceneToLoad);
        }
        
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
        playerAttributes.resetValues();

        LoadScene(0);
    }
}
