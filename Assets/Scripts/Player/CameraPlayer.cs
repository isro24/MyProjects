using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset = new Vector3(0.30f, 1.55f, -5.5f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
