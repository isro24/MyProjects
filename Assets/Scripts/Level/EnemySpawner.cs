using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int maxEnemy = 20;          // batas total enemy
    public int startAmount = 2;        // wave awal
    public float spawnInterval = 2f;   // jeda antar wave
    public float spawnRadius = 15f;

    int spawnedCount = 0;
    int currentWaveAmount;

    void Start()
    {
        currentWaveAmount = startAmount;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (spawnedCount < maxEnemy)
        {
            int spawnThisWave = Mathf.Min(currentWaveAmount, maxEnemy - spawnedCount);

            for (int i = 0; i < spawnThisWave; i++)
            {
                Vector3 pos = transform.position +
                    new Vector3(
                        Random.Range(-spawnRadius, spawnRadius),
                        0,
                        Random.Range(-spawnRadius, spawnRadius)
                    );

                Instantiate(enemyPrefab, pos, Quaternion.identity);
            }

            spawnedCount += spawnThisWave;
            currentWaveAmount *= 2;   // berlipat dua

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
