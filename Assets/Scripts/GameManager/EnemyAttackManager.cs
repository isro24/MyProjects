using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager instance;

    [Header("Attack Queue Settings")]
    public int maxAttacker = 2;

    private List<EnemyAttack> attackers = new List<EnemyAttack>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool RequestAttack(EnemyAttack enemy)
    {
        if (attackers.Contains(enemy))
            return true;

        if (attackers.Count >= maxAttacker)
            return false;

        attackers.Add(enemy);
        return true;
    }

    public void FinishAttack(EnemyAttack enemy)
    {
        if (attackers.Contains(enemy))
            attackers.Remove(enemy);
    }
}
