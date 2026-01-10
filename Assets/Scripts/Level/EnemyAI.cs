using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float chaseDistance = 8f;
    public float stopDistance = 1.5f;
    public float moveSpeed = 2f;

    public float fleeDistance = 10f;      // jarak aman saat takut
    public float lowHPPercent = 0.3f;     // 30% HP → takut
    public int formationIndex;
    public int formationTotal = 6;
    public float circleRadius = 2.5f;


    Transform player;
    EnemyHealth health;
    Rigidbody rb;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        health = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null || health == null) return;

        float hpPercent = (float)health.CurrentHP / health.maxHP;

        if (hpPercent <= lowHPPercent)
        {
            Flee();
        }
        else
        {
            Chase();
        }
    }

    void Chase()
    {
        Vector3 targetPos = EnemyPositioning.GetCirclePosition(
            player,
            formationIndex,
            formationTotal,
            circleRadius
        );

        Vector3 dir = targetPos - transform.position;
        dir.y = 0;

        rb.MovePosition(rb.position + dir.normalized * moveSpeed * Time.fixedDeltaTime);
        transform.forward = dir;
    }


    void Flee()
    {
        Vector3 dir = transform.position - player.position;
        dir.y = 0;

        rb.MovePosition(rb.position + dir.normalized * moveSpeed * Time.fixedDeltaTime);
        transform.forward = dir;
    }
}
