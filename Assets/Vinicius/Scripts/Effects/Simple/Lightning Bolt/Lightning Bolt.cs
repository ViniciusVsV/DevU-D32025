using System.Collections;
using UnityEngine;

namespace Effects.Simple.LightningBolt
{
    [RequireComponent(typeof(LineRenderer))]
    public class LightningBolt : MonoBehaviour
    {
        private LightningBoltManager pool;
        private LineRenderer lineRenderer;

        [Header("||===== Parameters =====||")]
        [SerializeField] private AnimationCurve segmentsDistanceCurve; // Quantidade de segmentos proporcional à distãncia (+ distância -> mais segmentos)
        [SerializeField] private int expectedSegments;
        [SerializeField] private float maxDistance;

        [SerializeField] private float noiseStrength;

        [SerializeField] private Vector2Int ocurrencesRange; // Quantidade de ocorrências dentro do alcance (x = min, y = max)
        [SerializeField] private float baseDuration;

        private int usedSegments;
        private float distance;
        private float normalizedDistance;

        private Vector3 position;
        private Vector3 noise;
        private float progress;

        private int ocurrences;
        private int ocurrenceCounter;
        private float usedDuration;

        public bool isPlaying;

        public void Initialize(LightningBoltManager pool)
        {
            this.pool = pool;

            lineRenderer = GetComponent<LineRenderer>();
        }

        public void ApplyEffect(Vector2 startPoint, Vector2 endPoint)
        {
            distance = Vector2.Distance(startPoint, endPoint);

            normalizedDistance = Mathf.Clamp01(distance / maxDistance);

            usedSegments = Mathf.Max(2, Mathf.RoundToInt(segmentsDistanceCurve.Evaluate(normalizedDistance) * expectedSegments));

            ocurrences = Random.Range(ocurrencesRange.x, ocurrencesRange.y + 1);

            usedDuration = Random.Range(baseDuration * 0.7f, baseDuration * 1.1f);

            StartCoroutine(Routine(startPoint, endPoint));
        }

        private IEnumerator Routine(Vector2 startPoint, Vector2 endPoint)
        {
            ocurrenceCounter = 0;

            while (ocurrenceCounter < ocurrences)
            {
                lineRenderer.positionCount = usedSegments;

                lineRenderer.SetPosition(0, startPoint);

                // Gera os pontos com ruído ao longo do caminho para criar o efeito de raio
                for (int i = 1; i < usedSegments - 1; i++)
                {
                    progress = (float)i / (usedSegments - 2);
                    position = Vector3.Lerp(startPoint, endPoint, progress);

                    noise = Random.insideUnitCircle * noiseStrength;

                    position.x += noise.x;
                    position.y += noise.y;

                    lineRenderer.SetPosition(i, position);
                }

                lineRenderer.SetPosition(usedSegments - 1, endPoint);

                ocurrenceCounter++;

                yield return new WaitForSeconds(usedDuration);

                lineRenderer.positionCount = 0;
            }

            pool.ReturnBolt(this);
        }
    }
}