using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public Vector2 knockbackDirection;
    public OneWayPlatform currentPlatform;

    [Header("||===== States =====||")]
    [SerializeField] private Idle idleState;
    [SerializeField] private Run runState;
    [SerializeField] private Jump jumpState;
    [SerializeField] private Fall fallState;
    [SerializeField] private Dash dashState;
    [SerializeField] private Crouch crouchState;
    [SerializeField] private Knockback knockbackState;
    [SerializeField] private WallSlide wallSlideState;
    [SerializeField] private WallJump wallJumpState;

    [Header("||===== Jump Parameters =====||")]
    [SerializeField] private int extraJumps;
    private int remainingExtraJumps;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float coyoteDuration;
    private float coyoteTimer;

    [Header("||===== Dash Parameters =====||")]
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("||===== Crouch Parameters =====||")]
    [SerializeField] private float doubleCrouchThreshold;
    private float doubleCrouchTimer;

    [Header("||===== Wall Slide/Jump Parameter =====||")]
    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private float wallCoyoteDuraion;
    private float wallCoyoteTimer;

    [Header("||===== Booleans =====||")]
    public bool jumpPressed;
    public bool dashPressed;
    public bool tookKnockback;

    public bool isFacingRight;
    public bool isGrounded;
    public bool isWalled;
    public bool isCrouching;

    protected override void Awake()
    {
        base.Awake();

        idleState.Setup(rb, transform, animator, spriteRenderer, this);
        runState.Setup(rb, transform, animator, spriteRenderer, this);
        jumpState.Setup(rb, transform, animator, spriteRenderer, this);
        fallState.Setup(rb, transform, animator, spriteRenderer, this);
        dashState.Setup(rb, transform, animator, spriteRenderer, this);
        crouchState.Setup(rb, transform, animator, spriteRenderer, this);
        knockbackState.Setup(rb, transform, animator, spriteRenderer, this);
        wallSlideState.Setup(rb, transform, animator, spriteRenderer, this);
        wallJumpState.Setup(rb, transform, animator, spriteRenderer, this);

        SetIdle(false);

        remainingExtraJumps = extraJumps;

        isFacingRight = true;
    }

    protected override void Update()
    {
        base.Update();

        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);

        isWalled = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer) && !isGrounded;
        wallCoyoteTimer = isWalled ? wallCoyoteDuraion : wallCoyoteTimer - Time.deltaTime;

        if (isGrounded)
        {
            coyoteTimer = coyoteDuration;
            remainingExtraJumps = extraJumps;
        }
        else
            coyoteTimer -= Time.deltaTime;

        if (dashCooldownTimer > Mathf.Epsilon)
            dashCooldownTimer -= Time.deltaTime;
        if (doubleCrouchTimer > Mathf.Epsilon)
            doubleCrouchTimer -= Time.deltaTime;

        jumpPressed = false;
        dashPressed = false;
        tookKnockback = false;
    }

    public void RunInput(InputAction.CallbackContext context)
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

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (coyoteTimer > Mathf.Epsilon || wallCoyoteTimer > Mathf.Epsilon)
                jumpPressed = true;

            else if (remainingExtraJumps > 0)
            {
                remainingExtraJumps--;
                jumpPressed = true;
            }
        }

        else if (context.canceled && stateMachine.currentState == jumpState)
        {
            jumpState.JumpCut();
            return;
        }
    }

    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.performed && dashCooldownTimer <= Mathf.Epsilon)
        {
            dashPressed = true;

            dashCooldownTimer = dashCooldown;
        }
    }

    public void CrouchInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = true;

            if (doubleCrouchTimer > Mathf.Epsilon && currentPlatform != null)
                currentPlatform.DisableCollision();
            else
                doubleCrouchTimer = doubleCrouchThreshold;
        }
        else
            isCrouching = false;
    }

    public void SetIdle(bool forced = false) => SetNewState(idleState, forced);
    public void SetRun(bool forced = false) => SetNewState(runState, forced);
    public void SetJump(bool forced = false) => SetNewState(jumpState, forced);
    public void SetFall(bool forced = false) => SetNewState(fallState, forced);
    public void SetDash(bool forced = false) => SetNewState(dashState, forced);
    public void SetCrouch(bool forced = false) => SetNewState(crouchState, forced);
    public void SetKnockback(bool forced = false) => SetNewState(knockbackState, forced);
    public void SetWallSlide(bool forced = false) => SetNewState(wallSlideState, forced);
    public void SetWallJump(bool forced = false) => SetNewState(wallJumpState, forced);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
            currentPlatform = collision.gameObject.GetComponent<OneWayPlatform>();

        else if (collision.gameObject.CompareTag("MovingPlatform"))
            transform.parent = collision.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
            currentPlatform = null;

        else if (collision.gameObject.CompareTag("MovingPlatform"))
            transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Killbox") || other.CompareTag("Obstacle"))
            TakeDamage(1, new Vector2(-1f, 1f));
    }

    public override void TakeDamage(int damage, Vector2 direction)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
        else
        {
            knockbackDirection = direction;

            tookKnockback = true;
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
}