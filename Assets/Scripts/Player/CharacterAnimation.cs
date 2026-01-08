using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 finalPosition;
    private Vector3 initialPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, 0.5f);
    }

    private void OnDisable()
    {
        transform.position = initialPosition;
    }
}
