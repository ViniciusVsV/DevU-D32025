using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Effects.Simple
{
    public class BeatAberration : MonoBehaviour, IRythmSyncable
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private Volume volume;
        private ChromaticAberration chromaticAberration;

        [Header("||===== Parameters =====||")]
        [SerializeField] private int beatDelay;
        [SerializeField] private float aberrationAmount;
        [Range(0, 1)][SerializeField] private float beatLengthPercentage;
        private float beatLength;
        private float usedBeatLength;

        private Coroutine coroutine;

        private void Start()
        {
            beatLength = BeatController.Instance.GetBeatLength();
            usedBeatLength = beatLength * beatLengthPercentage;

            volume.profile.TryGet(out chromaticAberration);
        }

        private void OnEnable() { BeatInterval.OnOneBeatHappened += RespondToBeat; }
        private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }

        public void RespondToBeat()
        {
            if (beatDelay > 0)
            {
                beatDelay--;
                return;
            }

            chromaticAberration.intensity.value = aberrationAmount;

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            float elapsedTime = 0;
            float progress;

            while (elapsedTime < usedBeatLength)
            {
                progress = elapsedTime / usedBeatLength;

                chromaticAberration.intensity.value = Mathf.Lerp(aberrationAmount, 0, progress);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            coroutine = null;
        }
    }
}