using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class Knockback : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float knockbackStrength;
        [SerializeField] private float knockbackDuration;
        private float knockbackTimer;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.gray;

            rb.linearVelocity = playerController.knockbackDirection * knockbackStrength;

            knockbackTimer = knockbackDuration;
        }

        public override void StateUpdate()
        {
            if (knockbackTimer > Mathf.Epsilon)
                knockbackTimer -= Time.deltaTime;

            else
            {
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