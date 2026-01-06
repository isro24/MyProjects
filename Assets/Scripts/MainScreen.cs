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
        SceneManager.LoadScene("ChooseLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
