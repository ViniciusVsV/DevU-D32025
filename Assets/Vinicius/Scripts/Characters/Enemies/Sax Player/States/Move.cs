using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Move : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [Header("||===== Objects =====||")]
        [SerializeField] private Transform spriteTransform;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private int playSaxCooldown; // Quantidade de 4's de cooldown do ataque

        private int beatCounter => saxPlayerController.beatCounter;
        private int playSaxCounter => saxPlayerController.playSaxCounter;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.yellow;

            if (
                saxPlayerController.moveDirection == 1 && !saxPlayerController.isFacingRight ||
                saxPlayerController.moveDirection == -1 && saxPlayerController.isFacingRight
            )
            {
                spriteTransform.localScale = new Vector3(
                    spriteTransform.localScale.x * -1,
                    spriteTransform.localScale.y,
                    spriteTransform.localScale.z
                );

                saxPlayerController.isFacingRight = !saxPlayerController.isFacingRight;
            }
        }

        public override void StateUpdate()
        {
            if (saxPlayerController.beatHappened)
            {
                if (beatCounter == 0)
                {
                    if (playSaxCounter == playSaxCooldown)
                    {
                        saxPlayerController.playSaxCounter = 0;

                        // Transição para Play Sax
                        saxPlayerController.SetPlaySax();
                    }

                    else
                    {
                        saxPlayerController.playSaxCounter++;
                        saxPlayerController.SetIdle();
                    }
                }

                // Transição para Idle
                else
                    saxPlayerController.SetIdle();
            }
        }

        public override void StateFixedUpdate()
        {
            rb.linearVelocityX = moveSpeed * saxPlayerController.moveDirection;
        }

        public override void StateExit()
        {
            rb.linearVelocityX = 0;
        }
    }
}