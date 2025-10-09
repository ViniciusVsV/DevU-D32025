using StateMachine;
using UnityEngine;

namespace Characters.Player.States
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

            if (rb.linearVelocity != Vector2.zero)
                rb.AddForce(-rb.linearVelocity, ForceMode2D.Impulse);

            rb.AddForce(playerController.knockbackDirection * knockbackStrength, ForceMode2D.Impulse);

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