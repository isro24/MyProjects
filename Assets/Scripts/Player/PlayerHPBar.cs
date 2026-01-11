using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    PlayerHealth health;
    Vector3 startScale;

    void Start()
    {
        health = GetComponentInParent<PlayerHealth>();
        startScale = transform.localScale;
    }

    void Update()
    {
        if (health == null) return;

        float percent = (float)health.CurrentHP / health.maxHP;
        percent = Mathf.Clamp01(percent);

        transform.localScale = new Vector3(
            startScale.x * percent,
            startScale.y,
            startScale.z
        );
    }
}
