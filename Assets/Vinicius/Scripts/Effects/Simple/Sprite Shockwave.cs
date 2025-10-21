using System.Collections;
using UnityEngine;

namespace Effects.Simple
{
    public class SpriteShockwave : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        private MaterialPropertyBlock propertyBlock;

        private static int shockwaveProgress = Shader.PropertyToID("_WaveDistanceFromCenter");

        private void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();
        }

        public void ApplyEffect(Renderer renderer, float duration)
        {
            StartCoroutine(Routine(renderer, duration));
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
        }
    }
}