using System;
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    public class Die : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private InputHandler inputHandler;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float respawnDelay;

        public static event Action OnPlayerDied;

        public override void StateEnter()
        {
            //Desativa tudo
            playerController.enabled = false;
            inputHandler.enabled = false;
            spriteRenderer.enabled = false;

            rb.simulated = false;
            rb.linearVelocity = Vector2.zero;

            // Desativa os inimigos (evento, talvez)
            OnPlayerDied?.Invoke();

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            yield return new WaitForSeconds(respawnDelay);

            // Ativar efeito de transição
            //

            // Transição para Respawn
            playerController.SetRespawn();
        }
    }
}