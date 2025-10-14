using UnityEngine;
using System.Collections;

namespace Objects.Obstacles
{
    public class SmashingBlock : MonoBehaviour, IRythmSyncable
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private Rigidbody2D blockRb;
        [SerializeField] private Transform smashPoint;
        [SerializeField] private BoxCollider2D smashZone;

        [Header("||===== Parameters =====||")]
        [SerializeField] private int beatsCooldown;
        [SerializeField] private int beatsStill; // Tempo em batidas que fica parado após bater
        private int beatCounter;
        private int stillCounter;

        private float beatLength;
        private float smashLength;
        private float retreatLength;

        [SerializeField] private AnimationCurve smashCurve;
        [SerializeField] private AnimationCurve retreatCurve;

        private Vector3 initialPosition;
        private Vector2 startPos;
        private Vector2 targetPos;
        private Vector2 desiredPos;
        private Vector2 desiredVelocity;
        private float progress;
        private float curveFactor;

        public bool showGizmos;
        private bool isStill;

        private void Start()
        {
            initialPosition = transform.position;

            beatLength = BeatController.Instance.GetBeatLength();

            smashLength = beatLength;
            retreatLength = beatLength * beatsCooldown;
        }

        public void RespondToBeat()
        {
            if (isStill)
            {
                stillCounter--;

                isStill = stillCounter > 0;

                return;
            }

            beatCounter = (beatCounter + 1) % (beatsCooldown + 1);

            if (beatCounter == 0)
            {
                StopAllCoroutines();
                StartCoroutine(SmashRoutine());
            }
        }

        private IEnumerator SmashRoutine()
        {
            startPos = initialPosition;
            targetPos = smashPoint.position;

            // Atribui os valores antes para não ocorrer possível dessincronização
            stillCounter = beatsStill + 1;
            isStill = true;

            smashZone.enabled = true;

            float elapsedTime = 0f;

            while (elapsedTime < smashLength)
            {
                progress = elapsedTime / smashLength;

                curveFactor = smashCurve.Evaluate(progress);

                desiredPos = Vector2.LerpUnclamped(startPos, targetPos, curveFactor);

                desiredVelocity = (desiredPos - blockRb.position) / Time.fixedDeltaTime;

                blockRb.linearVelocity = desiredVelocity;

                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            blockRb.position = targetPos;
            blockRb.linearVelocity = Vector2.zero;

            smashZone.enabled = false;

            StartCoroutine(RetreatRoutine());
        }

        private IEnumerator RetreatRoutine()
        {
            startPos = smashPoint.position;
            targetPos = initialPosition;

            while (isStill)
                yield return null;

            float elapsedTime = 0f;

            while (elapsedTime < retreatLength)
            {
                progress = elapsedTime / retreatLength;

                curveFactor = retreatCurve.Evaluate(progress);

                desiredPos = Vector2.LerpUnclamped(startPos, targetPos, curveFactor);

                desiredVelocity = (desiredPos - blockRb.position) / Time.fixedDeltaTime;

                blockRb.linearVelocity = desiredVelocity;

                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            blockRb.position = targetPos;
            blockRb.linearVelocity = Vector2.zero;
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(blockRb.position, smashPoint.position);
            Gizmos.DrawWireCube(initialPosition, Vector3.one);
        }
    }
}