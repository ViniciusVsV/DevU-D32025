using System;
using System.Collections;
using Effects.Complex.Player;
using StateMachine;
using Unity.Cinemachine;
using UnityEngine;

namespace Characters.Player.States
{
    // Spawn durante o jogo (após morrer)
    public class Respawn : BaseState
    {
        private StateController playerController => (StateController)controller;

        [Header("||===== Objects =====||")]
        [SerializeField] private InputHandler inputHandler;
        private RespawnEffects respawnEffects;

        public static event Action OnPlayerRespawned;

        private void Start()
        {
            respawnEffects = RespawnEffects.Instance;
        }

        public override void StateEnter()
        {
            OnPlayerRespawned?.Invoke();

            // Ativar sprite e state controller
            spriteRenderer.enabled = true;
            playerController.enabled = true;

            // Transição para Idle
            playerController.SetIdle();

            respawnEffects.ApplyEffects();

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            rb.simulated = true;

            while (!respawnEffects.finishedPlaying)
                yield return null;

            // Ativar input handler
            inputHandler.enabled = true;
        }
    }
}