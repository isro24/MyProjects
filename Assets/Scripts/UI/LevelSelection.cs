using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;
    [SerializeField] string[] levelScenes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int unlocked = GameManager.instance.GetUnlockedLevel();
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = i < unlocked;
            int index = i;
            levelButtons[i].onClick.AddListener(() => LoadLevel(index));
        }
    }

    void LoadLevel(int index)
    {
        if (index < levelScenes.Length)
            SceneManager.LoadScene(levelScenes[index]);
    }

    public void BackScene()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
