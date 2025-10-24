using Effects.Complex.Enemies.VynilDisc;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.VynilDisc.States
{
    public class Charge : BaseState
    {
        private StateController vynilDiscController => (StateController)controller;

        [SerializeField] private AnimationClip animationClip;

        [Header("||===== Objects =====||")]
        [SerializeField] private CircleCollider2D killTrigger;
        [SerializeField] private LayerMask terrainLayers;
        [SerializeField] private Transform spriteTransform;
        private ChargeEffects chargeEffects;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float chargeDistance;
        [Range(0, 1)][SerializeField] private float chargeBeatPercentage;
        [Range(0, 1)][SerializeField] private float brakeBeatPercentage;
        private float beatLength;
        private float beatTimer;

        [SerializeField] private float dazeDetectionRadius;
        [SerializeField] private float dazeDetectionDistance;

        private Vector2 chargeSpeed;

        private Vector2 targetPosition;
        private Vector2 targetVector;
        private Vector2 targetDirection;

        public bool showGismoz;

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
            chargeEffects = ChargeEffects.Instance;
        }

        public override void StateEnter()
        {
            animator.Play(animationClip.name, 0, 0);

            beatTimer = 0;

            targetVector = vynilDiscController.playerTransform.position - tr.position;
            targetDirection = targetVector.normalized;

            //Flipa o sprite
            if (Mathf.Sign(targetVector.x) != Mathf.Sign(spriteTransform.localScale.x))
            {
                Vector3 newScale = spriteTransform.localScale;
                newScale.x *= -1;
                spriteTransform.localScale = newScale;
            }

            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            if (spriteTransform.localScale.x < 0)
                angle += 180f;

            spriteTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            targetPosition = rb.position + targetDirection * chargeDistance;

            vynilDiscController.followPoint = targetPosition;

            chargeSpeed = chargeDistance / (beatLength * chargeBeatPercentage) * targetDirection;

            killTrigger.enabled = true;

            chargeEffects.ApplyEffects(spriteTransform, spriteRenderer);

            rb.linearVelocity = chargeSpeed;
        }

        public override void StateUpdate()
        {
            beatTimer += Time.deltaTime;

            // Transição para Daze
            if (Physics2D.CircleCast(tr.position, dazeDetectionRadius, targetDirection, dazeDetectionDistance, terrainLayers))
                vynilDiscController.SetDaze();

            // Transição para Chase
            else if (beatTimer >= beatLength * (chargeBeatPercentage + brakeBeatPercentage))
                vynilDiscController.SetChase();
        }

        public override void StateFixedUpdate()
        {
            //Passou do tempo de charge
            if (beatTimer >= beatLength * chargeBeatPercentage)
            {
                killTrigger.enabled = false;

                float brakeElapsed = beatTimer - (beatLength * chargeBeatPercentage);

                float brakeDuration = beatLength * brakeBeatPercentage;

                float t = Mathf.Clamp01(brakeElapsed / brakeDuration);

                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, t);
            }
        }

        public override void StateExit()
        {
            spriteTransform.rotation = Quaternion.Euler(Vector3.zero);

            chargeEffects.RemoveEffects(spriteTransform);

            rb.linearVelocity = Vector2.zero;

            killTrigger.enabled = false;
        }

        private void OnDrawGizmos()
        {
            if (!showGismoz)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + targetVector);

            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, chargeDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, dazeDetectionRadius);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + targetDirection * dazeDetectionDistance);
            Gizmos.DrawWireSphere((Vector2)transform.position + (targetDirection * dazeDetectionDistance), dazeDetectionRadius);
        }
    }
}