using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemySpawner;
    public bool isSpawnerActive;
    public int numberOfEnemyToSpawn;
    public float spawnRate;
    private float spawnRateCounter;
    public float howLongUntilActivate;
    private bool waitEnough = false;

    private int spawnCounter = 0;

    public EnemyController[] enemiesSpawnable;
    public SpriteRenderer theBody;
    public Transform spawnPoint;


    public bool activeOnSight, activeOnTouch, activeOnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnRateCounter = 0;
        if (activeOnTouch)
        {
            theBody.enabled = false;
            isSpawnerActive = false;
        }
        if (activeOnTimer)
        {
            theBody.enabled = false;
            isSpawnerActive = true;

        }
        if (activeOnSight)
        {
            isSpawnerActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (activeOnSight)
        {
            enemySpawner.SetActive(true);
            if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
            {

                SpawnEnemies();
            }
        }

        if (activeOnTimer)
        {
            if (howLongUntilActivate > 0)
            {
                howLongUntilActivate -= Time.deltaTime;
            }
            if (howLongUntilActivate <= 0)
            {
                //enemySpawner.SetActive(true);
                SpawnEnemies();
            }
        }

        if (activeOnTouch && isSpawnerActive)
        {
            SpawnEnemies();
        }


    }

    public void SpawnEnemies()
    {
        if (isSpawnerActive)
        {
            if (!waitEnough)
            {
                spawnRateCounter += Time.deltaTime;
            }
            if (!waitEnough && spawnRateCounter >= spawnRate)
            {
                int enemyToSpawn = Random.Range(0, enemiesSpawnable.Length);
                Instantiate(enemiesSpawnable[enemyToSpawn], spawnPoint.position, spawnPoint.rotation);
                spawnCounter++;
                spawnRateCounter = 0;
                if (spawnCounter >= numberOfEnemyToSpawn)
                {
                    isSpawnerActive = false;
                    enemySpawner.SetActive(false);
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && activeOnTouch)
        {
            isSpawnerActive = true;
        }
    }
}

