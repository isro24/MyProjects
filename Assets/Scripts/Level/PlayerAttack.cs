using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public InputActionAsset inputActions;
    public Transform weapon;

    public float attackDuration = 0.25f;
    public float attackCooldown = 0.4f;

    InputAction attackAction;
    bool canAttack = true;

    Weapon weaponScript;

    Quaternion defaultWeaponRotation;
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
        weaponScript = weapon.GetComponent<Weapon>();

        // SIMPAN ROTASI AWAL
        defaultWeaponRotation = weapon.localRotation;
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

        Debug.Log("[PlayerAttack] ATTACK START");

        weapon.localRotation = defaultWeaponRotation;

        Quaternion attackRotation = defaultWeaponRotation * Quaternion.Euler(0, 90f, 0);

        weaponScript.EnableHit();

        float t = 0f;
        while (t < attackDuration)
        {
            t += Time.deltaTime;
            weapon.localRotation = Quaternion.Slerp(
                defaultWeaponRotation,
                attackRotation,
                t / attackDuration
            );
            yield return null;
        }

        // RESET TOTAL (INI KUNCI)
        weapon.localRotation = defaultWeaponRotation;
        weaponScript.DisableHit();

        Debug.Log("[PlayerAttack] ATTACK END");

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        Debug.Log("[PlayerAttack] READY AGAIN");
    }
}
