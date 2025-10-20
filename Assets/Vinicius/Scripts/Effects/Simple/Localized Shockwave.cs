using System.Collections;
using UnityEngine;

namespace Effects.Simple
{
    public class LocalizedShockwave : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private Material shaderMaterial;
        [SerializeField] private GameObject shockwaveObjectPrefab;
        private Renderer shockwaveRenderer;
        private MaterialPropertyBlock propertyBlock;

        private static int shockwaveProgress = Shader.PropertyToID("_WaveDistanceFromCenter");

        private Coroutine coroutine;

        private void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();
        }

        public void ApplyEffect(float duration, Vector2 position, Vector3 size)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(Routine(duration, position, size));
        }

        private IEnumerator Routine(float duration, Vector2 position, Vector3 size)
        {
            GameObject newObject = Instantiate(shockwaveObjectPrefab);
            shockwaveRenderer = newObject.GetComponent<Renderer>();

            newObject.transform.localScale = size;
            newObject.transform.position = position;

            newObject.SetActive(true);

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

            Destroy(newObject);

            coroutine = null;
        }
    }
}