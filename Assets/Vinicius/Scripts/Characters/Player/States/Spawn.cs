using System;
using System.Collections;
using Effects.Complex.Player;
using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    // Spawn no inicio do jogo (do menu inicial)
    public class Spawn : BaseState
    {
        private StateController playerController => (StateController)controller;

        private SpawnEffects spawnEffects;

        public static event Action OnPlayerSpawned;

        private void Start()
        {
            spawnEffects = SpawnEffects.Instance;
        }

        public override void StateEnter()
        {
            rb.simulated = false;

            OnPlayerSpawned?.Invoke();

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            while (spawnEffects == null)
                yield return null;

            spawnEffects.ApplyEffects();

            // Espera o tempo da transição
            while (!spawnEffects.finishedPlaying)
                yield return null;

            rb.simulated = true;

            // Transição para Fall
            playerController.SetIdle();
        }
    }
}