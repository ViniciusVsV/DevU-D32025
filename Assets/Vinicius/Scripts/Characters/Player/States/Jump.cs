using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    public class Jump : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private Run runState;
        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCutMultiplier;
        [SerializeField] private float minJumpDuration;

        private float jumpTimer;

        private float appliedForce;

        public override void StateEnter()
        {
            playerController.jumpPressed = false;

            animator.Play(animationClip.name);

            appliedForce = jumpForce;

            if (rb.linearVelocityY < 0)
                appliedForce -= rb.linearVelocityY;

            rb.AddForce(Vector2.up * appliedForce, ForceMode2D.Impulse);

            jumpTimer = minJumpDuration;
        }

        public override void StateUpdate()
        {
            jumpTimer -= Time.deltaTime;

            if (playerController.jumpCutted)
                rb.linearVelocityY *= jumpCutMultiplier;

            // Transição para Jump
            if (playerController.jumpPressed && jumpTimer <= Mathf.Epsilon)
                playerController.SetJump(true);

            // Transição para Dash
            else if (playerController.dashPressed)
                playerController.SetDash();

            // Transição para Wall Slide
            else if (playerController.isWallSliding && jumpTimer <= Mathf.Epsilon)
                playerController.SetWallSlide();

            // Transição para Fall
            else if (rb.linearVelocityY < -0.1f)
                playerController.SetFall();
        }

        public override void StateFixedUpdate()
        {
            runState.StateFixedUpdate();
        }
    }
}