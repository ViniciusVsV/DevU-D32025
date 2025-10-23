using DG.Tweening;
using Effects.Complex.Objects;
using Effects.Simple;
using UnityEngine;

namespace Objects.Obstacles.DestructibleSpike
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class DestructibleSpike : MonoBehaviour, IRythmSyncable
    {
        private SpriteRenderer spriteRenderer;
        private Collider2D col;

        [SerializeField] private Material shockwaveMaterial;

        [Header("||=====Rythm Parameters =====||")]
        [SerializeField] private Vector3 pulseSize;
        [SerializeField] private float pulseDuration;
        [SerializeField] private Ease pulseEase;

        private SpikeDestroyedEffects spikeDestroyedEffects;
        private SpriteShockwave spriteShockwave;

        private bool isActive;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col = GetComponent<Collider2D>();
        }

        private void Start()
        {
            spriteShockwave = FindFirstObjectByType<SpriteShockwave>();
            spikeDestroyedEffects = SpikeDestroyedEffects.Instance;
        }

        public void RespondToBeat()
        {
            if (isActive)
                spriteShockwave.ApplyEffect(spriteRenderer, 0.4f);
        }

        public void Activate()
        {
            BeatInterval.OnOneBeatHappened += RespondToBeat;

            spriteRenderer.material = shockwaveMaterial;
            col.enabled = true;

            isActive = true;
        }

        public void Deactivate()
        {
            BeatInterval.OnOneBeatHappened -= RespondToBeat;

            spikeDestroyedEffects.ApplyEffects(transform.position, spriteRenderer);
            col.enabled = false;

            isActive = false;
        }
        private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }
        private void OnDestroy() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }
    }
}