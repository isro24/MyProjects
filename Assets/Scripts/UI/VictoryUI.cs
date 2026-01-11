using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject victoryUI; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinGame();
        }
    }

    // Logic Menang
    public void WinGame()
    {
        if (victoryUI != null)
            victoryUI.SetActive(true);

        Time.timeScale = 0f; 
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("LevelSelection"); 
    }
}