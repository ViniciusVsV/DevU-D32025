using UnityEngine;
using StateMachine;
using UnityEngine.Events;

namespace Characters.Enemies.VynilDisc.States
{
    public class Deactivate : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [Header("||===== Objects ====||")]
        [SerializeField] private NoteSequence noteSequence;

        public override void StateEnter()
        {
            vynilDiscController.isAggroed = false;

            noteSequence.Deactivate();
        }

        public override void StateUpdate()
        {
            // Transição para Respawn
            if (vynilDiscController.restored)
                vynilDiscController.SetRespawn();

            // Transição para Idle
            else if (vynilDiscController.activated)
                vynilDiscController.SetIdle();
        }
    }
}