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
            Debug.LogError("[Weapon] ❌ Collider TIDAK DITEMUKAN di Sword");
        }
        else
        {
            col.enabled = false;
            Debug.Log("[Weapon] ✅ Collider ditemukan & DISABLE saat start");
        }
    }

    public void EnableHit()
    {
        if (col == null) return;

        col.enabled = true;
        Debug.Log("[Weapon] 🟢 HIT ENABLED");
    }

    public void DisableHit()
    {
        if (col == null) return;

        col.enabled = false;
        Debug.Log("[Weapon] 🔴 HIT DISABLED");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("[Weapon] ⚠️ Trigger ENTER dengan: " + other.name +
                  " | TAG = " + other.tag);

        if (!col.enabled)
        {
            Debug.Log("[Weapon] ⛔ Collider DISABLE, abaikan hit");
            return;
        }

        // ================= ENEMY =================
        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
        if (enemy != null)
        {
            Debug.Log("[Weapon] 🎯 KENA ENEMY");
            enemy.TakeDamage(damage);
            return;
        }

        // ================= BOSS =================
        BossHealth boss = other.GetComponentInParent<BossHealth>();
        if (boss != null)
        {
            Debug.Log("[Weapon] 🟥 KENA BOSS");
            boss.TakeDamage(damage);
            Debug.Log("[Weapon] 💥 DAMAGE ke BOSS = " + damage);
            return;
        }

        Debug.Log("[Weapon] ℹ️ Collider bukan Enemy / Boss");
    }
}
