using System.Collections.Generic;
using Effects.Complex.Enemies.VynilDisc;
using StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies.VynilDisc.States
{
    public class Chase : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Objects =====||")]
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private NavMeshAgent navMeshAgent;

        [SerializeField] private LayerMask terrainLayers;
        [SerializeField] private CircleCollider2D discCollider;
        private float colliderRadius;

        [SerializeField] private Transform exclamationPoint;

        private AggroEffects aggroEffects;

        [Header("||===== Parameters =====||")]
        [SerializeField] private int chargeBeatsCooldown;
        private int chargeBeatCounter;
        [SerializeField] private float chargeThreshold;

        [Range(0, 1)][SerializeField] private float beatLengthPercentage;
        private float beatLength;
        private float beatLenghtTimer;

        [Header("||===== Circle Points =====||")]
        [SerializeField] private int numberOfPoints;
        private List<Vector2> points = new List<Vector2>();

        private Vector2 targetPosition;
        private Vector2 targetVector;

        private float closestPoint;
        private Vector2 bestPoint;
        private Vector2 relativePointPosition;
        private Vector2 relativePointVector;

        private bool seesPlayer;

        public bool showGizmos;

        private void Awake()
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
            colliderRadius = discCollider.radius;

            //Calcula um círculo de pontos
            points.Clear();

            float angleStep = 360f / numberOfPoints;

            float angle;
            Vector2 newPoint;

            for (int i = 0; i < numberOfPoints; i++)
            {
                angle = i * angleStep * Mathf.Deg2Rad;

                newPoint = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * chargeThreshold;
                points.Add(newPoint);
            }
        }

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();

            aggroEffects = AggroEffects.Instance;
        }

        public override void StateEnter()
        {
            if (vynilDiscController.firstTimeAggroed)
                aggroEffects.ApplyEffects(exclamationPoint);

            vynilDiscController.firstTimeAggroed = false;
        }

        public override void StateUpdate()
        {
            targetPosition = vynilDiscController.playerTransform.position;
            targetVector = targetPosition - rb.position;

            //Flipa o sprite
            if (Mathf.Sign(targetVector.x) != Mathf.Sign(spriteTransform.localScale.x))
            {
                Vector3 newScale = spriteTransform.localScale;
                newScale.x *= -1;
                spriteTransform.localScale = newScale;
            }

            seesPlayer = !Physics2D.CircleCast(tr.position, colliderRadius, targetVector.normalized, targetVector.magnitude, terrainLayers);

            if (vynilDiscController.beatHappened)
            {
                // Transição para Wind Up
                if
                (
                    targetVector.magnitude <= chargeThreshold &&
                    seesPlayer &&
                    chargeBeatCounter <= 0
                )
                {
                    chargeBeatCounter = chargeBeatsCooldown;
                    vynilDiscController.SetWindUp();
                    return;
                }

                animator.Play(animationClip.name, 0, 0);

                beatLenghtTimer = beatLength * beatLengthPercentage;
                chargeBeatCounter--;
            }

            beatLenghtTimer -= Time.deltaTime;

            // Encontra o melhor ponto para se mover
            bestPoint = targetPosition;

            closestPoint = Mathf.Infinity;

            foreach (Vector2 currentPoint in points)
            {
                relativePointPosition = targetPosition + currentPoint; // Posição do ponto somada à posição do jogador (move o circulo para ficar ao redor do jogador)

                relativePointVector = targetPosition - relativePointPosition;

                // Se é possível ver o jogador a partir do ponto
                if (!Physics2D.CircleCast(relativePointPosition, colliderRadius, relativePointVector.normalized, relativePointVector.magnitude, terrainLayers))
                {
                    float pointDistance = Vector2.Distance(rb.position, relativePointPosition);

                    if (pointDistance < closestPoint)
                    {
                        closestPoint = pointDistance;
                        bestPoint = relativePointPosition;
                    }
                }
            }

            if (beatLenghtTimer > Mathf.Epsilon && (targetVector.magnitude > chargeThreshold || !seesPlayer))
                navMeshAgent.SetDestination(bestPoint);
            else
                navMeshAgent.ResetPath();
        }

        public override void StateExit()
        {
            navMeshAgent.ResetPath();
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chargeThreshold);
        }
    }
}