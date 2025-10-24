using DG.Tweening;
using UnityEngine;

namespace Effects.Simple
{
    public class MusicMuffling : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioLowPassFilter lowPassFilter;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float muffledFrequency = 800f;
        [SerializeField] private float normalFrequency = 22000f;
        [SerializeField] private Ease applyEase = Ease.OutCubic;
        [SerializeField] private Ease removeEase = Ease.InCubic;

        public bool isPlaying;

        private void Awake()
        {
            lowPassFilter.cutoffFrequency = normalFrequency;
        }

        public void ApplyEffect(float duration)
        {
            isPlaying = true;

            DOTween.To(
                () => lowPassFilter.cutoffFrequency,
                x => lowPassFilter.cutoffFrequency = x,
                muffledFrequency,
                duration
            )
            .SetEase(applyEase)
            .SetUpdate(true)
            .OnComplete(() => isPlaying = false);
        }

        public void RemoveEffect(float duration)
        {
            isPlaying = true;

            DOTween.To(
                () => lowPassFilter.cutoffFrequency,
                x => lowPassFilter.cutoffFrequency = x,
                normalFrequency,
                duration
            )
            .SetEase(removeEase)
            .SetUpdate(true)
            .OnComplete(() => isPlaying = false);
        }
    }
}
