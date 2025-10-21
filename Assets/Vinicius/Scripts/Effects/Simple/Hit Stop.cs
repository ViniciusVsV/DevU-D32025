using System.Collections;
using UnityEngine;

namespace Effects.Simple
{
    public class HitStop : MonoBehaviour
    {
        private float currentTimeScale;

        private Coroutine coroutine;

        public bool isPlaying;

        public void ApplyEffect(float duration)
        {
            isPlaying = true;

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(Routine(duration));
        }

        private IEnumerator Routine(float duration)
        {
            currentTimeScale = Time.timeScale;

            Time.timeScale = 0f;

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = currentTimeScale;

            isPlaying = false;

            coroutine = null;
        }
    }
}