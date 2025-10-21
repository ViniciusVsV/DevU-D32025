using DG.Tweening;
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

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col = GetComponent<Collider2D>();
        }

        public void RespondToBeat()
        {
            transform.localScale = pulseSize;

            transform.DOScale(Vector3.one, pulseDuration).SetEase(pulseEase);
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