using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] int MaxNumOfUnits = 5;
    public int currnetNumOfUnits = 0;
    PlayerHealth player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        InvokeRepeating("Spawn", spawnDelay, spawnDelay);
    }

	void Spawn()
    {
        if (currnetNumOfUnits >= MaxNumOfUnits || player.currentHealth <= 0)
            return;

        int spawnPointIndex = Random.Range(0, spawnPoints.Count);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(EnemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        currnetNumOfUnits++;
    }
}
