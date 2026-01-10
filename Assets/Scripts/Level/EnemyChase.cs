using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;

    [Header("Separation")]
    public float separationRadius = 1.2f;
    public float separationStrength = 2.5f;
    public LayerMask enemyLayer;

    Transform player;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 dirToPlayer = player.position - transform.position;
        dirToPlayer.y = 0f;

        float dist = dirToPlayer.magnitude;
        if (dist <= stopDistance) return;

        Vector3 moveDir = dirToPlayer.normalized;

        // 🔴 SEPARATION
        Collider[] nearbyEnemies = Physics.OverlapSphere(
            transform.position,
            separationRadius,
            enemyLayer
        );

        Vector3 separationDir = Vector3.zero;

        foreach (Collider c in nearbyEnemies)
        {
            if (c.gameObject == gameObject) continue;

            Vector3 diff = transform.position - c.transform.position;
            diff.y = 0f;

            if (diff.magnitude > 0.01f)
                separationDir += diff.normalized / diff.magnitude;
        }

        Vector3 finalDir =
            moveDir +
            separationDir * separationStrength;

        rb.MovePosition(
            rb.position +
            finalDir.normalized * moveSpeed * Time.fixedDeltaTime
        );

        transform.forward = moveDir;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }
}
