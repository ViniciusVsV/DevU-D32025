using DG.Tweening;
using UnityEngine;

namespace Effects.Simple
{
    public class MusicMuffling : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioLowPassFilter musicLowPassFilter;
        [SerializeField] private AudioLowPassFilter sfxLowPassFilter;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float muffledFrequency = 800f;
        [SerializeField] private float normalFrequency = 22000f;
        [SerializeField] private Ease applyEase = Ease.OutCubic;
        [SerializeField] private Ease removeEase = Ease.InCubic;

        public bool isPlaying;

        private void Awake()
        {
            musicLowPassFilter.cutoffFrequency = normalFrequency;
            sfxLowPassFilter.cutoffFrequency = normalFrequency;
        }

        public void ApplyEffect(float duration)
        {
            isPlaying = true;

            // Música
            DOTween.To(
                () => musicLowPassFilter.cutoffFrequency,
                x => musicLowPassFilter.cutoffFrequency = x,
                muffledFrequency,
                duration
            ).SetEase(applyEase)
             .SetUpdate(true);

            // SFX
            DOTween.To(
                () => sfxLowPassFilter.cutoffFrequency,
                x => sfxLowPassFilter.cutoffFrequency = x,
                muffledFrequency,
                duration
            ).SetEase(applyEase)
             .SetUpdate(true)
             .OnComplete(() => isPlaying = false);
        }

        public void RemoveEffect(float duration)
        {
            isPlaying = true;

            // Música
            DOTween.To(
                () => musicLowPassFilter.cutoffFrequency,
                x => musicLowPassFilter.cutoffFrequency = x,
                normalFrequency,
                duration
            ).SetEase(removeEase)
             .SetUpdate(true);

            // SFX
            DOTween.To(
                () => sfxLowPassFilter.cutoffFrequency,
                x => sfxLowPassFilter.cutoffFrequency = x,
                normalFrequency,
                duration
            ).SetEase(removeEase)
             .SetUpdate(true)
             .OnComplete(() => isPlaying = false);
        }
    }
}
