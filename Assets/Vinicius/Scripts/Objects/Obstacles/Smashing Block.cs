using UnityEngine;
using System.Collections;

namespace Objects.Obstacles
{
    public class SmashingBlock : MonoBehaviour, IActivatable, IDeactivatable, IRestorable, IRythmSyncable
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private Rigidbody2D blockRb;
        [SerializeField] private Transform smashPoint;
        [SerializeField] private BoxCollider2D smashTrigger;
        [SerializeField] private BoxCollider2D retreatTrigger;

        [Header("||===== Parameters =====||")]
        [SerializeField] private int beatDelay;
        private int beatCounter = -1;

        private float beatLength;

        [SerializeField] private AnimationCurve smashCurve;
        [SerializeField] private AnimationCurve retreatCurve;

        private Vector3 initialPosition;
        private Vector2 startPos;
        private Vector2 targetPos;
        private Vector2 desiredPos;
        private Vector2 desiredVelocity;
        private float progress;
        private float curveFactor;

        private bool isActive;
        public bool showGizmos;

        private void Start()
        {
            initialPosition = transform.position;

            beatLength = BeatController.Instance.GetBeatLength();
        }

        public void Activate() { isActive = true; }
        public void Deactivate() { isActive = false; }
        public void Restore()
        {
            StopAllCoroutines();

            blockRb.position = initialPosition;

            isActive = true;
        }

        private void OnEnable() { BeatInterval.OnOneBeatHappened += RespondToBeat; }
        private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }

        public void RespondToBeat()
        {
            if (beatDelay > 0)
            {
                beatDelay--;
                return;
            }

            beatCounter = (beatCounter + 1) % 4; // Mantendo todo o comportamento do bloco dentro do 4:4

            if (!isActive)
                return;

            if (beatCounter == 0)
            {
                StopAllCoroutines();
                StartCoroutine(SmashRoutine());
            }
            else if (beatCounter == 2)
            {
                StopAllCoroutines();
                StartCoroutine(RetreatRoutine());
            }
        }

        private IEnumerator SmashRoutine()
        {
            startPos = initialPosition;
            targetPos = smashPoint.position;

            float elapsedTime = 0f;

            while (elapsedTime < beatLength)
            {
                progress = elapsedTime / beatLength;

                if (progress >= 0.8)
                    smashTrigger.enabled = true;

                curveFactor = smashCurve.Evaluate(progress);

                desiredPos = Vector2.LerpUnclamped(startPos, targetPos, curveFactor);

                desiredVelocity = (desiredPos - blockRb.position) / Time.fixedDeltaTime;

                blockRb.linearVelocity = desiredVelocity;

                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            blockRb.position = targetPos;
            blockRb.linearVelocity = Vector2.zero;

            smashTrigger.enabled = false;
        }

        private IEnumerator RetreatRoutine()
        {
            startPos = smashPoint.position;
            targetPos = initialPosition;

            float elapsedTime = 0f;

            while (elapsedTime < beatLength)
            {
                progress = elapsedTime / beatLength;

                if (progress >= 0.7)
                    retreatTrigger.enabled = true;

                curveFactor = retreatCurve.Evaluate(progress);

                desiredPos = Vector2.LerpUnclamped(startPos, targetPos, curveFactor);

                desiredVelocity = (desiredPos - blockRb.position) / Time.fixedDeltaTime;

                blockRb.linearVelocity = desiredVelocity;

                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            blockRb.position = targetPos;
            blockRb.linearVelocity = Vector2.zero;

            retreatTrigger.enabled = false;
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