using Effects.Complex.Enemies;
using StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies.VynilDisc.States
{
    public class Respawn : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [Header("||===== Objects =====||")]
        [SerializeField] private NavMeshAgent navMeshAgent;

        private RespawnEffects respawnEffects;
        private Vector2 spawnPoint;

        private void Awake()
        {
            spawnPoint = transform.position;
        }

        private void Start()
        {
            respawnEffects = RespawnEffects.Instance;
        }

        public override void StateEnter()
        {
            vynilDiscController.isAggroed = false;
            vynilDiscController.firstTimeAggroed = true;

            navMeshAgent.enabled = true;

            // Restaura a posição
            tr.position = spawnPoint;

            // Restaura o sprite
            spriteRenderer.enabled = true;

            respawnEffects.ApplyEffects(spriteRenderer);

            // Transição para Idle
            vynilDiscController.SetIdle();
        }
    }
}