using DG.Tweening;
using UnityEngine;

namespace Effects.Simple
{
    public class CameraTransition : MonoBehaviour, IRythmSyncable
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private RectTransform transitionImage;
        [SerializeField] private RectTransform loadingLogo;

        [Header("||===== Parameters =====||")]
        [SerializeField] private Ease applyEase;
        [SerializeField] private Ease removeEase;
        [SerializeField] private float logoSizePulse;

        private float screenWidth;
        private float beatLength;

        private Vector2 offScreenLeft;
        private Vector2 center;
        private Vector2 offScreenRight;

        public bool isPlaying;
        private bool logoActive;

        private void Awake()
        {
            screenWidth = transitionImage.rect.width;

            center = Vector2.zero;
            offScreenLeft = new Vector2(-screenWidth, 0);
            offScreenRight = new Vector2(screenWidth, 0);

            transitionImage.anchoredPosition = offScreenLeft;
        }

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
        }

        private void OnEnable() { BeatInterval.OnOneBeatHappened += RespondToBeat; }
        private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }

        public void ApplyEffect(float duration, bool showLoadingLogo)
        {
            isPlaying = true;

            if (showLoadingLogo)
            {
                loadingLogo.localScale = Vector3.one;
                logoActive = true;
            }
            else
            {
                loadingLogo.DOKill();
                loadingLogo.localScale = Vector3.zero;
                logoActive = false;
            }

            transitionImage.DOAnchorPos(center, duration).SetEase(applyEase)
            .SetUpdate(true)
            .OnComplete(() => { isPlaying = false; });

        }

        public void RemoveEffect(float duration)
        {
            isPlaying = true;

            transitionImage.DOAnchorPos(offScreenRight, duration).SetEase(removeEase)
            .SetUpdate(true)
            .OnComplete(() =>
                {
                    transitionImage.anchoredPosition = offScreenLeft;
                    isPlaying = false;
                });
        }

        public void RespondToBeat()
        {
            loadingLogo.DOKill();

            if (!logoActive)
                return;

            loadingLogo.localScale = Vector3.one;

            loadingLogo.DOScale(Vector3.one * logoSizePulse, 0)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                loadingLogo.DOScale(Vector3.one, beatLength);
            });
        }
    }
}