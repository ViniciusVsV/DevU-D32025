using DG.Tweening;
using Effects.Complex.Objects;
using Effects.Simple;
using UnityEngine;

namespace Objects.Obstacles.DestructibleSpike
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class DestructibleSpike : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Collider2D col;

        [SerializeField] private Material shockwaveMaterial;

        [Header("||=====Rythm Parameters =====||")]
        [SerializeField] private Vector3 pulseSize;
        [SerializeField] private float pulseDuration;
        [SerializeField] private Ease pulseEase;

        private SpikeDestroyedEffects spikeDestroyedEffects;

        private bool isActive;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col = GetComponent<Collider2D>();
        }

        private void Start()
        {
            spikeDestroyedEffects = SpikeDestroyedEffects.Instance;
        }

        public void Activate()
        {
            col.enabled = true;
            spikeDestroyedEffects.RemoveEffects(spriteRenderer);

            isActive = true;
        }

        public void Deactivate()
        {
            spikeDestroyedEffects.ApplyEffects(spriteRenderer);
            col.enabled = false;

            isActive = false;
        }
    }
}