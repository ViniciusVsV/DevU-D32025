using UnityEngine;
using StateMachine;

namespace Characters.Player.States
{
    public class Dash : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private Run runState;
        [SerializeField] private AnimationClip animationClip;

        [Header("----- Parameters -----")]
        [SerializeField] private float mainDashSpeed;
        [SerializeField] private float secondaryDashSpeed;
        [SerializeField] private float mainDashDuration;
        [SerializeField] private float secondayDashDuration;

        [Range(0, 1f)][SerializeField] private float runLerp;

        private int direction;
        private float baseGravityScale;

        private float dashTimer;
        private bool dashing;

        public bool isOnSecondStage;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.yellow;

            if (rb.linearVelocity != Vector2.zero)
                rb.AddForce(-rb.linearVelocity, ForceMode2D.Impulse);

            direction = playerController.isFacingRight ? 1 : -1;

            baseGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;

            dashing = true;
            dashTimer = 0f;
            isOnSecondStage = false;

            rb.linearVelocity = new Vector2(direction * mainDashSpeed, 0);

            runState.SetLerp(runLerp);
        }

        public override void StateFixedUpdate()
        {
            if (!dashing) return;

            dashTimer += Time.fixedDeltaTime;

            if (!isOnSecondStage)
            {
                rb.linearVelocity = new Vector2(direction * mainDashSpeed, 0);

                if (dashTimer >= mainDashDuration)
                {
                    isOnSecondStage = true;
                    dashTimer = 0f;

                    rb.gravityScale = baseGravityScale;
                    rb.linearVelocity = Vector2.zero;
                    rb.AddForce(new Vector2(direction * secondaryDashSpeed, 0), ForceMode2D.Impulse);
                }
            }

            else
            {
                runState.StateFixedUpdate();

                if (dashTimer >= secondayDashDuration)
                {
                    dashing = false;

                    // Transição para Dash
                    if (playerController.dashPressed)
                        playerController.SetDash();

                    // Transição para Idle
                    else if (playerController.isGrounded)
                        playerController.SetIdle();

                    // Transição para Fall
                    else
                        playerController.SetFall();
                }
            }
        }

        public override void StateExit()
        {
            dashing = false;
            rb.gravityScale = baseGravityScale;
        }
    }
}
