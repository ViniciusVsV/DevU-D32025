using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class WallSlide : BaseState
    {
        private Controller playerController => (Controller)controller;

        [SerializeField] private AnimationClip animationClip;

        [SerializeField] private float wallSlideSpeed;
        private float baseGravityScale;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.black;

            baseGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;

            rb.linearVelocityY = -1 * wallSlideSpeed;
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

        public override void StateExit()
        {
            rb.gravityScale = baseGravityScale;
        }
    }
}