using UnityEngine;
using StateMachine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Deactivate : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        public override void StateEnter()
        {
            // Não faz nada

            spriteRenderer.color = Color.black;
        }

        public override void StateUpdate()
        {
            // Transição para Respawn
            if (saxPlayerController.restored)
                saxPlayerController.SetRespawn();

            // Transição para Idle
            else if (saxPlayerController.activated)
                saxPlayerController.SetIdle();
        }
    }
}