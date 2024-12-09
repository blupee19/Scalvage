using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{

    [Header("Input Actions and Asset")]
    public InputActionAsset playerControls;
    private InputAction aimAction;
    public Vector2 AimInput {  get; private set; }

    [Header("Offset")]
    public PlayerController playerController;// The player's transform
    public Vector3 offset = new Vector3(0, 0, -10); // Offset to keep the camera at a distance
    public float smoothTime = 0.3f; // Time for damping effect
    public float camOffset = 2f;


    private Vector3 velocity = Vector3.zero; // Internal variable for smooth damping

    private void Awake()
    {
        aimAction = playerControls.FindActionMap("Player").FindAction("Look");
        RegisterInputActions();
    }

    private void OnEnable()
    {
        aimAction.Enable();
    }
    private void OnDisable()
    {
        aimAction.Disable();
    }

    void RegisterInputActions()
    {
        aimAction.performed += context => AimInput = context.ReadValue<Vector2>();
        aimAction.canceled += context => AimInput = Vector2.zero;
    }


    void LateUpdate()
    {


        //if (playerController != null)
        //{
        //    // Adjust the offset based on the player's facing direction
        //    float directionOffset = playerController.facingRight ? camOffset : -camOffset;

        //    // Update offset while keeping y and z consistent
        //    Vector3 dynamicOffset = new Vector3(directionOffset, offset.y, offset.z);

        //    // Add movement-based offset (MoveInput affects x and y)
        //    Vector2 moveDir = playerController.MoveInput;
        //    dynamicOffset += new Vector3(moveDir.x * camOffset, moveDir.y * camOffset, 0);

        //    // Calculate the target position
        //    Vector3 targetPosition = playerController.transform.position + dynamicOffset;

        //    // Smoothly move the camera towards the target position
        //    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //}
    }
}