using UnityEngine;
using StateMachine;
using UnityEngine.Events;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Deactivate : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [Header("||===== Objects ====||")]
        [SerializeField] private NoteSequence noteSequence;

        public override void StateEnter()
        {
            saxPlayerController.playSaxCounter = 0;

            noteSequence.Deactivate();
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