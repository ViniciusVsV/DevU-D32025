using Characters.Player.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private StateController playerController;
        private Transform playerTransform;

        [Header("||===== Jump Parameters =====||")]
        [SerializeField] private int extraJumps;
        private int remainingExtraJumps;

        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Vector2 groundCheckSize;
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private float coyoteDuration;
        private float coyoteTimer;

        [SerializeField] private float jumpBuffer;
        private float jumpBufferTimer;

        [Header("||===== Dash Parameters =====||")]
        [SerializeField] private int dashUses;
        private int remainingDashes;

        [Header("||===== Wall Slide/Jump Parameter =====||")]
        [SerializeField] private Transform wallCheckPoint;
        [SerializeField] private Vector2 wallCheckSize;
        [SerializeField] private LayerMask wallLayer;

        private int lookingDirection;

        private GuitarManager guitarManager;

        private void Awake()
        {
            playerTransform = transform.parent;
        }

        private void Update()
        {
            playerController.isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
            playerController.isWalled = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer) && !playerController.isGrounded;

            lookingDirection = playerController.isFacingRight ? 1 : -1;

            playerController.isWallSliding = playerController.isWalled && playerController.moveDirection.x != 0 && Mathf.Sign(playerController.moveDirection.x) == Mathf.Sign(lookingDirection);

            if (playerController.isGrounded)
            {
                coyoteTimer = coyoteDuration;
                remainingExtraJumps = extraJumps;
                remainingDashes = dashUses;
            }
            else if (playerController.GetCurrentState() is not States.Dash)
                coyoteTimer -= Time.deltaTime;

            if (jumpBufferTimer < Mathf.Epsilon)
                playerController.jumpPressed = false;

            jumpBufferTimer -= Time.deltaTime;

            Flip();
        }

        public void Move(InputAction.CallbackContext context)
        {
            playerController.moveDirection = context.ReadValue<Vector2>();
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                bool canJump = false;

                if (coyoteTimer > Mathf.Epsilon)
                    canJump = true;
                else if (playerController.isWalled && playerController.GetCurrentState() is WallSlide)
                    canJump = true;
                else if (remainingExtraJumps > 0)
                {
                    remainingExtraJumps--;
                    canJump = true;
                }

                if (canJump)
                {
                    playerController.jumpPressed = true;
                    jumpBufferTimer = jumpBuffer;
                }
            }

            else if (context.canceled && playerController.GetCurrentState() is Jump)
            {
                playerController.jumpCutted = true;
                return;
            }
        }

        public void Dash(InputAction.CallbackContext context)
        {
            if (context.performed && remainingDashes > 0)
            {
                remainingDashes--;

                playerController.dashPressed = true;
            }
        }

        public void Crouch(InputAction.CallbackContext context)
        {
            if (context.performed)
                playerController.isCrouching = true;
            else
                playerController.isCrouching = false;
        }

        public void GreenNote(InputAction.CallbackContext context)
        {
            if (context.performed)
                GuitarManager.InvokeNotePlayed(MusicalNote.Verde);
        }
        private void Flip()
        {
            if (playerController.moveDirection.x > 0 && !playerController.isFacingRight || playerController.moveDirection.x < 0 && playerController.isFacingRight)
            {
                playerController.isFacingRight = !playerController.isFacingRight;

                Vector2 localScale = playerTransform.localScale;
                localScale.x *= -1;
                playerTransform.localScale = localScale;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

            Gizmos.color = Color.red;
            Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
        }
    }
}