using UnityEngine;
using StateMachine;

namespace Player.States
{
    public class Crouch : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        public override void StateEnter()
        {
            rb.linearVelocityX = 0;

            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.magenta;
        }

        public override void StateUpdate()
        {
            // Transição para Dash
            if (playerController.dashPressed)
                playerController.SetDash();

            // Transição para Knockback
            else if (playerController.tookKnockback)
                playerController.SetKnockback();

            // Transição para Idle
            else if (!playerController.isCrouching)
                playerController.SetIdle();

            // Transição para Fall
            else if (rb.linearVelocityY < 0)
                playerController.SetFall();
        }
    }
}