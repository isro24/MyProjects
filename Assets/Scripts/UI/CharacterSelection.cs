using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public TMPro.TMP_InputField inputName;

    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TMP_Text startButtonText;

    private int currentCharacter;
    private bool hasData;

    private void Start()
    {
        string savedName = GameManager.instance.GetPlayerName();
        int saveCharacter = GameManager.instance.GetCharacter();

        hasData = !string.IsNullOrEmpty(savedName);

        if (!string.IsNullOrEmpty(savedName))
            inputName.text = savedName;

        int savedCharacter = GameManager.instance.GetCharacter();
        if (savedCharacter >= 0 && savedCharacter < transform.childCount)
            currentCharacter = savedCharacter;
        else
            currentCharacter = 0;

        startButtonText.text = hasData ? "Save " : "Start";

        SelectCharacter(currentCharacter);
    }

    private void SelectCharacter(int characterIndex)
    {
        prevButton.interactable = (characterIndex != 0);
        nextButton.interactable = (characterIndex != transform.childCount-1);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == characterIndex);
        }
    }

    public void ChangeCharacter(int characterChange)
    {
        currentCharacter += characterChange;
        SelectCharacter(currentCharacter);
    }

    public void StartScene()
    {
        GameManager.instance.SetPlayerName(inputName.text);
        GameManager.instance.SetCharacter(currentCharacter);

        if (!hasData)
            SceneManager.LoadScene("LevelSelection");
    }


    public void BackScene()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
