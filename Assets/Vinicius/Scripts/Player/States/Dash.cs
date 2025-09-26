using UnityEngine;
using StateMachine;

namespace Player.States
{
    public class Dash : BaseState
    {
        private Controller playerController => (Controller)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("----- Parameters -----")]
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDuration;
        private float dashTimer;
        private int direction;
        private float baseGravityScale;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.yellow;

            direction = playerController.isFacingRight ? 1 : -1;

            baseGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;

            rb.linearVelocity = new Vector2(dashSpeed * direction, 0);

            dashTimer = dashDuration;
        }

        public override void StateUpdate()
        {
            if (dashTimer > Mathf.Epsilon)
                dashTimer -= Time.deltaTime;

            else
            {
                rb.gravityScale = baseGravityScale;
                rb.linearVelocity = Vector2.zero;

                // Transição para Idle
                if (playerController.isGrounded)
                    playerController.SetIdle();

                // Transição para Fall
                else
                    playerController.SetFall();
            }

        }
    }
}