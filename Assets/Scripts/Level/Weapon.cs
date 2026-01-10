using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 25;
    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();

        if (col == null)
        {
            Debug.LogError("[Weapon] COLLIDER TIDAK ADA!");
            return;
        }

        col.isTrigger = true;
        col.enabled = false;

        Debug.Log("[Weapon] Collider ditemukan & DISABLE");
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[Weapon] KENA: " + other.name);

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("[Weapon] DAMAGE MASUK: " + damage);
            }
            else
            {
                Debug.Log("[Weapon] EnemyHealth TIDAK ADA");
            }
        }
    }
}
