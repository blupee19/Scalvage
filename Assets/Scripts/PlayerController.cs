using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public InputActionAsset playerControls;
    public Animator animator;

    [Header("Action Map Name")]
    private string actionMapName = "Player";

    [Header("Input Actions")]
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    [Header("Action Map References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";

    [Header("Movement")]
    public bool facingRight = true;
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashTime;
    private float lastDashTime;
    private Vector2 dashDirection;

    [Header("Jumping")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] private float airDrag = 0.5f;
    private bool canJump = true;
    public bool jumpPressed = false;
    private float coyoteTimeCounter;
    public LayerMask Ground;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;


    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool SprintInput { get; private set; }


    Vector2 moveDirection = Vector2.zero;


    private void Awake()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        jumpAction.performed += context =>
        {
            if (canJump)
            { 
                JumpInput = true;
                canJump = false; // Prevents further jumps until button release
            }
        };
        jumpAction.canceled += context =>
        {
            JumpInput = false;
            canJump = true; // Allows jumping again after button release
        };

        sprintAction.performed += context => SprintInput = true;
        sprintAction.canceled += context => SprintInput = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
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

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);
        
    }

    private void Update()
    {
        Flip();
        
    }

    private void FixedUpdate()
    {
        AnimationCalls();
        moveDirection = moveAction.ReadValue<Vector2>();
        rb.linearVelocityX = moveDirection.x * moveSpeed;

       

        if (IsGrounded())
        {
            //animator.SetBool("isJumping", false);
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            rb.linearVelocityX = moveDirection.x * moveSpeed * airDrag;
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }


        if (coyoteTimeCounter > 0f && JumpInput)
        {
            rb.linearVelocityY = jumpForce;                    
            rb.linearVelocityY = jumpForce;                    
            coyoteTimeCounter = 0f;

            //animator.SetBool("isJumping", true);
            
            // Reset JumpInput to prevent immediate consecutive jumps
            JumpInput = false;
                        
        }


        if (SprintInput && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            StartDash();
        }

    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        lastDashTime = Time.time;

        dashDirection = new Vector2(moveDirection.x, moveDirection.y).normalized;

        if (facingRight)
        {
            dashDirection = Vector2.right;
        }
        else 
        {
            dashDirection = Vector2.left;
        }
        StartCoroutine(DashCoroutine());

    }

    private IEnumerator DashCoroutine()
    {
        rb.linearVelocityX = dashDirection.x * dashSpeed * moveSpeed;
        animator.SetBool("isDashing", true);
        yield return new WaitForSeconds(dashDuration);
        animator.SetBool("isDashing", false);
        rb.linearVelocityX = 0f;
        isDashing = false;
    }

    void Flip()
    {
        if (moveDirection.x < 0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
        }

        if (moveDirection.x > 0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
        }
    }

    void AnimationCalls()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x));

        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }
        if (coyoteTimeCounter > 0f && JumpInput)
        {
            animator.SetBool("isJumping", true);
        }
        //if(SprintInput && Time.time >= lastDashTime + dashCooldown && !isDashing)
        //{
        //    animator.SetBool("isDashing", true);
        //}
        //else
        //{
        //    animator.SetBool("isDashing", false);
        //}
        
    }

}
