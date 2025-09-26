using Player.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputReader : MonoBehaviour, IRythmSyncable
    {
        [SerializeField] private Controller playerController;
        private Transform playerTransform;

        [Header("||===== Rythm Parameters =====||")]
        [SerializeField] private float maxBeatDeviation;
        private float beatLength;
        private float timeSinceLastBeat;
        private float timeUntilNextBeat;

        [Header("||===== Jump Parameters =====||")]
        [SerializeField] private int extraJumps;
        private int remainingExtraJumps;

        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Vector2 groundCheckSize;
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private float coyoteDuration;
        private float coyoteTimer;

        [Header("||===== Dash Parameters =====||")]
        [SerializeField] private float regularDashCooldown;
        [SerializeField] private float onBeatDashCooldown;
        private float dashCooldownTimer;

        [Header("||===== Wall Slide/Jump Parameter =====||")]
        [SerializeField] private Transform wallCheckPoint;
        [SerializeField] private Vector2 wallCheckSize;
        [SerializeField] private LayerMask wallLayer;

        private void Awake()
        {
            playerTransform = transform.parent;
        }

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
        }

        private void Update()
        {
            timeSinceLastBeat += Time.deltaTime;
            timeUntilNextBeat -= Time.deltaTime;

            playerController.isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
            playerController.isWalled = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer) && !playerController.isGrounded;

            if (playerController.isGrounded)
            {
                coyoteTimer = coyoteDuration;
                remainingExtraJumps = extraJumps;
            }
            else
                coyoteTimer -= Time.deltaTime;

            if (dashCooldownTimer > Mathf.Epsilon)
                dashCooldownTimer -= Time.deltaTime;

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
                    playerController.jumpPresseOnBeat = CheckOnBeat();
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
            if (context.performed && dashCooldownTimer <= Mathf.Epsilon)
            {
                dashCooldownTimer = CheckOnBeat() ? onBeatDashCooldown : regularDashCooldown;

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

        private bool CheckOnBeat()
        {
            return timeSinceLastBeat < maxBeatDeviation || timeUntilNextBeat < maxBeatDeviation;
        }

        public void RespondToBeat()
        {
            timeSinceLastBeat = 0;
            timeUntilNextBeat = beatLength;
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