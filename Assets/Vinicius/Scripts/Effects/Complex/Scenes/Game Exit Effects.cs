using System.Collections;
using Effects.Simple;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Effects.Complex.Scenes
{
    public class GameExitEffects : MonoBehaviour
    {
        public static GameExitEffects Instance;

        [Header("Objects")]
        private MusicMuffling musicMuffling;
        private MusicFade musicFade;
        private CameraTransition cameraTransition;
        private AberrationPulse aberrationPulse;
        private TimeSlow timeSlow;

        [Header("Parameters")]
        [SerializeField] private float timeSlowDuration;
        [SerializeField] private float musicMufflingDuration;
        [SerializeField] private float musicFadeOutDuration;
        [SerializeField] private float cameraTransitionDuration;
        [SerializeField] private float aberrationPulseDuration;
        [SerializeField] private float aberrationPulseIntensity;
        [SerializeField] private float loadDelay;

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
            aberrationPulse = FindFirstObjectByType<AberrationPulse>();
            timeSlow = FindFirstObjectByType<TimeSlow>();
        }

        public void ApplyEffects()
        {
            finishedPlaying = false;

            timeSlow.ApplyEffect(timeSlowDuration);
            musicMuffling.ApplyEffect(musicMufflingDuration);
            aberrationPulse.ApplyEffect(aberrationPulseIntensity, aberrationPulseDuration);
            musicFade.FadeOut(musicFadeOutDuration);

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            yield return new WaitUntil(() => !musicFade.isPlaying);

            cameraTransition.ApplyEffect(cameraTransitionDuration, true);

            yield return new WaitUntil(() => !cameraTransition.isPlaying);

            yield return new WaitForSecondsRealtime(loadDelay);

            SceneManager.LoadScene("Main Menu");

            Time.timeScale = 1;

            finishedPlaying = true;
        }
    }
}