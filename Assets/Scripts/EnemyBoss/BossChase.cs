using UnityEngine;

public class BossChase : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float stopDistance = 2.5f;

    Transform player;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void FixedUpdate()
    {
        // 🔍 Cari player terus sampai ketemu
        if (player == null)
        {
            PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();

            if (ph != null)
            {
                player = ph.transform;
                Debug.Log("✅ BossChase: Player ditemukan -> " + ph.name);
            }
            else
            {
                return; // belum ada player
            }
        }

        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        float dist = dir.magnitude;

        if (dist <= stopDistance)
        {
            if (dir != Vector3.zero)
                transform.forward = dir.normalized;
            return;
        }

        Vector3 move = dir.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
        transform.forward = dir.normalized;
    }
}
