using System;
using System.IO;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    string playerName;
    int selectedCharacterId;
    int selectedLevelId;

    [Serializable]
    public class JsonData
    {
        public string playerName;
        public int selectedCharacterId;
        public int selectedLevelId;
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

    public void SetPlayerName(string name) => playerName = name;
    public void SetCharacter(int id) => selectedCharacterId = id;
    public void SetLevel(int id) => selectedLevelId = id;

    public string GetPlayerName() => playerName;
    public int GetCharacter() => selectedCharacterId;
    public int GetLevel() => selectedLevelId;


    public void SaveData()
    {
        JsonData data = new JsonData();

        data.playerName = playerName;
        data.selectedCharacterId = selectedCharacterId;
        data.selectedLevelId = selectedLevelId;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/save.json";
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonData data = JsonUtility.FromJson<JsonData>(json);

            playerName = data.playerName;
            selectedCharacterId = data.selectedCharacterId;
            selectedLevelId = data.selectedLevelId;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
