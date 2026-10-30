using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Simple3DCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isJumping;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Grounded check using CharacterController
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
            isJumping = false;
        }

        // Input
        float x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float z = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
        Vector3 move = transform.right * x + transform.forward * z;
        move.Normalize();

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}