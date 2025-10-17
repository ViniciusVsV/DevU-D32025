using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Die : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [SerializeField] private GameObject notesObject;

        public override void StateEnter()
        {
            spriteRenderer.enabled = false;

            notesObject.SetActive(false);
        }

        public override void StateUpdate()
        {
            // Transição para Respawn
            if (saxPlayerController.restored)
                saxPlayerController.SetRespawn();
        }
    }
}