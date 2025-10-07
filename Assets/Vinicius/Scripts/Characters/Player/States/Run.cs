using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    public class Run : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private AnimationClip regularRunClip;
        [SerializeField] private AnimationClip fastRunClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float maxSpeed;
        [SerializeField] private float groundAcceleration;
        [SerializeField] private float groundDeceleration;

        [SerializeField] private float airAcceleration;
        [SerializeField] private float airDeceleration;

        private float targetSpeed;
        private float accelerationRate;
        private float speedDiff;
        private int direction;

        private float lerpAmount;

        public override void StateEnter()
        {
            if (Mathf.Abs(rb.linearVelocityX) > maxSpeed)
            {
                //animator.Play(fastRunClip.name);
                spriteRenderer.color = Color.green;
            }
            else
            {
                //animator.Play(regularRunClip.name);
                spriteRenderer.color = Color.cyan;
            }

            lerpAmount = 1;
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

            // Transição para Idle
            else if (Mathf.Abs(playerController.moveDirection.x) <= 0.01f)
                playerController.SetIdle();

            // Transição para Crouch
            else if (playerController.isCrouching)
                playerController.SetCrouch();

            // Transição para Fall
            else if (rb.linearVelocityY < -0.1f)
                playerController.SetFall();
        }

        public override void StateFixedUpdate()
        {
            if (playerController.moveDirection.x != 0)
                direction = playerController.moveDirection.x > 0 ? 1 : -1;
            else
                direction = 0;

            targetSpeed = direction * maxSpeed;

            targetSpeed = Mathf.Lerp(rb.linearVelocityX, targetSpeed, lerpAmount);

            if (playerController.isGrounded)
                accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? groundAcceleration : groundDeceleration;
            else
                accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? airAcceleration : airDeceleration;

            if (Mathf.Abs(rb.linearVelocityX) > Mathf.Abs(targetSpeed)
                && Mathf.Sign(rb.linearVelocityX) == Mathf.Sign(targetSpeed)
                && Mathf.Abs(playerController.moveDirection.x) > 0.01f
                && Mathf.Sign(playerController.moveDirection.x) == Mathf.Sign(rb.linearVelocityX))
            {
                accelerationRate = 0;
            }

            speedDiff = targetSpeed - rb.linearVelocityX;

            rb.AddForce(speedDiff * accelerationRate * Vector2.right, ForceMode2D.Force);
        }

        public void SetLerp(float newLerp) { lerpAmount = newLerp; }
    }
}