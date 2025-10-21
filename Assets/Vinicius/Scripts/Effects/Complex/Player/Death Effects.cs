using System.Collections;
using Effects.Simple;
using NUnit.Framework.Constraints;
using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Complex.Player
{
    public class DeathEffects : MonoBehaviour
    {
        public static DeathEffects Instance;

        [Header("Objects")]
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private CinemachineImpulseSource impulseSource;

        private CameraShake cameraShake;
        private CameraTransition cameraTransition;
        private HitStop hitStop;
        private ControllerRumble controllerRumble;
        private MusicMuffling musicMuffling;
        private FullScreenShockwave fullScreenShockwave;

        [Header("Parameters")]
        [SerializeField] private float hitStopDuration;
        [SerializeField] private float shockwaveDuration;
        [SerializeField] private float musicMufflingDuration;
        [SerializeField] private float transitionDelay;
        [SerializeField] private float transitionDuration;
        [SerializeField] private float cameraShakeForce;

        [Header("Controller Rumble Parameters")]
        [SerializeField] private float lowFrequency;
        [SerializeField] private float highFrequency;
        [SerializeField] private float rumbleDuration;

        [Header("Control Booleans")]
        public bool finishedHitStopping;
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            cameraShake = FindFirstObjectByType<CameraShake>();
            cameraTransition = FindFirstObjectByType<CameraTransition>();
            hitStop = FindFirstObjectByType<HitStop>();
            controllerRumble = FindFirstObjectByType<ControllerRumble>();
            musicMuffling = FindFirstObjectByType<MusicMuffling>();
            fullScreenShockwave = FindFirstObjectByType<FullScreenShockwave>();
        }

        public void ApplyEffects(Vector2 position)
        {
            finishedHitStopping = false;
            finishedPlaying = false;

            StartCoroutine(Routine(position));
        }

        private IEnumerator Routine(Vector2 positionn)
        {
            //Ativa um overlay / vinheta


            //Chama primeira parte do efeito sonoro

            //Aplica abafamento na música
            musicMuffling.ApplyEffect(musicMufflingDuration);

            //Aplica hitstop
            hitStop.ApplyEffect(hitStopDuration);

            while (hitStop.isPlaying)
                yield return null;

            finishedHitStopping = true;

            //Chama segunda parte do efeito sonoro


            //Camera treme
            cameraShake.ApplyEffect(impulseSource, cameraShakeForce, Vector2.zero);

            //Aplica a shockwave
            fullScreenShockwave.ApplyEffect(shockwaveDuration);

            //Da play em aprticulas
            particles.transform.position = positionn;
            particles.Play();

            // Controle treme bastante
            controllerRumble.ApplyEffect(lowFrequency, highFrequency, rumbleDuration);

            // Ativa transição de tela após um tempo
            yield return new WaitForSeconds(transitionDelay);

            cameraTransition.ApplyEffect(transitionDuration, false);

            while (cameraTransition.isPlaying)
                yield return null;

            finishedPlaying = true;
        }
    }
}