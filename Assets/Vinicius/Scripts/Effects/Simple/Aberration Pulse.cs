using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Effects.Simple
{
    public class AberrationPulse : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private Volume volume;
        private ChromaticAberration chromaticAberration;

        private void Start()
        {
            volume.profile.TryGet(out chromaticAberration);
        }

        public void ApplyEffect(float intensity, float duration)
        {
            StartCoroutine(Routine(intensity, duration));
        }

        private IEnumerator Routine(float intensity, float duration)
        {
            float elapsedTime = 0;
            float progress;

            while (elapsedTime < duration)
            {
                progress = elapsedTime / duration;

                chromaticAberration.intensity.value = Mathf.Lerp(intensity, 0, progress);

                elapsedTime += Time.unscaledDeltaTime;

                yield return null;
            }

            chromaticAberration.intensity.value = 0;
        }
    }
}