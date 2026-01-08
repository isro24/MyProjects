using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] GameObject[] characterPrefabs;
    [SerializeField] Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("Character Prefabs belum diisi");
            return;
        }

        int id = GameManager.instance.GetCharacter();
        if (id < 0 || id >= characterPrefabs.Length)
            id = 0;

        Vector3 spawnPos = spawnPoint.position + Vector3.up * 0.5f;

        GameObject player = Instantiate(
                  characterPrefabs[id],
                  spawnPos,
                  spawnPoint.rotation
              );

        CameraPlayer cam = Camera.main.GetComponent<CameraPlayer>();
        if (cam != null)
        {
            cam.player = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
