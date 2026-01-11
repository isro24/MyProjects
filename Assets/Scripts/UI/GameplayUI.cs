using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI enemyCountText;

    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.instance != null)
        {
            string pName = GameManager.instance.GetPlayerName();
            nameText.text = pName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            CountEnemies();
            timer = 0f;
        }
    }

    void CountEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies: " + enemies.Length.ToString();
        }
    }
}
