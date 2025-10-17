using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Die : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [SerializeField] private GameObject notesObject;

        public override void StateEnter()
        {
            vynilDiscController.isAggroed = false;

            spriteRenderer.enabled = false;
            
            notesObject.SetActive(false);
        }

        public override void StateUpdate()
        {
            // Transição para Respawn
            if (vynilDiscController.restored)
                vynilDiscController.SetRespawn();
        }
    }
}