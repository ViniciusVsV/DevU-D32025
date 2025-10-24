using Effects.Complex.Enemies;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Die : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [Header("||===== Objects =====||")]
        [SerializeField] private NoteSequence noteSequence;
        private DeathEffects deathEffects;

        private void Start()
        {
            deathEffects = DeathEffects.Instance;
        }

        public override void StateEnter()
        {
            saxPlayerController.isDead = true;

            AudioController.Instance.PlaySaxPlayerDeathSFX();

            deathEffects.ApplyEffects(tr.position, spriteRenderer);

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