using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [SerializeField] private int maxEnemy;
    public int startAmount = 2;     
    public float spawnInterval = 2f;
    public float spawnRadius = 15f;

    int spawnedCount = 0;
    int currentWaveAmount;

    public void InitializeAndStart(int amountFromConfig)
    {
        maxEnemy = amountFromConfig;

        spawnedCount = 0;
        currentWaveAmount = startAmount;

        Debug.Log($"[Spawner] Menerima perintah spawn: {maxEnemy} musuh.");

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (spawnedCount < maxEnemy)
        {
            int spawnThisWave = Mathf.Min(currentWaveAmount, maxEnemy - spawnedCount);

            for (int i = 0; i < spawnThisWave; i++)
            {
                Vector3 randomXZ = transform.position + new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    10f,
                    Random.Range(-spawnRadius, spawnRadius)
                );

                if (Physics.Raycast(randomXZ, Vector3.down, out RaycastHit hit, 50f))
                {
                    Vector3 spawnPos = hit.point;
                    spawnPos.y += 1f;

                    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                    EnemyManager.instance.RegisterEnemy();

                    EnemyAI ai = enemy.GetComponent<EnemyAI>();
                    if (ai != null)
                    {
                        ai.formationIndex = spawnedCount % ai.formationTotal;
                    }

                    spawnedCount++;
                }
            }

            currentWaveAmount *= 2;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
