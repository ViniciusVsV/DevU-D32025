using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    [Header("||===== States =====||")]
    [SerializeField] private Idle idleState;
    [SerializeField] private Run runState;
    [SerializeField] private Jump jumpState;
    [SerializeField] private Fall fallState;
    [SerializeField] private Dash dashState;
    [SerializeField] private Crouch crouchState;

    [Header("||===== Jump Parameters =====||")]
    [Header("|=== Grounded Check ===|")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;

    [Header("|=== Buffer ===|")]
    [SerializeField] private float jumpBufferDuration;
    private float jumpBufferTimer;

    [Header("|=== Coyote Timer ===|")]
    [SerializeField] private float coyoteDuration;
    private float coyoteTimer;

    [Header("||===== Dash Parameters =====||")]
    [Header("|=== Cooldown ===|")]
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("||===== Crouch Parameters =====||")]
    [Header("|=== One Way Platform ===|")]
    [SerializeField] private float doubleCrouchThreshold;
    private float doubleCrouchTimer;
    private OneWayPlatformBehaviour currentPlatformBehaviour;

    [Header("||===== Booleans =====||")]
    public bool isFacingRight;
    public bool isGrounded;
    public bool isCrouching;

    [HideInInspector] public Vector2 moveDirection;

    protected override void Awake()
    {
        base.Awake();

        idleState.Setup(rb, transform, animator, spriteRenderer, this);
        runState.Setup(rb, transform, animator, spriteRenderer, this);
        jumpState.Setup(rb, transform, animator, spriteRenderer, this);
        fallState.Setup(rb, transform, animator, spriteRenderer, this);
        dashState.Setup(rb, transform, animator, spriteRenderer, this);
        crouchState.Setup(rb, transform, animator, spriteRenderer, this);

        stateMachine.Set(idleState);

        isFacingRight = true;
    }

    protected override void Update()
    {
        base.Update();

        if (stateMachine.currentState.isComplete == true)
        {
            if (rb.linearVelocityY < 0)
                stateMachine.Set(fallState);

            else
            {
                if (isCrouching)
                    stateMachine.Set(crouchState);

                else if (moveDirection.x != 0)
                    stateMachine.Set(runState);

                else
                    stateMachine.Set(idleState);
            }
        }

        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        coyoteTimer = isGrounded ? coyoteDuration : coyoteTimer -= Time.deltaTime;

        if (jumpBufferTimer > Mathf.Epsilon)
            jumpBufferTimer -= Time.deltaTime;
        if (dashCooldownTimer > Mathf.Epsilon)
            dashCooldownTimer -= Time.deltaTime;
        if (doubleCrouchTimer > Mathf.Epsilon)
            doubleCrouchTimer -= Time.deltaTime;
    }

    public void Run(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

        if (moveDirection.x > 0 && !isFacingRight || moveDirection.x < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;

            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpBufferTimer = jumpBufferDuration;

        else if (context.canceled && stateMachine.currentState == jumpState)
        {
            jumpState.JumpCut();
            return;
        }

        if (stateMachine.currentState.isComplete && coyoteTimer > Mathf.Epsilon && jumpBufferTimer > Mathf.Epsilon)
            stateMachine.Set(jumpState);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && dashCooldownTimer <= Mathf.Epsilon)
        {
            dashCooldownTimer = dashCooldown;

            stateMachine.Set(dashState);
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = true;

            if (doubleCrouchTimer > Mathf.Epsilon && currentPlatformBehaviour != null)
                currentPlatformBehaviour.DisableCollision();
            else
                doubleCrouchTimer = doubleCrouchThreshold;
        }
        else
            isCrouching = false;
    }

    public void Fall()
    {
        stateMachine.Set(fallState);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
            currentPlatformBehaviour = collision.gameObject.GetComponent<OneWayPlatformBehaviour>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
            currentPlatformBehaviour = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);
    }
}