using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Move : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float moveSpeed;

        [SerializeField] private int playSaxCooldown; // Quantidade de 4's de cooldown do ataque
        private int playSaxCountdown;

        private int beatCounter => saxPlayerController.beatCounter;

        private void Awake()
        {
            playSaxCountdown = playSaxCooldown;
        }

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.yellow;

            if (
                saxPlayerController.moveDirection == 1 && !saxPlayerController.isFacingRight ||
                saxPlayerController.moveDirection == -1 && saxPlayerController.isFacingRight
            )
            {
                tr.localScale = new Vector3(
                    tr.localScale.x * -1,
                    tr.localScale.y,
                    tr.localScale.z
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
                    if (playSaxCountdown == 0)
                    {
                        playSaxCountdown = playSaxCooldown;

                        // Transição para Play Sax
                        saxPlayerController.SetPlaySax();
                    }

                    else
                    {
                        playSaxCountdown--;
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