using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Respawn : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

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

            // Transição para Idle
            vynilDiscController.SetIdle();
        }
    }
}