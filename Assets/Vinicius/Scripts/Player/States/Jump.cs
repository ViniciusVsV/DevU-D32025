using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class Jump : BaseState
    {
        private Controller playerController => (Controller)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float regularJumpForce;
        [SerializeField] private float onBeatJumpForce;
        [SerializeField] private float jumpCutMultiplier;

        [Header("||===== Horizontal Movement -----||")]
        [SerializeField] private float moveSpeed;
        private int direction;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.green;

            rb.linearVelocityY = playerController.jumpPresseOnBeat ? onBeatJumpForce : regularJumpForce;
        }

        public override void StateUpdate()
        {
            if (playerController.jumpCutted)
                rb.linearVelocityY *= jumpCutMultiplier;

            // Transição para Jump
            if (playerController.jumpPressed)
                playerController.SetJump(true);

            // Transição para Dash
            else if (playerController.dashPressed)
                playerController.SetDash();

            // Transição para Knockback
            else if (playerController.tookKnockback)
                playerController.SetKnockback();

            // Transição para Fall
            else if (rb.linearVelocityY < 0)
                playerController.SetFall();
        }

        public override void StateFixedUpdate()
        {
            if (playerController.moveDirection.x > 0)
                direction = 1;
            else
                direction = playerController.moveDirection.x < 0 ? -1 : 0;

            rb.linearVelocityX = direction * moveSpeed;
        }
    }
}