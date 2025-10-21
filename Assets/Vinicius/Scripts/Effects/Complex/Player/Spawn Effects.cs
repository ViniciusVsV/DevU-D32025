using System.Collections;
using Effects.Simple;
using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Complex.Player
{
    public class SpawnEffects : MonoBehaviour, IRythmSyncable
    {
        public static SpawnEffects Instance;

        private CameraTransition cameraTransition;

        [Header("Parameters")]
        [SerializeField] private float transitionDuration;
        [SerializeField] private int beatsToStart;
        [SerializeField] private int beatsToFinish;
        private int beatCounter;

        private Vector3 originalDamping;

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            cameraTransition = FindFirstObjectByType<CameraTransition>();
            cameraTransition.ApplyEffect(0, true);
        }

        private void OnEnable() { BeatInterval.OnOneBeatHappened += RespondToBeat; }
        private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }

        public void ApplyEffects()
        {
            finishedPlaying = false;

            AudioController.Instance.PlayGameMusic();

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            while (beatCounter < beatsToStart)
                yield return null;

            cameraTransition.RemoveEffect(transitionDuration);

            while (beatCounter < beatsToFinish)
                yield return null;

            finishedPlaying = true;
        }

        public void RespondToBeat()
        {
            beatCounter++;
        }
    }
}