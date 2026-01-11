using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset inputActions;

    [Header("Reference")]
    public Transform handR;
    public Weapon weaponScript;  

    [Header("Attack Settings")]
    public float attackDuration = 0.25f;
    public float attackCooldown = 0.4f;
    public float swingAngle = 180f;

    InputAction attackAction;
    bool canAttack = true;
    bool swingRight = true;

    Quaternion defaultHandRotation;
    Coroutine attackCoroutine;

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

    void Start()
    {
        defaultHandRotation = handR.localRotation;
    }

    void Update()
    {
        if (attackAction.WasPressedThisFrame() && canAttack)
        {
            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);

            attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        canAttack = false;

        float dir = swingRight ? 1f : -1f;
        swingRight = !swingRight;

        Quaternion from = defaultHandRotation *
            Quaternion.Euler(0, -swingAngle / 2f * dir, 0);

        Quaternion to = defaultHandRotation *
            Quaternion.Euler(0, swingAngle / 2f * dir, 0);

        handR.localRotation = from;

        weaponScript.EnableHit();

        float t = 0f;
        while (t < attackDuration)
        {
            t += Time.deltaTime;
            handR.localRotation = Quaternion.Slerp(from, to, t / attackDuration);
            yield return null;
        }

        weaponScript.DisableHit();
        handR.localRotation = defaultHandRotation;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}
