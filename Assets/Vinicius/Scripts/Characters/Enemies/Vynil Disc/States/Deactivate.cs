using UnityEngine;
using StateMachine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Deactivate : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        public override void StateEnter()
        {
            vynilDiscController.isAggroed = false;

            spriteRenderer.color = Color.black;
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