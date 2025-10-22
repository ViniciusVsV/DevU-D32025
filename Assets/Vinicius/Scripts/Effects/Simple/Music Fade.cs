using System.Collections;
using UnityEngine;

namespace Effects.Simple
{
    public class MusicFade : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;

        public bool isPlaying;

        public void FadeIn(float fadeDuration)
        {
            isPlaying = false;

            StartCoroutine(Routine(fadeDuration, 0, 1));
        }

        public void FadeOut(float fadeDuration)
        {
            isPlaying = true;

            StartCoroutine(Routine(fadeDuration, 1, 0));
        }

        private IEnumerator Routine(float duration, float initialValue, float targetValue)
        {
            float elapsedTime = 0;
            float progress;

            while (elapsedTime < duration)
            {
                progress = elapsedTime / duration;

                musicSource.volume = Mathf.Lerp(initialValue, targetValue, progress);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            musicSource.volume = targetValue;

            isPlaying = false;
        }
    }
}