using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects.Simple
{
    public class SpriteFlash : MonoBehaviour
    {
        [SerializeField] private Material flashMaterial;

        public void ApplyEffect(SpriteRenderer sprite, float duration)
        {
            StartCoroutine(Routine(sprite, duration));
        }

        private IEnumerator Routine(SpriteRenderer sprite, float duration)
        {
            Material baseMaterial = sprite.material;

            sprite.material = flashMaterial;

            yield return new WaitForSeconds(duration);

            sprite.material = baseMaterial;
        }
    }
}