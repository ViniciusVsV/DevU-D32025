using System.Collections;
using UnityEngine;

namespace Effects.Simple
{
    public class SpriteDissolve : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        private MaterialPropertyBlock propertyBlock;

        private static int dissolveAmount = Shader.PropertyToID("_DissolveAmount");

        private void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();
        }

        public void ApplyEffect(Renderer renderer, float duration)
        {
            StartCoroutine(Routine(renderer, duration, 0, 1));
        }

        public void RemoveEffect(Renderer renderer, float duration)
        {
            StartCoroutine(Routine(renderer, duration, 1, 0));
        }

        private IEnumerator Routine(Renderer renderer, float duration, float initialAmount, float targetAmount)
        {
            propertyBlock.SetFloat(dissolveAmount, initialAmount);
            renderer.SetPropertyBlock(propertyBlock);

            float elapsedTime = 0;
            float progress;
            float newValue;

            while (elapsedTime < duration)
            {
                progress = elapsedTime / duration;

                newValue = Mathf.Lerp(initialAmount, targetAmount, progress);

                propertyBlock.SetFloat(dissolveAmount, newValue);
                renderer.SetPropertyBlock(propertyBlock);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            propertyBlock.SetFloat(dissolveAmount, targetAmount);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}