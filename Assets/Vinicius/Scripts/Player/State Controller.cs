using UnityEngine;
using Player.States;
using StateMachine;

namespace Player
{
    public class StateController : BaseStateController
    {
        [HideInInspector] public Vector2 moveDirection;
        [HideInInspector] public Vector2 knockbackDirection;

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

        [Header("||===== Booleans =====||")]
        public bool jumpPressed;
        public bool jumpCutted;
        public bool dashPressed;
        public bool tookKnockback;

        public bool isFacingRight;
        public bool isGrounded;
        public bool isWalled;
        public bool isWallSliding;
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

            isFacingRight = true;
        }

        protected override void Update()
        {
            base.Update();

            jumpCutted = false;
            dashPressed = false;
            tookKnockback = false;
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
    }
}