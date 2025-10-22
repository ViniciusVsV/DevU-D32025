using System.Collections;
using Effects.Simple;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Effects.Complex.Menus
{
    public class SceneExitEffects : MonoBehaviour
    {
        public static SceneExitEffects Instance;

        [Header("Objects")]
        private MusicMuffling musicMuffling;
        private MusicFade musicFade;
        private CameraTransition cameraTransition;
        private AberrationPulse aberrationPulse;

        [Header("Parameters")]
        [SerializeField] private float musicMufflingDuration;
        [SerializeField] private float aberrationPulseDuration;
        [SerializeField] private float aberrationPulseIntensity;
        [SerializeField] private float musicFadeDuration;
        [SerializeField] private float cameraTransitionDuration;
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
        }

        public void ApplyEffects(string sceneName)
        {
            finishedPlaying = false;

            musicMuffling.ApplyEffect(musicMufflingDuration);
            aberrationPulse.ApplyEffect(aberrationPulseIntensity, aberrationPulseDuration);
            musicFade.FadeOut(musicFadeDuration);

            StartCoroutine(Routine(sceneName));
        }

        private IEnumerator Routine(string sceneName)
        {
            yield return new WaitUntil(() => !musicFade.isPlaying);

            cameraTransition.ApplyEffect(cameraTransitionDuration, true);

            yield return new WaitUntil(() => !cameraTransition.isPlaying);

            yield return new WaitForSeconds(loadDelay);

            SceneManager.LoadScene(sceneName);

            finishedPlaying = true;
        }
    }
}