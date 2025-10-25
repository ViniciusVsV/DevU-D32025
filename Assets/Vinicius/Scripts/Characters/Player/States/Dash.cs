using UnityEngine;
using StateMachine;
using Effects.Complex.Player;

namespace Characters.Player.States
{
    public class Dash : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private Run runState;
        [SerializeField] private AnimationClip animationClip;
        private MovementEffects movementEffects;

        [Header("----- Parameters -----")]
        [SerializeField] private float mainSpeed;
        [SerializeField] private float secondarySpeed;

        [SerializeField] private float mainDuration;
        [SerializeField] private float secondaryDuration;
        private float totalDuration;
        private float dashTimer;

        [Range(0, 1f)][SerializeField] private float runLerp;

        private int direction;
        private float baseGravityScale;
        private bool isOnMainSection;

        private void Awake()
        {
            totalDuration = mainDuration + secondaryDuration;
        }

        private void Start()
        {
            movementEffects = MovementEffects.Instance;
        }

        public override void StateEnter()
        {
            animator.Play(animationClip.name);

            playerController.dashPressed = false;
            playerController.isDashing = true;

            baseGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;

            dashTimer = 0f;

            isOnMainSection = true;

            movementEffects.ApplyDashEffects();

            rb.AddForce(-rb.linearVelocity, ForceMode2D.Impulse);

            direction = playerController.isFacingRight ? 1 : -1;

            rb.linearVelocity = new Vector2(direction * mainSpeed, 0);

            runState.SetLerp(runLerp);
        }

        public override void StateUpdate()
        {
            dashTimer += Time.deltaTime;

            // Transição para Dash
            if (playerController.dashPressed && !isOnMainSection)
                playerController.SetDash();
        }

        public override void StateFixedUpdate()
        {
            if (dashTimer <= mainDuration)
                rb.linearVelocity = new Vector2(direction * mainSpeed, 0);

            else if (dashTimer <= totalDuration)
            {
                if (isOnMainSection)
                {
                    playerController.dashHappened = true;
                    playerController.isDashing = false;

                    rb.gravityScale = baseGravityScale;
                    rb.linearVelocity = Vector2.zero;

                    rb.AddForce(new Vector2(direction * secondarySpeed, 0), ForceMode2D.Impulse);

                    isOnMainSection = false;
                }

                runState.StateFixedUpdate();
            }

            else
            {
                // Transição para Idle
                if (playerController.isGrounded)
                    playerController.SetIdle();

                // Transição para Fall
                else
                    playerController.SetFall();
            }
        }

        public override void StateExit()
        {
            rb.gravityScale = baseGravityScale; // Reforçando o retorno da gravidade ao normal
            playerController.isDashing = false;
        }
    }
}
