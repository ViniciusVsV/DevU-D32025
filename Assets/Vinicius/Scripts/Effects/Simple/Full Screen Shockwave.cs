using System.Collections;
using UnityEngine;

namespace Effects.Simple
{
    public class FullScreenShockwave : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private GameObject shockwaveObject;
        private Renderer shockwaveRenderer;
        private MaterialPropertyBlock propertyBlock;

        private static int shockwaveProgress = Shader.PropertyToID("_WaveDistanceFromCenter");

        private Coroutine coroutine;

        private void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();

            shockwaveRenderer = shockwaveObject.GetComponent<Renderer>();
        }

        public void ApplyEffect(float duration)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(Routine(duration));
        }

        private IEnumerator Routine(float duration)
        {
            shockwaveObject.SetActive(true);

            propertyBlock.SetFloat(shockwaveProgress, -0.1f);
            shockwaveRenderer.SetPropertyBlock(propertyBlock);

            float elapsedTime = 0;
            float progress;
            float newValue;

            while (elapsedTime < duration)
            {
                progress = elapsedTime / duration;

                newValue = Mathf.Lerp(-0.1f, 1, progress);

                propertyBlock.SetFloat(shockwaveProgress, newValue);
                shockwaveRenderer.SetPropertyBlock(propertyBlock);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            shockwaveObject.SetActive(false);

            coroutine = null;
        }
    }
}