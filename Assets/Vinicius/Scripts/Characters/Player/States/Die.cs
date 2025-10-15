using System.Collections;
using StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters.Player.States
{
    public class Die : BaseState
    {
        private StateController playerController => (StateController)controller;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float respawnDelay;
        [SerializeField] private float respawnDuration;

        public override void StateEnter()
        {
            Debug.Log("Entrou no estado de morte!");

            //Desativa tudo
            playerController.enabled = false;
            spriteRenderer.enabled = false;

            rb.simulated = false;
            rb.linearVelocity = Vector2.zero;

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            yield return new WaitForSeconds(respawnDelay);

            // Restaurar todos os objetos / inimigos ativos

            // Ativar efeito de transição

            // Mover o jogador para a posição do spawn point
            tr.position = CheckpointManager.Instance.GetSpawnPoint();

            // Ativar o sprite
            spriteRenderer.enabled = true;

            yield return new WaitForSeconds(respawnDuration);

            // Tirar o efeito de transição

            // Ativar os controles
            playerController.enabled = true;
            rb.simulated = true;

            // Transição para Idle
            playerController.SetIdle();
        }
    }
}