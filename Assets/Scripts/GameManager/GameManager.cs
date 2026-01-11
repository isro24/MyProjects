using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    string playerName;
    int selectedCharacterId = -1;
    int unlockedLevel;
    int currentLevel;

    [Serializable]
    public class JsonData
    {
        public string playerName;
        public int selectedCharacterId;
        public int unlockedLevel;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    public void ResetGame()
    {
        isGameOver = false;
        Time.timeScale = 1f; 
    }

    // Setter
    public void SetPlayerName(string name)
    {
        if (playerName == name)return;
        playerName = name;
        SaveData();
    }
    public void SetCharacter(int id)
    {
        if(selectedCharacterId == id)return;
        selectedCharacterId = id;
        SaveData();
    }
    public void SetUnlockedLevel(int level)
    {
        if(level <= unlockedLevel) return;
        unlockedLevel = level;
        SaveData();
    }
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }

    // Getter
    public string GetPlayerName() => playerName;
    public int GetCharacter() => selectedCharacterId;
    public int GetUnlockedLevel()
    {
        return unlockedLevel;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SaveData()
    {
        JsonData data = new JsonData();

        data.playerName = playerName;
        data.selectedCharacterId = selectedCharacterId;
        data.unlockedLevel = unlockedLevel;

        string json = JsonUtility.ToJson(data);
        string path = Application.dataPath + "/saveData.json";
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.dataPath + "/saveData.json";

        if (!File.Exists(path))
        {
            unlockedLevel = 1;
            selectedCharacterId = 0;
            playerName = "";
            return;
        }

        string json = File.ReadAllText(path);
        JsonData data = JsonUtility.FromJson<JsonData>(json);

        playerName = data.playerName;
        selectedCharacterId = data.selectedCharacterId;
        unlockedLevel = Mathf.Max(1, data.unlockedLevel);
    }
}
