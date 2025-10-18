using DG.Tweening;
using UnityEngine;

namespace Effects.Simple
{
    public class CameraTransition : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private RectTransform transitionImage;

        [Header("||===== Parameters =====||")]
        [SerializeField] private Ease applyEase;
        [SerializeField] private Ease removeEase;

        private float screenWidth;

        private Vector2 offScreenLeft;
        private Vector2 center;
        private Vector2 offScreenRight;

        public bool isPlaying;

        private void Awake()
        {
            screenWidth = transitionImage.rect.width;

            center = Vector2.zero;
            offScreenLeft = new Vector2(-screenWidth, 0);
            offScreenRight = new Vector2(screenWidth, 0);

            transitionImage.anchoredPosition = offScreenLeft;
        }

        public void ApplyEffect(float duration)
        {
            isPlaying = true;

            transitionImage.DOAnchorPos(center, duration).SetEase(applyEase)
                .OnComplete(() => { isPlaying = false; });

        }

        public void RemoveEffect(float duration)
        {
            isPlaying = true;

            transitionImage.DOAnchorPos(offScreenRight, duration).SetEase(removeEase)
                .OnComplete(() =>
                    {
                        transitionImage.anchoredPosition = offScreenLeft;
                        isPlaying = false;
                    });
        }
    }
}