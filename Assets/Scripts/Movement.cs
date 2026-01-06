using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody playerRB;

    public InputActionAsset inputActions;

    private InputAction move;
    private InputAction jump;

    private Vector3 moveVector;

    private float jumpForce = 8.0f;
    private float speed = 4.0f;

    private bool isGrounded;


    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        move = InputSystem.actions.FindAction("3DMove");
        jump = InputSystem.actions.FindAction("Jump");

    }

    // Update is called once per frame
    void Update()
    {
        moveVector = move.ReadValue<Vector3>();

        transform.Translate(Vector3.forward * moveVector.z * Time.deltaTime * speed);

        transform.Translate(Vector3.right * moveVector.x * Time.deltaTime * speed);

        if (jump.WasPressedThisFrame() && isGrounded)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}
