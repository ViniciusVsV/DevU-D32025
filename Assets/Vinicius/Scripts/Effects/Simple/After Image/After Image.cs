using System.Collections;
using UnityEngine;

namespace Effects.Simple.AfterImage
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AfterImage : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        private AfterImagesManager afterImagesManager;
        private SpriteRenderer spriteRenderer;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float startingAlpha;
        [SerializeField] private float imageDuration;
        private Color startingColor;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            startingColor = Color.white;
            startingColor.a = startingAlpha;
        }

        public void Initialize(AfterImagesManager afterImagesManager)
        {
            this.afterImagesManager = afterImagesManager;
        }

        public void ApplyEffect(Vector2 position, Sprite sprite)
        {
            transform.position = position;

            //Inicializa o sprite renderer
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = startingColor;

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            float elapsedTime = 0;
            float progress;

            Color color = startingColor;

            while (elapsedTime < imageDuration)
            {
                progress = elapsedTime / imageDuration;

                color.a = Mathf.Lerp(startingAlpha, 0, progress);
                spriteRenderer.color = color;

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            spriteRenderer.color = Color.clear;
            spriteRenderer.sprite = null;

            afterImagesManager.ReturnImage(this);
        }
    }
}