using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;

    Transform player;
    Rigidbody rb;
    bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    public void SetDead()
    {
        isDead = true;
    }

    void FixedUpdate()
    {
        if (isDead || player == null) return;

        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        if (dir.magnitude <= stopDistance) return;

        Vector3 nextPos = rb.position + dir.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPos);

        rb.rotation = Quaternion.LookRotation(dir);
    }
}
