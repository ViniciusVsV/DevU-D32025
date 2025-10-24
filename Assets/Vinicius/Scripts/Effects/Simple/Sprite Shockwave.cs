using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects.Simple
{
    public class SpriteShockwave : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        private MaterialPropertyBlock propertyBlock;

        private Dictionary<Renderer, Coroutine> activeCoroutines = new();

        private static int shockwaveProgress = Shader.PropertyToID("_WaveDistanceFromCenter");

        private void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();
        }

        public void ApplyEffect(Renderer renderer, float duration)
        {
            if (activeCoroutines.ContainsKey(renderer))
                return;

            Coroutine newRoutine = StartCoroutine(Routine(renderer, duration));

            activeCoroutines.Add(renderer, newRoutine);
        }

        public void RemoveEffect(Renderer renderer)
        {
            if (activeCoroutines.TryGetValue(renderer, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);

                propertyBlock.SetFloat(shockwaveProgress, -0.1f);
                renderer.SetPropertyBlock(propertyBlock);

                activeCoroutines.Remove(renderer);
            }
        }

        private IEnumerator Routine(Renderer renderer, float duration)
        {
            propertyBlock.SetFloat(shockwaveProgress, -0.1f);
            renderer.SetPropertyBlock(propertyBlock);

            float elapsedTime = 0;
            float progress;
            float newValue;

            while (elapsedTime < duration)
            {
                progress = elapsedTime / duration;

                newValue = Mathf.Lerp(-0.1f, 1, progress);

                propertyBlock.SetFloat(shockwaveProgress, newValue);
                renderer.SetPropertyBlock(propertyBlock);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            activeCoroutines.Remove(renderer);
        }

        private void OnDestroy() { StopAllCoroutines(); }
    }
}