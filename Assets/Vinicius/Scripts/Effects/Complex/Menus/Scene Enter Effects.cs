using System.Collections;
using Effects.Simple;
using UnityEngine;

namespace Effects.Complex.Menus
{
    public class SceneEnterEffects : MonoBehaviour
    {
        public static SceneEnterEffects Instance;

        [Header("Objects")]
        private MusicMuffling musicMuffling;
        private MusicFade musicFade;
        private CameraTransition cameraTransition;

        [Header("Parameters")]
        [SerializeField] private float musicMufflingDuration;
        [SerializeField] private float musicFadeDuration;
        [SerializeField] private float cameraTransitionDuration;

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
        }

        public void ApplyEffects()
        {
            finishedPlaying = false;

            musicMuffling.RemoveEffect(musicMufflingDuration);
            musicFade.FadeIn(musicFadeDuration);

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            yield return new WaitUntil(() => !musicFade.isPlaying);

            cameraTransition.RemoveEffect(cameraTransitionDuration);

            yield return new WaitUntil(() => !cameraTransition.isPlaying);

            finishedPlaying = true;
        }
    }
}