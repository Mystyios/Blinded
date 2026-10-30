using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerSO : MonoBehaviour
{
    [Header("Movement Settings")]
    public FloatData walkSpeed;
    public FloatData sprintSpeed;
    public FloatData jumpHeight;
    public FloatData gravity;
    public BooleanData canMove; // Reference to toggle movement on/off

    [Header("Visual Model Rotation")]
    public Transform visualModel;
    public float rotationSmoothTime = 0.1f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 previousMoveDirection;
    private bool isJumping;

    // Heavy Attack Override
    private bool heavyOverrideActive = false;
    private Vector3 heavyOverrideDirection;
    private float heavyOverrideSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;
        }

        // Check if normal movement is allowed or heavy override is active
        if ((canMove == null || !canMove.value) && !heavyOverrideActive)
        {
            ApplyGravity();
            return;
        }

        Vector3 move = Vector3.zero;

        if (heavyOverrideActive)
        {
            move = heavyOverrideDirection;
        }
        else
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 input = new Vector3(x, 0f, z).normalized;

            move = transform.TransformDirection(input);
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed.value : walkSpeed.value;

            if (move.sqrMagnitude > 0.01f)
            {
                previousMoveDirection = move;
                heavyOverrideSpeed = currentSpeed; // Save current speed for heavy override
            }

            controller.Move(move * currentSpeed * Time.deltaTime);
        }

        // Apply heavy override movement if active
        if (heavyOverrideActive)
        {
            controller.Move(heavyOverrideDirection * heavyOverrideSpeed * Time.deltaTime);
        }

        // Handle jumping (only when not overriding)
        if (!heavyOverrideActive && Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight.value * -2f * gravity.value);
            isJumping = true;
        }

        ApplyGravity();
        RotateModel();
    }

    void ApplyGravity()
    {
        velocity.y += gravity.value * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RotateModel()
    {
        if (visualModel != null && previousMoveDirection.sqrMagnitude > 0.01f)
        {
            Vector3 flatDirection = new Vector3(previousMoveDirection.x, 0f, previousMoveDirection.z);
            Quaternion targetRotation = Quaternion.LookRotation(flatDirection);
            Quaternion smoothRotation = Quaternion.Slerp(visualModel.rotation, targetRotation, Time.deltaTime / rotationSmoothTime);
            visualModel.rotation = Quaternion.Euler(0f, smoothRotation.eulerAngles.y, 0f);
        }
    }

    // === Heavy Attack Movement Override Methods ===
    public void StartHeavyOverride()
    {
        heavyOverrideActive = true;
        heavyOverrideDirection = previousMoveDirection.normalized;
        heavyOverrideSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed.value : walkSpeed.value;
    }

    public void StopHeavyOverride()
    {
        heavyOverrideActive = false;
    }
}
