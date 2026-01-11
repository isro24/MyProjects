using System.Collections;
using UnityEngine;

public class BossGroundSlam : MonoBehaviour
{
    [Header("AOE Settings")]
    public float slamRadius = 2f;
    public int slamDamage = 50;
    public float slamCooldown = 1f;

    [Header("Indicator")]
    public GameObject slamIndicator;
    public float warningDuration = 2f;
    public float blinkSpeed = 0.25f;

    [Header("Audio")]
    public AudioSource slamAudio;

    PlayerHealth player;
    bool isSlamming;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();

        if (player == null)
            Debug.LogError("PLAYER TIDAK DITEMUKAN");

        slamIndicator.SetActive(false);
        StartCoroutine(SlamLoop());
    }


    IEnumerator SlamLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(slamCooldown);
            yield return StartCoroutine(DoSlam());
        }
    }

    IEnumerator DoSlam()
    {
        if (isSlamming || player == null) yield break;
        isSlamming = true;

        // POSISI INDICATOR DI PLAYER
        Vector3 slamPos = new Vector3(
            player.transform.position.x,
            0.01f,
            player.transform.position.z
        );

        slamIndicator.transform.position = slamPos;
        slamIndicator.transform.localScale = Vector3.one * slamRadius * 2f;

        // === WARNING BLINK ===
        float timer = 0f;
        bool visible = true;
        slamIndicator.SetActive(true);

        while (timer < warningDuration)
        {
            visible = !visible;
            slamIndicator.SetActive(visible);
            yield return new WaitForSeconds(blinkSpeed);
            timer += blinkSpeed;
        }

        slamIndicator.SetActive(true);

        // === SLAM ===
        if (slamAudio != null)
            slamAudio.Play();

        Collider[] hits = Physics.OverlapSphere(slamPos, slamRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth ph = hit.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(slamDamage);
                    Debug.Log("💥 PLAYER KENA GROUND SLAM");
                }
            }
        }

        slamIndicator.SetActive(false);
        isSlamming = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slamRadius);
    }
}
