using System;
using System.Collections;
using Effects.Complex.Player;
using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    public class Die : BaseState
    {
        private StateController playerController => (StateController)controller;

        [Header("||===== Objects =====||")]
        [SerializeField] private InputHandler inputHandler;
        private DeathEffects deathEffects;

        public static event Action OnPlayerDied;

        private void Start()
        {
            deathEffects = DeathEffects.Instance;
        }

        public override void StateEnter()
        {
            //Desativa tudo
            animator.Play("Idle", 0, 0);

            inputHandler.isEnabled = false;

            rb.simulated = false;
            rb.linearVelocity = Vector2.zero;

            deathEffects.ApplyEffects(tr.position);

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            while (!deathEffects.finishedHitStopping)
                yield return null;

            spriteRenderer.enabled = false;

            OnPlayerDied?.Invoke();

            while (!deathEffects.finishedPlaying)
                yield return null;

            // Transição para Respawn
            playerController.SetRespawn();
        }
    }
}