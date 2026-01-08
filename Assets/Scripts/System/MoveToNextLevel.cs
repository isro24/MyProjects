using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    [SerializeField] int levelId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int current = SceneManager.GetActiveScene().buildIndex;
            int next = current + 1;
            int total = SceneManager.sceneCountInBuildSettings;

            if (next < total)
            {
                GameManager.instance.SetUnlockedLevel(next);
                SceneManager.LoadScene(next);
            }
            else
            {
                SceneManager.LoadScene("MainScreen");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
