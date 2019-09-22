using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> listOfEnemies;

    public List<Transform> spawnLimits;

    private float topLimit;
    private float rightLimit;
    private float botLimit;
    private float leftLimit;

    private LevelManager levelManager;


    public int amountOfKillsNecessary;

    public int amountOfEnemiesKilled;

    GameInitializer gameInitializer;

    private void Awake()
    {
        SetSpawnLimits();
        GlobalEvents.OnWaveStart += StartSpawningEnemies;
        levelManager = FindObjectOfType<LevelManager>();
        GlobalEvents.OnEnemyDeath += EnemyDeath;
        gameInitializer = FindObjectOfType<GameInitializer>();
    }


    private void OnDestroy()
    {
        GlobalEvents.OnEnemyDeath -= EnemyDeath;
        GlobalEvents.OnWaveStart -= StartSpawningEnemies;
    }

   

    private void SetSpawnLimits()
    {
        topLimit = spawnLimits[0].position.y;
        rightLimit = spawnLimits[1].position.x;
        botLimit = spawnLimits[2].position.y;
        leftLimit = spawnLimits[3].position.x;
    }

    public Vector2 GetRandomSpawnTransform()
    {

        float randomValue = UnityEngine.Random.Range(0f, 1f);
        // Top
        if (randomValue < 0.25f) {
            return new Vector2(UnityEngine.Random.Range(leftLimit, rightLimit),topLimit);
        }
        // Right
        else if (randomValue < 0.5f) {
            return new Vector2(rightLimit, UnityEngine.Random.Range(botLimit, topLimit));
        }
        // Bot
        else if (randomValue < 0.75f) {
            return new Vector2(UnityEngine.Random.Range(leftLimit, rightLimit), botLimit);
        }
        // Left
        else {
            return new Vector2(leftLimit, UnityEngine.Random.Range(botLimit, topLimit));
        }
    }

    public void StartSpawningEnemies(object sender, System.EventArgs e)
    {
        amountOfEnemiesKilled = 0;
        StartCoroutine(StartSpawningWave());
    }

    private float GetLevelSpawnTimer(int currentLevel)
    {
        
        if(currentLevel < 5) {
            return 5;
        }
        else if (currentLevel < 10) {
            return 4.5f;
        }else if (currentLevel < 20) {
            return 3;
        }
        else {
            return 2;
        }

    
    }

    IEnumerator StartSpawningWave()
    {
        amountOfKillsNecessary = GetAmountOfEnemiesOfLevel();
        for (int i = 0; i < GetAmountOfEnemiesOfLevel(); i++) {

            int currentLevel = levelManager.GetCurrentLevel();
            GameObject prefab = GetEnemyPrefab(currentLevel);
            SpawnEnemy(prefab);
            yield return new WaitForSeconds(GetLevelSpawnTimer(currentLevel));
        }
    }

    public int GetAmountOfEnemiesOfLevel()
    {
        
        return levelManager.GetCurrentLevel() + 2;
    }

    private GameObject GetEnemyPrefab(int currentLevel)
    {

        float randomValue = UnityEngine.Random.Range(0f, 1f);

        GameObject prefab;

        if (randomValue < 0.2) {
            prefab = listOfEnemies[1];
        }
        else {
            prefab = listOfEnemies[0];
        }
        
        return prefab;
    }

    private void EnemyDeath(object sender, System.EventArgs e)
    {
        amountOfEnemiesKilled++;
        if(amountOfEnemiesKilled == GetAmountOfEnemiesOfLevel()) {
            gameInitializer.EndWave();
        }
    }

    private void SpawnEnemy(GameObject prefab)
    {
        GameObject newEnemy = Instantiate(prefab, GetRandomSpawnTransform(), Quaternion.identity);
        BaseEnemy baseEnemy = newEnemy.GetComponent<BaseEnemy>();
        baseEnemy.StartActing();
    }
}