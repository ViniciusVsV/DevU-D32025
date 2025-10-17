using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class Respawn : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [SerializeField] private GameObject notesObject;

        private Vector2 spawnPoint;

        private void Awake()
        {
            spawnPoint = transform.position;
        }

        public override void StateEnter()
        {
            // Restaura a posição
            tr.position = spawnPoint;

            // Restaura o sprite
            spriteRenderer.enabled = true;

            // Restaura as notas
            notesObject.SetActive(true);

            // Transição para Idle
            saxPlayerController.SetIdle();
        }
    }
}