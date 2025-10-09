using System;
using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    public class WallJump : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private Run runState;
        [SerializeField] private AnimationClip animationClip;

        [Header("||==== Objects =====||")]
        [SerializeField] private LayerMask terrainLayers;

        [Header("||===== Parameters =====||")]
        [SerializeField] private Vector2 wallJumpForce;
        [SerializeField] private float minJumpDuration;
        private float jumpTimer;

        [Range(0, 1f)][SerializeField] private float runLerp;

        private Vector2 appliedForce;
        private int direction;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.magenta;

            rb.AddForce(-rb.linearVelocity, ForceMode2D.Impulse);

            direction = playerController.isFacingRight ? 1 : -1;

            appliedForce = wallJumpForce;

            //Se estiver na parede, sempre Flipa
            if (Physics2D.CircleCast(transform.position, 0.5f, Vector2.right * direction, 0.2f, terrainLayers))
            {
                appliedForce.x *= direction * -1;

                tr.localScale = new Vector3(tr.localScale.x * -1, tr.localScale.y, tr.localScale.z);
                playerController.isFacingRight = !playerController.isFacingRight;
            }
            else
            {
                //Se NÃO está na parede e não há um input, flipa
                if (playerController.moveDirection.x == 0)
                {
                    appliedForce.x *= direction * -1;

                    tr.localScale = new Vector3(tr.localScale.x * -1, tr.localScale.y, tr.localScale.z);
                    playerController.isFacingRight = !playerController.isFacingRight;
                }
                else
                    appliedForce.x *= direction;
            }

            rb.AddForce(appliedForce, ForceMode2D.Impulse);

            jumpTimer = minJumpDuration;

            runState.SetLerp(runLerp);
        }

        public override void StateUpdate()
        {
            jumpTimer -= Time.deltaTime;

            // Transição para Jump
            if (playerController.jumpPressed && jumpTimer <= Mathf.Epsilon)
                playerController.SetJump();

            // Transição para Dash
            else if (playerController.dashPressed)
                playerController.SetDash();

            // Transição pra Knockback
            else if (playerController.tookKnockback)
                playerController.SetKnockback();

            // Transição para Wall Slide
            else if (playerController.isWalled && jumpTimer <= Mathf.Epsilon)
                playerController.SetWallSlide();

            // Transição para Fall
            else if (rb.linearVelocityY < -0.1f)
                playerController.SetFall();
        }

        public override void StateFixedUpdate()
        {
            runState.StateFixedUpdate();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * direction);
        }
    }
}