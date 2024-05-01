using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private List<GameObject> spawnPoints = null;
    [SerializeField] private int count = 20;
    [SerializeField] private float minDelay = 0.8f, maxDelay = 1.5f, minDistance = 10, maxDistance = 20;
    private bool isSpawning = false, canSpawn = true;

    IEnumerator SpawnCoroutine()
    {
        while (count > 0 && isSpawning)
        {
            count--;
            var randomIndex = Random.Range(0, spawnPoints.Count);

            var randomOffset = Random.insideUnitCircle;
            var spawnPoint = spawnPoints[randomIndex].transform.position + (Vector3)randomOffset;

            SpawnEnemy(spawnPoint);

            var randomTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomTime);
        }
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    private void DetectPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, maxDistance, LayerMask.GetMask("Player"));
        if (colliders.Length > 0 && canSpawn)
        {
            isSpawning = true;
            StartCoroutine(DelayNextSpawnCoroutine());
        }
        else
        {
            isSpawning = false;
        }
    }

    IEnumerator DelayNextSpawnCoroutine()
    {
        canSpawn = false;
        StartCoroutine(SpawnCoroutine());
        isSpawning = false;
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        canSpawn = true;
    }

    private void Update()
    {
        DetectPlayer();
    }
}
