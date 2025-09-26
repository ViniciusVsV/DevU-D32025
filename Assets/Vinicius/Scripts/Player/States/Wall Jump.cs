using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class WallJump : BaseState
    {
        private Controller playerController => (Controller)controller;

        [SerializeField] private AnimationClip animationClip;

        [SerializeField] private Vector2 baseWallJumpForce;
        private Vector2 currentWallJumpForce;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.magenta;

            currentWallJumpForce = baseWallJumpForce;
            currentWallJumpForce.x *= playerController.isFacingRight ? -1 : 1;

            rb.linearVelocity = currentWallJumpForce;

            tr.localScale = new Vector3(tr.localScale.x * -1, tr.localScale.y, tr.localScale.z);
            playerController.isFacingRight = !playerController.isFacingRight;
        }

        public override void StateUpdate()
        {
            // Transição para Jump
            if (playerController.jumpPressed)
                playerController.SetJump();

            // Transição para Dash
            else if (playerController.dashPressed)
                playerController.SetDash();

            // Transição pra Knockback
            else if (playerController.tookKnockback)
                playerController.SetKnockback();

            // Transição para Fall
            else if (rb.linearVelocityY < 0)
                playerController.SetFall();
        }
    }
}