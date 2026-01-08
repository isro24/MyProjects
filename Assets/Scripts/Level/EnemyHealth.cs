using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 50;
    int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            Destroy(gameObject);
    }
}
