using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class WallSlide : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float slideSpeed;
        [SerializeField] private float acceleration;

        private float baseGravityScale;
        private float speedDiff;
        private float forceToApply;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.black;

            baseGravityScale = rb.gravityScale;
            rb.gravityScale = 0;

            if (rb.linearVelocityY != 0)
                rb.AddForce(-rb.linearVelocityY * Vector2.up, ForceMode2D.Impulse);
        }

        public override void StateUpdate()
        {
            // Transição para Wall Jump
            if (playerController.jumpPressed)
                playerController.SetWallJump();

            // Transição para Dash
            else if (playerController.dashPressed)
                playerController.SetDash();

            // Transição para Knockback
            else if (playerController.tookKnockback)
                playerController.SetKnockback();

            // Transição para Fall
            else if (!playerController.isWalled && !playerController.isGrounded)
                playerController.SetFall();

            // Transição para Idle
            else if (playerController.isGrounded)
                playerController.SetIdle();
        }

        public override void StateFixedUpdate()
        {
            speedDiff = slideSpeed - rb.linearVelocityY;

            forceToApply = speedDiff * -acceleration;
            forceToApply = Mathf.Clamp(forceToApply, -Mathf.Abs(speedDiff) * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDiff) * (1 / Time.fixedDeltaTime));

            rb.AddForce(forceToApply * Vector2.up);
        }

        public override void StateExit()
        {
            rb.gravityScale = baseGravityScale;
        }
    }
}