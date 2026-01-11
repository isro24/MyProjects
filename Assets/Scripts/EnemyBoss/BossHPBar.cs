using UnityEngine;

public class BossHPBar : MonoBehaviour
{
    public Transform fill;
    BossHealth boss;

    Vector3 startScale;

    void Start()
    {
        boss = GetComponentInParent<BossHealth>();
        startScale = fill.localScale;

        boss.OnHealthChanged += UpdateBar;
    }

    void UpdateBar(int current, int max)
    {
        float percent = (float)current / max;
        fill.localScale = new Vector3(
            startScale.x * percent,
            startScale.y,
            startScale.z
        );
    }
}
