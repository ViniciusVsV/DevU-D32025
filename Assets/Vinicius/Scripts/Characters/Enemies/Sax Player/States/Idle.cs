using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Idle : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        private int beatCounter => saxPlayerController.beatCounter;

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.blue;

            saxPlayerController.moveDirection = Random.Range(0, 2) > 0 ? 1 : -1;
        }

        public override void StateUpdate()
        {
            if (saxPlayerController.beatHappened)
            {
                // Transição para Move
                if (beatCounter == 1 || beatCounter == 3)
                    saxPlayerController.SetMove();
            }
        }
    }
}