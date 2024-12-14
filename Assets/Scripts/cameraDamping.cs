using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public PlayerController playerController; // Reference to the player

    [Header("Input Actions and Asset")]
    public InputActionAsset playerControls;
    private InputAction aimAction;
    public Vector2 AimInput { get; private set; }

    [Header("Camera Settings")]
    public float smoothSpeed = 5f; // Smoothing speed for camera movement
    public float cameraZDepth = -10f; // Fixed Z position of the camera in 2D
    private Camera mainCamera;
    public float mouseSensitivity = 0.2f;
    public float yOffset = 0.5f;

    [Header("Clamp Settings")]
    public float clampRadius = 5f; // Maximum distance camera can move from the player

    private void Awake()
    {
        aimAction = playerControls.FindActionMap("Player").FindAction("Look");
        aimAction.performed += context => AimInput = context.ReadValue<Vector2>();
        aimAction.canceled += context => AimInput = Vector2.zero;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        aimAction.Enable();
    }

    private void OnDisable()
    {
        aimAction.Disable();
    }
    private void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Confined; // Confines the cursor within the game window
        
    }

    private void LateUpdate()
    {
        if (playerController == null || mainCamera == null) return;

        // Calculate mouse movement offset
        float mouseX = AimInput.x * mouseSensitivity;
        float mouseY = AimInput.y * mouseSensitivity;

        // Get the camera's current position
        Vector3 cameraPosition = mainCamera.transform.position;

        // Calculate the new camera position based on mouse input
        Vector3 newCameraPosition = cameraPosition + new Vector3(mouseX, mouseY + yOffset, 0);

        // Clamp the camera's position to within a certain radius of the player
        Vector3 playerPosition = playerController.transform.position;
        Vector3 offset = newCameraPosition - playerPosition;

        if (offset.magnitude > clampRadius)
        {
            // Adjust position to stay within the clamp radius
            offset = offset.normalized * clampRadius;
            newCameraPosition = playerPosition + offset;
        }

        // Update the camera's position
        newCameraPosition.z = cameraZDepth; // Ensure consistent Z depth
        mainCamera.transform.position = Vector3.Lerp(cameraPosition, newCameraPosition, smoothSpeed * Time.deltaTime);
    }
}
