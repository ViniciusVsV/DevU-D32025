using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Objects.Platforms
{
    public class MovingPlatform : MonoBehaviour, IRythmSyncable
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private Transform point1;
        [SerializeField] private Transform point2;
        [SerializeField] private Rigidbody2D platformRb;

        [Header("||===== Parameters =====||")]
        [SerializeField] private AnimationCurve moveCurve;
        [SerializeField] private int numberOfPoints;

        private List<Vector2> positions = new();

        private int currentPos;
        private int nextPos;
        private bool isForward;

        private float beatLength;

        private void Awake()
        {
            CalculatePositions();

            isForward = true;
        }

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
        }

        private void CalculatePositions()
        {
            float totalDistance = Vector2.Distance(point1.position, point2.position);
            float pointDistace = totalDistance / (numberOfPoints + 1);

            Vector2 direction = (point2.position - point1.position).normalized;

            positions.Add(point1.position);

            for (int i = 1; i <= numberOfPoints; i++)
                positions.Add((Vector2)point1.position + i * pointDistace * direction);

            positions.Add(point2.position);
        }

        public void RespondToBeat()
        {
            nextPos += isForward ? 1 : -1;

            if (nextPos > numberOfPoints + 1 || nextPos < 0)
            {
                isForward = !isForward;

                currentPos += isForward ? 1 : -1;
                nextPos = currentPos;
            }
            else
                currentPos = nextPos;

            StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            Vector2 startPos = platformRb.position;
            Vector2 targetPos = positions[nextPos];

            float elapsedTime = 0f;

            while (elapsedTime < beatLength)
            {
                float progress = elapsedTime / beatLength;

                float curveFactor = moveCurve.Evaluate(progress);

                Vector2 desiredPos = Vector2.LerpUnclamped(startPos, targetPos, curveFactor);

                Vector2 desiredVelocity = (desiredPos - platformRb.position) / Time.fixedDeltaTime;

                platformRb.linearVelocity = desiredVelocity;

                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            platformRb.position = targetPos;
            platformRb.linearVelocity = Vector2.zero;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            positions.Clear();

            CalculatePositions();
        }
#endif

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(point1.position, point2.position);

            Gizmos.color = Color.green;
            foreach (Vector2 position in positions)
                Gizmos.DrawCube(position, new Vector3(0.2f, 0.2f, 1f));
        }
    }
}