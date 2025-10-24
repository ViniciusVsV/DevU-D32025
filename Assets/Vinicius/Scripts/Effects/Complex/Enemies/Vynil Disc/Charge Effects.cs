using Effects.Simple.AfterImage;
using UnityEngine;

namespace Effects.Complex.Enemies.VynilDisc
{
    public class ChargeEffects : MonoBehaviour
    {
        public static ChargeEffects Instance;

        [Header("Objects")]
        private AfterImagesManager afterImagesManager;

        [Header("Parameters")]

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            afterImagesManager = FindFirstObjectByType<AfterImagesManager>();
        }

        public void ApplyEffects(Transform spriteTransform, SpriteRenderer spriteRenderer)
        {
            AudioController.Instance.PlayDiscDashSFX();

            afterImagesManager.StartAfterImages(spriteTransform, spriteRenderer);
        }

        public void RemoveEffects(Transform spriteTransform)
        {
            afterImagesManager.StopAfterImages(spriteTransform);
        }
    }
}