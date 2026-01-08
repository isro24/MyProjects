using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScene()
    {
        if (!string.IsNullOrEmpty(GameManager.instance.GetPlayerName()) &&
                GameManager.instance.GetCharacter() >= 0)
        {
            SceneManager.LoadScene("LevelSelection");
        }
        else
        {
            SceneManager.LoadScene("CharacterSelection");
        }
    }

    public void CharacterScene()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
