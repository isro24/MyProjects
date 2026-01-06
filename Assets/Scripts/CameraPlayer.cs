using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset = new Vector3(0.14f, 3.09f, -6.22f);
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
