using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class Idle : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private AnimationClip idleClip;
        [SerializeField] private AnimationClip stopRunClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float deceleration;
        private float speedDiff;

        public override void StateEnter()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fast Run"))
            {
                //animator.Play(stopRunClip.name);
                spriteRenderer.color = Color.black;
            }
            else
            {
                //animator.Play(idleClip.name);
                spriteRenderer.color = Color.blue;
            }
        }

        public override void StateUpdate()
        {
            // Transição para Jump
            if (playerController.jumpPressed)
                playerController.SetJump();

            // Transição para Dash
            else if (playerController.dashPressed)
                playerController.SetDash();

            // Transição para Knockback
            else if (playerController.tookKnockback)
                playerController.SetKnockback();

            // Transição para Run
            else if (Mathf.Abs(playerController.moveDirection.x) > 0.01f)
                playerController.SetRun();

            // Transição para Crouch
            else if (playerController.isCrouching)
                playerController.SetCrouch();

            // Transição para Fall
            else if (rb.linearVelocityY < -0.1f)
                playerController.SetFall();
        }

        public override void StateFixedUpdate()
        {
            speedDiff = 0 - rb.linearVelocityX;

            rb.AddForce(speedDiff * deceleration * Vector2.right, ForceMode2D.Force);
        }
    }
}