using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset inputActions;

    [Header("Reference")]
    public Transform handR;        // ⬅ GANTI dari weapon → Hand_R
    public Weapon weaponScript;    // ⬅ drag Sword (Weapon.cs)

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
        // SIMPAN ROTASI AWAL TANGAN
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

        // arah swing (kiri / kanan)
        float dir = swingRight ? 1f : -1f;
        swingRight = !swingRight;

        Quaternion from = defaultHandRotation *
            Quaternion.Euler(0, -swingAngle / 2f * dir, 0);

        Quaternion to = defaultHandRotation *
            Quaternion.Euler(0, swingAngle / 2f * dir, 0);

        handR.localRotation = from;

        // AKTIFKAN DAMAGE
        weaponScript.EnableHit();

        float t = 0f;
        while (t < attackDuration)
        {
            t += Time.deltaTime;
            handR.localRotation = Quaternion.Slerp(from, to, t / attackDuration);
            yield return null;
        }

        // RESET POSISI (INI YANG HILANG DI CODE LAMA)
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
