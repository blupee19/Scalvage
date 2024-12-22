using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    AudioManager audioManager;
    public Rigidbody2D rb;
    public InputActionAsset playerControls;
    public Camera mainCamera;
    public Animator animator;
    private MovingPlatform platform;

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
    private bool isDashing;
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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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

        //if (moveDirection.x > 0 || moveDirection.x < 0)
        //{
        //    audioManager.PlaySFX(audioManager.Walk);
        //}

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
            audioManager.PlaySFX(audioManager.JumpStart);
            rb.linearVelocityY = jumpForce;                   
            coyoteTimeCounter = 0f;

            //animator.SetBool("isJumping", true);
            
            // Reset JumpInput to prevent immediate consecutive jumps
            JumpInput = false;
                        
        }


        if (SprintInput && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            audioManager.PlaySFX(audioManager.Dash);
            StartDash();
        }

    }

    private void StartDash()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector3 charPos = mainCamera.transform.right;

        isDashing = true;
        dashTime = dashDuration;
        lastDashTime = Time.time;

        // Determine dash direction
        if (moveDirection.x > 0 && !facingRight)
        {
            // Moving right but facing left -> Backward dash
            dashDirection = Vector2.right;
            animator.SetBool("isBackwardDashing", true);
        }
        else if (moveDirection.x < 0 && facingRight)
        {
            // Moving left but facing right -> Backward dash
            dashDirection = Vector2.left;
            animator.SetBool("isBackwardDashing", true);
        }
        else
        {
            // Dash in the direction character is facing
            dashDirection = facingRight ? Vector2.right : Vector2.left;
            animator.SetBool("isBackwardDashing", false);
        }

        // Start dash coroutine
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        rb.linearVelocityX = dashDirection.x * dashSpeed * moveSpeed;
          
        yield return new WaitForSeconds(dashDuration);
        
        rb.linearVelocityX = 0f;
        isDashing = false;
    }

    void Flip()
    {
        // Get the mouse position in world space
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the character's position
        Vector2 characterPosition = transform.position;

        // Calculate the direction from the character to the mouse
        float direction = mouseWorldPosition.x - characterPosition.x;

        // Flip the character based on the mouse's horizontal position relative to the character
        if (direction < 0f && facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
        }
        else if (direction > 0f && !facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
        }
    }


    void AnimationCalls()
    {
        
        if (isDashing)
        {
            if ((moveDirection.x > 0 && !facingRight) || (moveDirection.x < 0 && facingRight))
            {
                animator.SetBool("isBackwardDashing", true);
                animator.SetBool("isDashing", false);                
            }
            else
            {
                animator.SetBool("isDashing", true);
                animator.SetBool("isBackwardDashing", false);
            }

            animator.SetBool("isJumping", false);
            
        }        
        else
        {
            animator.SetBool("isDashing", false);
            animator.SetBool("isBackwardDashing", false);

            animator.SetFloat("Speed", Mathf.Abs(moveDirection.x));

            if (IsGrounded())
            {
                animator.SetBool("isJumping", false);
            }
            if (coyoteTimeCounter > 0f && JumpInput)
            {
                animator.SetBool("isJumping", true);
            }
        }
        

    }

}
