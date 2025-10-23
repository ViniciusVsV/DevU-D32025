using System.Collections;
using Effects.Simple;
using UnityEngine;

namespace Effects.Complex.Scenes
{
    public class GameEnterEffects : MonoBehaviour, IRythmSyncable
    {
        public static GameEnterEffects Instance;

        [Header("Objects")]
        private MusicMuffling musicMuffling;
        private MusicFade musicFade;
        private CameraTransition cameraTransition;

        [Header("Parameters")]
        [SerializeField] private float musicDemufflingDuration;
        [SerializeField] private float musicFadeInDuration;
        [SerializeField] private float cameraTransitionDuration;
        [SerializeField] private int beatsToStart;
        [SerializeField] private int beatsToFinish;
        private int beatCounter;

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            musicFade = FindFirstObjectByType<MusicFade>();
            musicMuffling = FindFirstObjectByType<MusicMuffling>();
            cameraTransition = FindFirstObjectByType<CameraTransition>();

            musicFade.FadeOut(0);
            musicMuffling.ApplyEffect(0);
            cameraTransition.ApplyEffect(0, true);
        }

        private void OnEnable() { BeatInterval.OnOneBeatHappened += RespondToBeat; }
        private void OnDisable() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }
        private void OnDestroy() { BeatInterval.OnOneBeatHappened -= RespondToBeat; }

        public void ApplyEffects()
        {
            finishedPlaying = false;

            AudioController.Instance.PlayGameMusic();

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            yield return new WaitWhile(() => musicMuffling == null);

            musicMuffling.RemoveEffect(musicDemufflingDuration);
            musicFade.FadeIn(musicFadeInDuration);

            yield return new WaitUntil(() => beatCounter == beatsToStart);

            cameraTransition.RemoveEffect(cameraTransitionDuration);

            yield return new WaitUntil(() => !cameraTransition.isPlaying);

            finishedPlaying = true;
        }

        public void RespondToBeat()
        {
            beatCounter++;
        }
    }
}