using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    Animator animator;
    bool isFacingRight = true;
    public bool cantMove = false;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 1;
    int jumpsRemaining;
    //Coyote time
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    //Jump buffer
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    [SerializeField] bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    private GameObject currentPlatform;
    [SerializeField] private BoxCollider2D playerCollider;
    private float lastCrouchPressTime = -1f;
    private float doubleCrouchPressThreshold = 0.3f;
    public GameObject corpo;

    // [Header("WallCheck")]
    // public Transform wallCheckPos;
    // public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    // public LayerMask wallLayer;

    // [Header("WallMovement")]
    // public float wallSlideSpeed = 2;
    // [SerializeField] bool isWallSliding;

    // bool isWallJumping;
    // float wallJumpDirection;
    // float wallJumpTime = 0.5f;
    // float wallJumpTimer;
    // public Vector2 wallJumpPower = new Vector2(5f, 10f);

    private float baseMoveSpeed;
    void Start()
    {
        animator = GetComponent<Animator>();
        baseMoveSpeed = moveSpeed; // guarda a velocidade base
    }

    void Update()
    {
        GroundCheck();
        Gravity();
        // ProcessWallSlide();
        // ProcessWallJump();


        //Coyote Time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //Jump Buffer
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Jump Buffer + Coyote Time
        if (jumpBufferCounter > 0 && (isGrounded || coyoteTimeCounter > 0f) && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
            jumpsRemaining--;
        }

        // // One way platform drop down
        // if (Input.GetKeyDown(KeyCode.S) && currentPlatform != null)
        // {
        //     if (Time.time - lastSPressTime < doubleSPressThreshold)
        //     {
        //         StartCoroutine(DisableCollision());
        //         lastSPressTime = -1f; //
        //     }
        //     else
        //     {
        //         lastSPressTime = Time.time;
        //     }
        // }


        // animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        // animator.SetFloat("yVelocity", (rb.linearVelocity.y));

        // animator.SetFloat("magnitude", rb.velocity.magnitude);
        // animator.SetBool("isWallSliding", isWallSliding);
    }

    void FixedUpdate()
    {
        if (cantMove) return; 

        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        Flip();


    }


    private void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        // if (!GameManager.Instance.canPlayersMove) return;
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // if (!GameManager.Instance.canPlayersMove) return;
        if (context.started && jumpsRemaining > 0)
        {
            jumpBufferCounter = jumpBufferTime;
        }

        if (context.canceled && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // Wall jump
        // if (context.started && wallJumpTimer > 0f)
        // {
        //     isWallJumping = true;
        //     rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
        //     wallJumpTimer = 0;
        //     AudioManager.Instance.PlaySFX("Pulo");

        //     if (transform.localScale.x != wallJumpDirection)
        //     {
        //         isFacingRight = !isFacingRight;
        //         Vector3 ls = transform.localScale;
        //         ls.x *= -1f;
        //         transform.localScale = ls;
        //     }

        //     Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
        // }
    }



    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    // private void ProcessWallSlide()
    // {
    //     if (!isGrounded & WallCheck() & horizontalMovement != 0)
    //     {
    //         isWallSliding = true;
    //         animator.SetBool("isWallhanging", true);
    //         rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
    //     }
    //     else
    //     {
    //         isWallSliding = false;
    //         animator.SetBool("isWallhanging", false);
    //     }
    // }

    // private void ProcessWallJump()
    // {
    //     if (isWallSliding)
    //     {
    //         isWallJumping = false;
    //         wallJumpDirection = -transform.localScale.x;
    //         wallJumpTimer = wallJumpTime;

    //         CancelInvoke(nameof(CancelWallJump));
    //     }
    //     else if (wallJumpTimer > 0f)
    //     {
    //         wallJumpTimer -= Time.deltaTime;
    //     }
    // }

    // private void CancelWallJump()
    // {
    //     isWallJumping = false;
    // }

    // private bool WallCheck()
    // {
    //     return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    // }

    private void Flip()
    {
        if ((isFacingRight && horizontalMovement < 0) || (!isFacingRight && horizontalMovement > 0))
        {
            isFacingRight = !isFacingRight;


            float yRotation = isFacingRight ? 0f : 180f;


            corpo.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);


        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue;
        // Gizmos.DrawCube(wallCheckPos.position, wallCheckSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentPlatform = null;
        }
    }

    public IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        // if (!GameManager.Instance.canPlayersMove) return;
        if (context.started && currentPlatform != null)
        {
            if (Time.time - lastCrouchPressTime < doubleCrouchPressThreshold)
            {
                StartCoroutine(DisableCollision());
                lastCrouchPressTime = -1f;
            }
            else
            {
                lastCrouchPressTime = Time.time;
            }
        }
    }

}
