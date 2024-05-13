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

    // Used to space out enemy spawns
    float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttributes.playerHP <= 0)
        {
            playerAttributes.resetValues();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

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
        Instantiate(enemies[Random.Range(0, 2)], dungeonData.spawnPoints[Random.Range(0, 6)], Quaternion.identity);
    }
}
