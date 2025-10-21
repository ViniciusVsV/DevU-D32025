using DG.Tweening;
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

        [Header("||=====Rythm Parameters =====||")]
        [SerializeField] private Vector3 pulseSize;
        [SerializeField] private float pulseDuration;
        [SerializeField] private Ease pulseEase;

        private SpriteShockwave spriteShockwave;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col = GetComponent<Collider2D>();
        }

        private void Start()
        {
            spriteShockwave = FindFirstObjectByType<SpriteShockwave>();
        }

        public void RespondToBeat()
        {
            spriteShockwave.ApplyEffect(spriteRenderer, 0.4f);
        }

        public void Activate()
        {
            BeatInterval.OnOneBeatHappened += RespondToBeat;

            spriteRenderer.enabled = true;
            col.enabled = true;
        }

        public void Deactivate()
        {
            BeatInterval.OnOneBeatHappened -= RespondToBeat;

            spriteRenderer.enabled = false;
            col.enabled = false;
        }
        private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }
    }
}