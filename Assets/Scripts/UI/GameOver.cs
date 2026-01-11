using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartLevel()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetGame();
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToLevel()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetGame();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }
}