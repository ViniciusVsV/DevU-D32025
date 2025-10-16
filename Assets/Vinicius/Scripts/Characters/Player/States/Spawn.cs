using System;
using System.Collections;
using StateMachine;
using UnityEngine;

namespace Characters.Player.States
{
    // Spawn no inicio do jogo (do menu inicial)
    public class Spawn : BaseState
    {
        private StateController playerController => (StateController)controller;

        [SerializeField] private float spawnDuration;

        public static event Action OnPlayerSpawned;

        public override void StateEnter()
        {
            rb.simulated = false;

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            OnPlayerSpawned?.Invoke();

            // Espera o tempo da transição
            yield return new WaitForSeconds(spawnDuration);

            rb.simulated = true;

            // Transição para Fall
            playerController.SetIdle();
        }
    }
}