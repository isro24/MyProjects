using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Stats")]
    public int damage = 10;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;
    public float requestDelay = 1.0f;

    [Header("Visual & Audio")]
    public GameObject attackIndicator;
    public AudioSource attackAudio;

    PlayerHealth player;
    bool isAttacking;
    bool canRequest;

    EnemyAttackManager manager;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        manager = EnemyAttackManager.instance;

        if (attackAudio == null)
            attackAudio = GetComponent<AudioSource>();

        if (attackIndicator != null)
            attackIndicator.SetActive(false);

        StartCoroutine(EnableRequestAfterDelay());
    }

    IEnumerator EnableRequestAfterDelay()
    {
        yield return new WaitForSeconds(requestDelay);
        canRequest = true;
    }

    void Update()
    {
        if (!canRequest || isAttacking || player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= attackRange)
        {
            TryAttack();
        }
    }

    // =========================
    // DIPANGGIL OLEH EnemyAI
    // =========================
    public void TryAttack()
    {
        if (!canRequest || isAttacking) return;

        canRequest = false;

        if (manager.RequestAttack(this))
        {
            StartCoroutine(AttackRoutine());
        }
        else
        {
            // gagal → antri ulang
            StartCoroutine(EnableRequestAfterDelay());
        }
    }

    public void StopAttack()
    {
        if (!isAttacking) return;

        StopAllCoroutines();
        isAttacking = false;

        if (attackIndicator != null)
            attackIndicator.SetActive(false);

        manager.FinishAttack(this);
        StartCoroutine(EnableRequestAfterDelay());
    }

    // =========================
    // ATTACK LOGIC ASLI
    // =========================
    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (attackIndicator != null)
            attackIndicator.SetActive(true);

        yield return new WaitForSeconds(0.5f); // wind-up

        if (attackAudio != null)
            attackAudio.Play();

        if (player != null)
            player.TakeDamage(damage);

        yield return new WaitForSeconds(attackCooldown);

        if (attackIndicator != null)
            attackIndicator.SetActive(false);

        manager.FinishAttack(this);

        isAttacking = false;
        StartCoroutine(EnableRequestAfterDelay());
    }

    void OnDestroy()
    {
        if (manager != null)
            manager.FinishAttack(this);
    }
}
