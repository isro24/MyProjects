using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    [Header("Target & UI")]
    public GameObject bossObject; 
    public GameObject victoryUI;  

    private bool hasWon = false;

    void Update()
    {
        if (bossObject == null && !hasWon)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        hasWon = true;

        if (victoryUI != null) victoryUI.SetActive(true);

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