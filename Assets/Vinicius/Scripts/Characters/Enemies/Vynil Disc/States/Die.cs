using Effects.Complex.Enemies;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Die : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [Header("||===== Objects ====||")]
        [SerializeField] private NoteSequence noteSequence;
        private DeathEffects deathEffects;

        private void Start()
        {
            deathEffects = DeathEffects.Instance;
        }

        public override void StateEnter()
        {
            vynilDiscController.isDead = true;

            vynilDiscController.isAggroed = false;

            deathEffects.ApplyEffects(tr.position, spriteRenderer);

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