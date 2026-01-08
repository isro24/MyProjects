using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset inputActions;
    public LayerMask enemyLayer;

    public int attackDamage = 25;
    public float attackRadius = 2.5f;

    InputAction attackAction;

    void OnEnable()
    {
        var playerMap = inputActions.FindActionMap("Player");
        attackAction = playerMap.FindAction("Attack");

        attackAction.Enable();
    }

    void OnDisable()
    {
        attackAction.Disable();
    }

    void Update()
    {
        if (attackAction.WasPressedThisFrame())
        {
            Debug.Log("ATTACK J DITEKAN");
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(
    transform.position,
    attackRadius,
    enemyLayer

);


        Debug.Log("Enemy kena overlap: " + hits.Length);

        foreach (Collider hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, attackRadius);
    }
}
