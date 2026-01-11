using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public GameObject nextLevelTrigger;

    public EnemySpawner spawn;

    int aliveEnemy;
    int totalSpawned;
    int maxEnemyTarget;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        nextLevelTrigger.SetActive(false);
    }

    void Start()
    {
        LevelData data = LevelConfig.instance.GetCurrentData();

        maxEnemyTarget = data.maxEnemy;

        aliveEnemy = 0;
        totalSpawned = 0;


        if (spawn != null)
        {
            spawn.InitializeAndStart(maxEnemyTarget);
        }
        else
        {
            
        }
    }

    public void RegisterEnemy()
    {
        aliveEnemy++;
        totalSpawned++;
    }

    public void UnregisterEnemy()
    {
        aliveEnemy--;

        CheckClear();
    }

    void CheckClear()
    {
        if (totalSpawned >= maxEnemyTarget && aliveEnemy <= 0)
        {
            nextLevelTrigger.SetActive(true);

        }
    }
}
