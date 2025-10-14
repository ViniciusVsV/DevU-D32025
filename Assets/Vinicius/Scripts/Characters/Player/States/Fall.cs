using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    public class Fall : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private Run runState;
        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float newGravityScale;
        private float baseGravityScale;

        public override void StateEnter()
        {
            animator.Play(animationClip.name);

            baseGravityScale = rb.gravityScale;

            rb.gravityScale = newGravityScale;
        }

        public override void StateUpdate()
        {
            // Transição para Jump
            if (playerController.jumpPressed)
                playerController.SetJump();

            // Transição para Wall Jump
            if (playerController.wallJumpPressed)
                playerController.SetWallJump();

            // Transição para Dash
            else if (playerController.dashPressed)
                playerController.SetDash();

            // Transição para Knockback
            else if (playerController.tookKnockback)
                playerController.SetKnockback();

            // Transição para Idle
            else if (playerController.isGrounded)
                playerController.SetIdle();

            //Transição para Wall Slide
            else if (playerController.isWallSliding)
                playerController.SetWallSlide();
        }

        public override void StateFixedUpdate()
        {
            runState.StateFixedUpdate();
        }

        public override void StateExit()
        {
            rb.gravityScale = baseGravityScale;
        }
    }
}