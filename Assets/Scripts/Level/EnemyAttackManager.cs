using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager instance;

    public int maxAttacker = 2;
    List<EnemyAttack> attackers = new List<EnemyAttack>();

    void Awake()
    {
        instance = this;
    }

    public bool RequestAttack(EnemyAttack enemy)
    {
        if (attackers.Count >= maxAttacker)
            return false;

        if (!attackers.Contains(enemy))
            attackers.Add(enemy);

        return true;
    }

    public void FinishAttack(EnemyAttack enemy)
    {
        if (attackers.Contains(enemy))
            attackers.Remove(enemy);
    }
}
