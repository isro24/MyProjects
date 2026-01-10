using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager instance;

    [Header("Attack Queue Settings")]
    public int maxAttacker = 2;

    // LIST musuh yang SEDANG menyerang
    private List<EnemyAttack> attackers = new List<EnemyAttack>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Dipanggil Enemy sebelum menyerang
    /// </summary>
    public bool RequestAttack(EnemyAttack enemy)
    {
        // Jika sudah menyerang, izinkan
        if (attackers.Contains(enemy))
            return true;

        // Jika slot penuh → tolak
        if (attackers.Count >= maxAttacker)
            return false;

        attackers.Add(enemy);
        return true;
    }

    /// <summary>
    /// Dipanggil saat Enemy selesai menyerang / mati
    /// </summary>
    public void FinishAttack(EnemyAttack enemy)
    {
        if (attackers.Contains(enemy))
            attackers.Remove(enemy);
    }
}
