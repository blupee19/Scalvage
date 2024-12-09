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
    public float mouseSensivity = 0.2f;


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

        mainCamera.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10);

        float mouseX = AimInput.x * mouseSensivity;
        float mouseY = AimInput.y * mouseSensivity;

        mainCamera.transform.Translate(mouseX, mouseY, 0);



    }
}