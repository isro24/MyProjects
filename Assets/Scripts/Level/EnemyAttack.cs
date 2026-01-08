using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;

    PlayerHealth player;
    bool isAttacking;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (isAttacking || player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= attackRange)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (EnemyAttackManager.instance.RequestAttack(this))
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // 🔴 nanti di sini bisa ditambah tanda menyerang
        yield return new WaitForSeconds(0.5f);

        player.TakeDamage(damage);

        yield return new WaitForSeconds(attackCooldown);

        EnemyAttackManager.instance.FinishAttack(this);
        isAttacking = false;
    }
}
