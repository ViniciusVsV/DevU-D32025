using DG.Tweening;
using UnityEngine;

namespace Effects.Simple
{
    public class NoteMovement : MonoBehaviour
    {
        public static NoteMovement Instance;

        private Vector2 activatedPosition;
        private Vector2 deactivatedPosition;
        private Vector2 hitPosition;

        [Header("||===== Parameters =====||")]
        [Header("Note Activated")]
        [SerializeField] private float activatedDuration;
        [SerializeField] private Ease activatedEase;

        [Header("Note Deactivated")]
        [SerializeField] private float riseDistance;
        [SerializeField] private float deactivatedDuration;
        [SerializeField] private Ease deactivatedEase;

        [Header("Note Hit")]
        [SerializeField] private float fallDistance;
        [Range(0, 1)][SerializeField] private float sizeMultiplier;
        [SerializeField] private float hitDuration;
        [SerializeField] private Ease hitEase;

        [Header("Note Unhit")]
        [SerializeField] private float unhitDuration;
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeStrength;
        [SerializeField] private Ease unhitEase;

        private void Awake()
        {
            Instance = this;
        }

        public void ApplyActivatedEffect(Vector2 initialPosition, Transform noteTransform)
        {
            activatedPosition = initialPosition;
            deactivatedPosition = activatedPosition + riseDistance * Vector2.up;

            noteTransform.DOKill();

            // Garante que está na posição e tamanho corretos
            noteTransform.localPosition = deactivatedPosition;
            noteTransform.localScale = Vector3.one;

            noteTransform.DOLocalMove(activatedPosition, activatedDuration).SetEase(activatedEase);
        }

        public void ApplyDeactivatedEffect(Vector2 initialPosition, Transform noteTransform)
        {
            deactivatedPosition = initialPosition + riseDistance * Vector2.up;

            noteTransform.DOKill();

            noteTransform.DOLocalMove(deactivatedPosition, deactivatedDuration).SetEase(deactivatedEase)
                .OnComplete(() => { noteTransform.localScale = Vector3.zero; });
        }

        public void ApplyHitEffect(Vector2 initialPosition, Transform noteTransform, SpriteRenderer spriteRenderer)
        {
            hitPosition = initialPosition - fallDistance * Vector2.up;

            noteTransform.DOKill();

            var sequence = DOTween.Sequence();

            sequence.Join(noteTransform.DOLocalMove(hitPosition, hitDuration)).SetEase(hitEase);
            sequence.Join(noteTransform.DOScale(Vector3.one * sizeMultiplier, hitDuration)).SetEase(Ease.Linear);

            sequence.OnComplete(() => { spriteRenderer.color = Color.gray; });
        }

        public void ApplyUnhitEffect(Vector2 initialPosition, Transform noteTransform, SpriteRenderer spriteRenderer)
        {
            activatedPosition = initialPosition;

            noteTransform.DOKill();

            var sequence = DOTween.Sequence();

            sequence.Append(noteTransform.DOShakePosition(shakeDuration, new Vector3(shakeStrength, shakeStrength * 0.5f, 0), 20, 90, false, true));

            sequence.Append(noteTransform.DOLocalMove(activatedPosition, unhitDuration)).SetEase(hitEase);
            sequence.Join(noteTransform.DOScale(Vector3.one, unhitDuration)).SetEase(Ease.Linear);

            sequence.OnComplete(() => { spriteRenderer.color = Color.white; });
        }
    }
}