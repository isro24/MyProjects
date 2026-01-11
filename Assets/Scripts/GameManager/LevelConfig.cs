using System.Diagnostics.Contracts;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int enemyHP;
    public int enemyDamage;
    public int maxEnemy;
}

public class LevelConfig : MonoBehaviour
{
    public LevelData[] levels;

    public static LevelConfig instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public LevelData GetCurrentData()
    {
        int level = GameManager.instance.GetCurrentLevel();
        int index = Mathf.Clamp(level - 1, 0, levels.Length - 1);
        return levels[index];
    }
}