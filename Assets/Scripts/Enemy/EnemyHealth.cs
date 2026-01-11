using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Stats")]
    public int maxHP;
    int currentHP;

    public int CurrentHP => currentHP;

    EnemyAttack enemyAttack;

    void Start()
    {
        if (LevelConfig.instance != null)
        {
            LevelData data = LevelConfig.instance.GetCurrentData();
            maxHP = data.enemyHP;
        }
        else
        {
            maxHP = 50;
        }
        currentHP = maxHP;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        Debug.Log($"Enemy Hit! HP: {currentHP}/{maxHP}");

        if (currentHP <= 0)
            Die();
    }

    public bool IsLowHP()
    {
        return currentHP <= maxHP * 0.3f;
    }


    void Die()
    {
        if (EnemyManager.instance != null)
            EnemyManager.instance.UnregisterEnemy();

        Destroy(gameObject);
    }

}
