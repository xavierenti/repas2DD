using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Camera camera;
    private Vector2 cameraXBounds = Vector2.zero;
    private float enemySpriteWidth;
    private SpriteRenderer enemySpriteRenderer;


    [Header("Enemy")]
    [SerializeField]private float enemiMinTiemr;
    [SerializeField]private float enemyMaxTimer;
    private float enemyTimeSpawn;
    private float currentenemySpawn = 0f;
    [SerializeField] GameObject enemyPrefab;

    [Header("PowerUp")]
    [SerializeField] private float powerUpMinTiemr;
    [SerializeField] private float PowerUpMaxTimer;
    private float PowerUpTimeSpawn;
    private float currentPowerUpSpawn = 0f;

    [Header("Asteroid")]
    [SerializeField] private float AsteroidMinTiemr;
    [SerializeField] private float AsteroidMaxTimer;
    private float AsteroidTimeSpawn;
    private float currentAsteroidSpawn = 0f;



    // Start is called before the first frame update
    void Awake()
    {
        enemyTimeSpawn = Random.Range(enemiMinTiemr, enemyMaxTimer);
        PowerUpTimeSpawn = Random.Range(powerUpMinTiemr, PowerUpMaxTimer);
        AsteroidTimeSpawn = Random.Range(AsteroidMinTiemr, AsteroidMaxTimer);
    }

    private void Start()
    {
        enemySpriteWidth = enemySpriteRenderer.bounds.size.x * 0.5f;

        cameraXBounds.x = camera.ViewportToWorldPoint(new Vector2(0, 1)).x + enemySpriteWidth;
        cameraXBounds.y = camera.ViewportToWorldPoint(new Vector2(1, 1)).x - enemySpriteWidth;
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawn();
        powerUpSpawn();
        asteroidSpawn();
    }
    private void enemySpawn()
    {
        currentenemySpawn += Time.deltaTime;
        if (currentenemySpawn >= enemyTimeSpawn)
        {
            currentenemySpawn = 0f;
            enemyTimeSpawn = Random.Range(enemiMinTiemr, enemyMaxTimer);
            Debug.Log("enemySpawn");
            createenemy();
        }
    }

    private void powerUpSpawn()
    {
        currentPowerUpSpawn += Time.deltaTime;
        if (currentPowerUpSpawn >= PowerUpTimeSpawn)
        {
            currentPowerUpSpawn = 0f;
            PowerUpTimeSpawn = Random.Range(powerUpMinTiemr, PowerUpMaxTimer);
            Debug.Log("PowerUpSpawn");
        }
    }

    private void asteroidSpawn()
    {
        currentAsteroidSpawn += Time.deltaTime;
        if (currentAsteroidSpawn >= AsteroidTimeSpawn)
        {
            currentAsteroidSpawn = 0f;
            AsteroidTimeSpawn = Random.Range(AsteroidMinTiemr, AsteroidMaxTimer);
            Debug.Log("AsteroidSpawn");
        }
    }
    private void createenemy()
    {
        float spawnPositionX = Random.Range(cameraXBounds.x, cameraXBounds.y);
        Vector2 spawnPosition = new Vector2(spawnPositionX, 6f);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    }
}
