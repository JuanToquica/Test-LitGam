using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerMovementConfig stats;
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalVelocity;
    private float verticalRotation;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        inputReader.moveEvent += SetMoveInput;
        inputReader.lookEvent += SetLookInput;
        inputReader.jumpEvent += Jump;
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= SetMoveInput;
        inputReader.lookEvent -= SetLookInput;
        inputReader.jumpEvent -= Jump;
    }

    private void Update()
    {
        ApplyRotation();
        ApplyMovement();
        ApplyGravity();
    }

    private void SetMoveInput(Vector2 value) => moveInput = value;
    private void SetLookInput(Vector2 value) => lookInput = value;
    private void Jump()
    {
        if (controller.isGrounded)
            verticalVelocity = Mathf.Sqrt(stats.jumpHeight * -2f * stats.gravity);
    }

    private void ApplyMovement()
    {
        Vector3 movementDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(movementDirection * stats.moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2;
        else
            verticalVelocity += stats.gravity * Time.deltaTime;

        CollisionFlags flags = controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);

        if ((flags & CollisionFlags.Above) != 0 && verticalVelocity > 0)
        {
            verticalVelocity = 0;
        }
    }

    private void ApplyRotation()
    {
        transform.Rotate(Vector3.up * lookInput.x * stats.sensitivity);

        verticalRotation -= lookInput.y * stats.sensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -stats.maxViewAngle, stats.maxViewAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
