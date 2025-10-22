using Effects.Complex.Enemies;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Respawn : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [SerializeField] private GameObject notesObject;

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

            // Restaura a posição
            tr.position = spawnPoint;

            // Restaura o sprite
            spriteRenderer.enabled = true;

            respawnEffects.ApplyEffects(spriteRenderer);

            // Restaura as notas
            notesObject.SetActive(true);

            // Transição para Idle
            vynilDiscController.SetIdle();
        }
    }
}