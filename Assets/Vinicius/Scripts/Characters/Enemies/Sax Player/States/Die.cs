using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Die : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [Header("||===== Objects =====||")]
        [SerializeField] private NoteSequence noteSequence;

        public override void StateEnter()
        {
            saxPlayerController.isDead = true;

            spriteRenderer.enabled = false;

            noteSequence.Deactivate();
        }

        public override void StateUpdate()
        {
            // Transição para Respawn
            if (saxPlayerController.restored)
                saxPlayerController.SetRespawn();
        }

        public override void StateExit()
        {
            saxPlayerController.isDead = false;
        }
    }
}