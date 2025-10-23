using System;
using Effects.Simple;
using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Complex.Enemies.SaxPlayer
{
    public class MarkExplosionEffects : MonoBehaviour
    {
        public static MarkExplosionEffects Instance;

        [Header("Objects")]
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private CinemachineImpulseSource impulseSource;

        private CameraShake cameraShake;
        private ControllerRumble controllerRumble;
        private LocalizedShockwave localizedShockwave;

        [Header("Parameters")]
        [SerializeField] private float shockwaveDuration;
        [SerializeField] private float shockwaveSize;
        [SerializeField] private float cameraShakeForce;

        [Header("Controller Rumble Parameters")]
        [SerializeField] private float lowFrequency;
        [SerializeField] private float highFrequency;
        [SerializeField] private float rumbleDuration;

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            cameraShake = FindFirstObjectByType<CameraShake>();
            controllerRumble = FindFirstObjectByType<ControllerRumble>();
            localizedShockwave = FindFirstObjectByType<LocalizedShockwave>();
        }

        public void ApplyEffects(Vector2 position)
        {
            finishedPlaying = false;

            //Invoca efeitos de partícula
            //particles.Play();

            //Faz a câmera tremer
            cameraShake.ApplyEffect(impulseSource, cameraShakeForce, position);

            //Invoca a shockwabe
            localizedShockwave.ApplyEffect(shockwaveDuration, position, shockwaveSize * Vector3.one);

            //Faz o controle tremer um pouco
            controllerRumble.ApplyEffect(lowFrequency, highFrequency, rumbleDuration, position);

            finishedPlaying = true;
        }
    }
}