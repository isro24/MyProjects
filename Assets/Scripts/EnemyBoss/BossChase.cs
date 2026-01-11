using UnityEngine;

public class BossChase : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float stopDistance = 2.2f;

    Transform playerTransform;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0.0f;

        if (GameManager.instance != null) GameManager.instance.ResetGame();
    }

    void FixedUpdate()
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        if (playerTransform == null)
        {
            PlayerHealth ph = FindFirstObjectByType<PlayerHealth>();

            if (ph != null)
            {
                playerTransform = ph.transform;
                Debug.Log("✅ BossChase: Target Terkunci -> " + ph.name);
            }
            else
            {
                return; // Kalau belum ketemu, jangan gerak dulu
            }
        }

        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= stopDistance) return;

        Vector3 dir = (playerTransform.position - transform.position).normalized;

        dir.y = 0;

        Vector3 move = dir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }
    }
}