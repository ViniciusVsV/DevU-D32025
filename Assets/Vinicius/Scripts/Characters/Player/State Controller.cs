using UnityEngine;
using Characters.Player.States;
using StateMachine;

namespace Characters.Player
{
    public class StateController : BaseStateController
    {
        [HideInInspector] public Vector2 moveDirection;
        [HideInInspector] public Vector2 knockbackDirection;
        [HideInInspector] public Vector2 platformVelocity;

        [Header("||===== States =====||")]
        [SerializeField] private Spawn spawnState;
        [SerializeField] private Idle idleState;
        [SerializeField] private Run runState;
        [SerializeField] private Jump jumpState;
        [SerializeField] private Fall fallState;
        [SerializeField] private Dash dashState;
        [SerializeField] private Crouch crouchState;
        [SerializeField] private WallSlide wallSlideState;
        [SerializeField] private WallJump wallJumpState;
        [SerializeField] private Knockback knockbackState;
        [SerializeField] private Die dieState;
        [SerializeField] private Respawn respawnState;

        [Header("||===== Booleans =====||")]
        public bool jumpPressed;
        public bool wallJumpPressed;
        public bool jumpCutted;
        public bool dashPressed;
        public bool dashHappened;
        public bool tookKnockback;

        public bool isFacingRight;
        public bool isGrounded;
        public bool isDashing;
        public bool isWalled;
        public bool isWallSliding;
        public bool isCrouching;

        protected override void Awake()
        {
            base.Awake();

            spawnState.Setup(rb, transform, animator, spriteRenderer, this);
            idleState.Setup(rb, transform, animator, spriteRenderer, this);
            runState.Setup(rb, transform, animator, spriteRenderer, this);
            jumpState.Setup(rb, transform, animator, spriteRenderer, this);
            fallState.Setup(rb, transform, animator, spriteRenderer, this);
            dashState.Setup(rb, transform, animator, spriteRenderer, this);
            crouchState.Setup(rb, transform, animator, spriteRenderer, this);
            wallSlideState.Setup(rb, transform, animator, spriteRenderer, this);
            wallJumpState.Setup(rb, transform, animator, spriteRenderer, this);
            knockbackState.Setup(rb, transform, animator, spriteRenderer, this);
            dieState.Setup(rb, transform, animator, spriteRenderer, this);
            respawnState.Setup(rb, transform, animator, spriteRenderer, this);

            isFacingRight = true;
        }

        private void Start()
        {
            SetSpawn(false);
        }

        protected override void Update()
        {
            base.Update();

            jumpCutted = false;
            wallJumpPressed = false;
            dashPressed = false;
            tookKnockback = false;
        }

        public void SetSpawn(bool forced = false) => SetNewState(spawnState, forced);
        public void SetIdle(bool forced = false) => SetNewState(idleState, forced);
        public void SetRun(bool forced = false) => SetNewState(runState, forced);
        public void SetJump(bool forced = false) => SetNewState(jumpState, forced);
        public void SetFall(bool forced = false) => SetNewState(fallState, forced);
        public void SetDash(bool forced = false) => SetNewState(dashState, forced);
        public void SetCrouch(bool forced = false) => SetNewState(crouchState, forced);
        public void SetWallSlide(bool forced = false) => SetNewState(wallSlideState, forced);
        public void SetWallJump(bool forced = false) => SetNewState(wallJumpState, forced);
        public void SetKnockback(bool forced = false) => SetNewState(knockbackState, forced);
        public void SetDie(bool forced = false) => SetNewState(dieState, forced);
        public void SetRespawn(bool forced = false) => SetNewState(respawnState, forced);
    }
}