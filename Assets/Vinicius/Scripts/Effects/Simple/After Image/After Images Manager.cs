using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects.Simple.AfterImage
{
    public class AfterImagesManager : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private AfterImage afterImagePrefab;

        private Queue<AfterImage> pool = new();
        private Dictionary<Transform, Coroutine> activeCoroutines = new();

        [Header("||===== Parameters =====||")]
        [SerializeField] private int poolSize = 10;
        [SerializeField] private float delayBetweenImages;

        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                AfterImage newImage = Instantiate(afterImagePrefab, transform);

                newImage.Initialize(this);

                pool.Enqueue(newImage);
            }
        }

        public void StartAfterImages(Transform entity, SpriteRenderer spriteRenderer)
        {
            if (activeCoroutines.ContainsKey(entity))
                return;

            Coroutine newRoutine = StartCoroutine(SpawnRoutine(entity, spriteRenderer));

            activeCoroutines.Add(entity, newRoutine);
        }

        public void StopAfterImages(Transform entity)
        {
            if (activeCoroutines.TryGetValue(entity, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);

                activeCoroutines.Remove(entity);
            }
        }

        private IEnumerator SpawnRoutine(Transform entity, SpriteRenderer spriteRenderer)
        {
            while (true)
            {
                AfterImage afterImage = pool.Count > 0 ? pool.Dequeue() : Instantiate(afterImagePrefab, transform);

                afterImage.Initialize(this);

                // Flipa a imagem
                Vector3 newScale = afterImage.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * Mathf.Sign(entity.localScale.x);
                afterImage.transform.localScale = newScale;

                //Aplica a rotação
                afterImage.transform.rotation = entity.rotation;

                afterImage.ApplyEffect(entity.transform.position, spriteRenderer.sprite);

                yield return new WaitForSeconds(delayBetweenImages);
            }
        }

        public void ReturnImage(AfterImage image)
        {
            pool.Enqueue(image);
        }
    }
}