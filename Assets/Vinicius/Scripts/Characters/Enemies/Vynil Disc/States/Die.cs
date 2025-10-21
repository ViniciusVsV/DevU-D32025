using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Die : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [Header("||===== Objects ====||")]
        [SerializeField] private NoteSequence noteSequence;

        public override void StateEnter()
        {
            vynilDiscController.isDead = true;

            vynilDiscController.isAggroed = false;

            spriteRenderer.enabled = false;

            noteSequence.Deactivate();
        }

        public override void StateUpdate()
        {
            // Transição para Respawn
            if (vynilDiscController.restored)
                vynilDiscController.SetRespawn();
        }

        public override void StateExit()
        {
            vynilDiscController.isDead = false;
        }
    }
}