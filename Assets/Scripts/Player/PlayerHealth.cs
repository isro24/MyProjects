using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;

    public int CurrentHP => currentHP;

    [Header("UI References")]
    public GameObject gameOverPanel;

    void Start()
    {
        if (GameManager.instance != null)
            GameManager.instance.ResetGame();

        currentHP = maxHP;

        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.Find("GameUI");

            if (gameOverPanel == null)
            {
                var ui = FindFirstObjectByType<GameOver>();
                if (ui != null) gameOverPanel = ui.gameObject;
            }
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver) return;

        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (GameManager.instance != null)
            GameManager.instance.isGameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        gameObject.SetActive(false);
    }
}
