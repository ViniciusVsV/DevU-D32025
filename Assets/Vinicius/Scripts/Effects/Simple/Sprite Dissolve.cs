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
            StartCoroutine(Routine(renderer, duration));
        }

        private IEnumerator Routine(Renderer renderer, float duration)
        {
            propertyBlock.SetFloat(dissolveAmount, 0);
            renderer.SetPropertyBlock(propertyBlock);

            float elapsedTime = 0;
            float progress;
            float newValue;

            while (elapsedTime < duration)
            {
                progress = elapsedTime / duration;

                newValue = Mathf.Lerp(0, 1, progress);

                propertyBlock.SetFloat(dissolveAmount, newValue);
                renderer.SetPropertyBlock(propertyBlock);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            propertyBlock.SetFloat(dissolveAmount, 1);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}