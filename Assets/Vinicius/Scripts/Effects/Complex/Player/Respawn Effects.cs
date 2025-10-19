using System.Collections;
using Effects.Simple;
using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Complex.Player
{
    public class RespawnEffects : MonoBehaviour
    {
        public static RespawnEffects Instance;

        [Header("Objects")]
        [SerializeField] private CinemachineFollow cinemachineFollow;

        private CameraTransition cameraTransition;
        private MusicMuffling musicMuffling;

        [Header("Parameters")]
        [SerializeField] private float transitionDelay;
        [SerializeField] private float transitionDuration;
        [SerializeField] private float musicDemufflingDuration;
        
        private Vector3 originalDamping;

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;

            originalDamping = cinemachineFollow.TrackerSettings.PositionDamping;
        }

        private void Start()
        {
            cameraTransition = FindFirstObjectByType<CameraTransition>();
            musicMuffling = FindFirstObjectByType<MusicMuffling>();
        }

        public void ApplyEffects()
        {
            finishedPlaying = false;

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            // Desativa o damping do follow da cinemachine
            cinemachineFollow.TrackerSettings.PositionDamping = Vector3.zero;

            yield return new WaitForSeconds(transitionDelay);

            cameraTransition.RemoveEffect(transitionDuration);

            // Reativa o damping
            cinemachineFollow.TrackerSettings.PositionDamping = originalDamping;

            while (cameraTransition.isPlaying)
                yield return null;

            // Volta música ao normal
            musicMuffling.RemoveEffect(musicDemufflingDuration);

            finishedPlaying = true;
        }
    }
}