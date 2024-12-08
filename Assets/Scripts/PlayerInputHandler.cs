using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset inputActions;

    [Header("Action Map References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public float SprintInput { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);   
        }

        moveAction = inputActions.FindActionMap(actionMapName).FindAction(move);
        jumpAction = inputActions.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = inputActions.FindActionMap(actionMapName).FindAction(sprint);
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        jumpAction.performed += context => JumpInput = true;
        jumpAction.canceled += context => JumpInput = false;

        sprintAction.performed += context => SprintInput = context.ReadValue<float>();
        sprintAction.canceled += context => SprintInput = 0f;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
    }

}
