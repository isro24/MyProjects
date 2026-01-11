using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHP = 1000;
    int currentHP;

    public System.Action<int, int> OnHealthChanged;

    void Start()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log("[BossHealth] HP = " + currentHP);
        OnHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("BOSS MATI");
        Destroy(gameObject);
    }
}
