using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Idle : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Objects =====||")]
        [SerializeField] private LayerMask terrainLayers;
        [SerializeField] private CircleCollider2D houndCollider;
        private float colliderRadius;


        [Header("||===== Parameters =====||")]
        [SerializeField] private int maxAttempts;
        [SerializeField] private float maxDistance;
        private int attemptCounter;

        private Vector2 initialPosition;
        private Vector2 nextPoint;
        private Vector2 nextVector;

        private bool pathClear;
        private bool pointClear;

        private void Awake()
        {
            initialPosition = transform.position;
            colliderRadius = houndCollider.radius;
        }

        public override void StateEnter()
        {
            //animator.Play(animationClip.name);
            spriteRenderer.color = Color.blue;

            attemptCounter = 0;

            do
            {
                nextPoint = initialPosition + Random.insideUnitCircle * maxDistance;
                nextVector = nextPoint - (Vector2)tr.position;

                pathClear = !Physics2D.CircleCast(tr.position, colliderRadius, nextVector.normalized, nextVector.magnitude, terrainLayers);
                pointClear = !Physics2D.OverlapCircle(nextPoint, colliderRadius, terrainLayers);

                if (pathClear && pointClear)
                    break;

                nextPoint = tr.position;

                attemptCounter++;
            } while (attemptCounter < 30);

            vynilDiscController.followPoint = nextPoint;
        }

        public override void StateUpdate()
        {
            // Transição para Chase
            if (vynilDiscController.isAggroed)
                vynilDiscController.SetChase();

            // Transição para Move
            else if (vynilDiscController.beatHappened)
            {
                if (vynilDiscController.beatCounter == 0 || vynilDiscController.beatCounter == 2)
                    vynilDiscController.SetMove();
            }
        }
    }
}