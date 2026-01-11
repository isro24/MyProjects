using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int damage = 25;
    public float attackCooldown = 2.5f;
    public float attackRange = 2.5f;

    public GameObject attackIndicator;
    public AudioSource attackAudio;

    PlayerHealth player;
    bool isAttacking;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();

        if (attackIndicator != null)
            attackIndicator.SetActive(false);
    }

    void Update()
    {
        if (isAttacking || player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= attackRange)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (attackIndicator != null)
            attackIndicator.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        if (attackAudio != null)
            attackAudio.Play();

        if (player != null)
            player.TakeDamage(damage);

        if (attackIndicator != null)
            attackIndicator.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
