using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    public class Idle : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private AnimationClip idleClip;
        [SerializeField] private AnimationClip landClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float deceleration;
        private float speedDiff;

        public override void StateEnter()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                animator.Play(landClip.name);
            }
            else
            {
                animator.Play(idleClip.name);
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
            if (playerController.platformVelocity != Vector2.zero)
                rb.linearVelocity = playerController.platformVelocity;

            else
            {
                speedDiff = 0 - rb.linearVelocityX;

                rb.AddForce(speedDiff * deceleration * Vector2.right, ForceMode2D.Force);
            }
        }
    }
}