using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class CameraFollow : MonoBehaviour
{

    public PlayerController playerController;

    [Header("Input Actions and Asset")]
    public InputActionAsset playerControls;
    private InputAction aimAction;
    public Vector2 AimInput {  get; private set; }

    [Header("Camera Settings")]
    public float smoothSpeed = 5f; // Smoothing speed for camera movement
    public float cameraZDepth = -10f; // Fixed Z position of the camera in 2D
    private Camera mainCamera;


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

    private void LateUpdate()
    {

        float mouseX = AimInput.x;
        float mouseY = AimInput.y;

        transform.position = new Vector3(mouseX, mouseY, -10);


        //Vector3 camPosition = mainCamera.transform.position;
        //camPosition.z = cameraZDepth;
        //Vector3 playerPos = playerController.transform.position;
        //camPosition.x = Mathf.Clamp(playerPos.x, playerPos.x - 10, playerPos.x + 10);
        //camPosition.y = Mathf.Clamp(playerPos.y, playerPos.y - 10, playerPos.y + 10);
        //camPosition = AimInput;
    }
}