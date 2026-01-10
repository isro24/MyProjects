using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 25;

    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void EnableHit()
    {
        col.enabled = true;
        Debug.Log("[Weapon] HIT ENABLED");
    }

    public void DisableHit()
    {
        col.enabled = false;
        Debug.Log("[Weapon] HIT DISABLED");
    }

    void OnTriggerEnter(Collider other)
    {
        if (!col.enabled) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("SWORD HIT ENEMY");
            }
        }
    }
}
