using System;
using System.Collections;
using Effects.Complex.Scenes;
using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    // Spawn no inicio do jogo (do menu inicial)
    public class Spawn : BaseState
    {
        private StateController playerController => (StateController)controller;

        [Header("||===== Objects =====||")]
        [SerializeField] private InputHandler inputHandler;
        private GameEnterEffects gameEnterEffects;

        public static event Action OnPlayerSpawned;

        private void Start()
        {
            gameEnterEffects = GameEnterEffects.Instance;
        }

        public override void StateEnter()
        {
            rb.simulated = false;

            OnPlayerSpawned?.Invoke();

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            while (gameEnterEffects == null)
                yield return null;

            gameEnterEffects.ApplyEffects();

            // Espera o tempo da transição
            while (!gameEnterEffects.finishedPlaying)
                yield return null;

            inputHandler.isEnabled = true;
            rb.simulated = true;

            // Transição para Idle
            playerController.SetIdle();
        }
    }
}