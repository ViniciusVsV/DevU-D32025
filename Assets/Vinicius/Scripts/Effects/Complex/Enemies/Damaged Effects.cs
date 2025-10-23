using DG.Tweening;
using Effects.Simple;
using UnityEngine;

namespace Effects.Complex.Enemies
{
    public class DamagedEffects : MonoBehaviour
    {
        public static DamagedEffects Instance;

        [Header("Objects")]
        private SpriteFlash spriteFlash;

        [Header("Parameters")]
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeStrength;
        [SerializeField] private float spriteFlashDuration;


        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            spriteFlash = FindFirstObjectByType<SpriteFlash>();
        }

        public void ApplyEffects(Transform enemy, SpriteRenderer sprite)
        {
            finishedPlaying = false;

            spriteFlash.ApplyEffect(sprite, spriteFlashDuration);

            enemy.DOShakePosition(shakeDuration, new Vector3(shakeStrength, 0, 0), 20, 90, false, true)
                .OnComplete(() => { finishedPlaying = true; });
        }
    }
}