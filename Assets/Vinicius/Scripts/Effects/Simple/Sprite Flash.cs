using System.Collections;
using UnityEngine;

namespace Effects.Simple
{
    public class SpriteFlash : MonoBehaviour
    {
        private Coroutine coroutine;

        [SerializeField] private Material flashMaterial;
        [SerializeField] private Material baseMaterial;

        public void ApplyEffect(SpriteRenderer sprite, float duration)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                sprite.material = baseMaterial;
            }

            coroutine = StartCoroutine(Routine(sprite, duration));
        }

        private IEnumerator Routine(SpriteRenderer sprite, float duration)
        {
            sprite.material = flashMaterial;

            yield return new WaitForSeconds(duration);

            sprite.material = baseMaterial;

            coroutine = null;
        }
    }
}