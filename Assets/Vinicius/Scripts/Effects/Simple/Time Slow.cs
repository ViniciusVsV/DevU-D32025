using System.Collections;
using UnityEngine;

namespace Effects.Simple
{
    public class TimeSlow : MonoBehaviour
    {
        [SerializeField] private AnimationCurve slowCurve;

        public void ApplyEffect(float duration)
        {
            StartCoroutine(Routine(duration));
        }

        private IEnumerator Routine(float duration)
        {
            float elapsedTime = 0;
            float progress;

            while (elapsedTime < duration)
            {
                progress = elapsedTime / duration;

                Time.timeScale = 1 - slowCurve.Evaluate(progress);

                elapsedTime += Time.unscaledDeltaTime;

                yield return null;
            }

            Time.timeScale = 0;
        }
    }
}