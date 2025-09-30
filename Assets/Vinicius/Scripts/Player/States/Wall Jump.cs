using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class WallJump : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private Run runState;
        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private Vector2 wallJumpForce;
        [SerializeField] private float minJumpDuration;
        private float jumpTimer;

        [Range(0, 1f)][SerializeField] private float runLerp;

        private Vector2 appliedForce;

        public override void StateEnter()
        {
            playerController.jumpPressed = false;

            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.magenta;

            appliedForce = wallJumpForce;
            appliedForce.x *= playerController.isFacingRight ? -1 : 1;

            rb.AddForce(appliedForce, ForceMode2D.Impulse);

            tr.localScale = new Vector3(tr.localScale.x * -1, tr.localScale.y, tr.localScale.z);
            playerController.isFacingRight = !playerController.isFacingRight;

            jumpTimer = minJumpDuration;

            runState.SetLerp(runLerp);
        }

        public override void StateUpdate()
        {
            jumpTimer -= Time.deltaTime;

            // Transição para Jump
            if (playerController.jumpPressed && jumpTimer <= Mathf.Epsilon)
                playerController.SetJump();

            // Transição para Dash
            else if (playerController.dashPressed)
                playerController.SetDash();

            // Transição pra Knockback
            else if (playerController.tookKnockback)
                playerController.SetKnockback();

            // Transição para Wall Slide
            else if (playerController.isWalled && jumpTimer <= Mathf.Epsilon)
                playerController.SetWallSlide();

            // Transição para Fall
            else if (rb.linearVelocityY < 0)
                playerController.SetFall();
        }

        public override void StateFixedUpdate()
        {
            runState.StateFixedUpdate();
        }
    }
}