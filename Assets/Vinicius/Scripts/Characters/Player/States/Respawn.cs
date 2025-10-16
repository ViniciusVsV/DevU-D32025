using System;
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    // Spawn durante o jogo (após morrer)
    public class Respawn : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private InputHandler inputHandler;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float respawnDuration;
        
        public static event Action OnPlayerRespawned;

        public override void StateEnter()
        {
            OnPlayerRespawned?.Invoke();

            // Ativar sprite e state controller
            spriteRenderer.enabled = true;
            playerController.enabled = true;

            // Transição para Idle
            playerController.SetIdle();

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            yield return new WaitForSeconds(respawnDuration);

            // Tirar o efeito de transição
            //

            // Ativar física e input handler
            rb.simulated = true;
            inputHandler.enabled = true;
        }
    }
}