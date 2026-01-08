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
    void FixedUpdate()
    {
        Vector3 input = move.ReadValue<Vector3>();

        Vector3 moveDir = new Vector3(input.x, 0f, input.z);
        Vector3 targetPos = playerRB.position + moveDir * speed * Time.fixedDeltaTime;

        playerRB.MovePosition(targetPos);
    }

    void Update()
    {
        if (jump.WasPressedThisFrame() && isGrounded)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

}
