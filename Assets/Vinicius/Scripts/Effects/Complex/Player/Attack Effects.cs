using System.Collections.Generic;
using Characters.Enemies;
using Effects.Simple;
using Effects.Simple.LightningBolt;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Complex.Player
{
    public class AttackEffects : MonoBehaviour
    {
        public static AttackEffects Instance;

        [Header("Objects")]
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private CinemachineImpulseSource impulseSource;

        private CameraShake cameraShake;
        private LightningBoltsManager lightningBoltManager;
        private ControllerRumble controllerRumble;

        private List<Transform> enemyTransforms = new();

        private Transform playerTransform;

        [Header("Parameters")]
        [SerializeField] private float cameraShakeForce;

        [Header("Controller Rumble Parameters")]
        [SerializeField] private float lowFrequency;
        [SerializeField] private float highFrequency;
        [SerializeField] private float rumbleDuration;

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private bool calledThisFrame;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            NoteSequence.OnSequenceCompletedEffects += ManageCalls;
        }

        private void OnDisable()
        {
            NoteSequence.OnSequenceCompletedEffects -= ManageCalls;
        }

        public void ManageCalls(Transform enemyTransform)
        {
            enemyTransforms.Add(enemyTransform);

            calledThisFrame = true;
        }

        private void Start()
        {
            cameraShake = FindFirstObjectByType<CameraShake>();
            lightningBoltManager = FindFirstObjectByType<LightningBoltsManager>();
            controllerRumble = FindFirstObjectByType<ControllerRumble>();

            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            if (calledThisFrame)
            {
                ApplyEffects();

                enemyTransforms.Clear();

                calledThisFrame = false;
            }
        }

        public void ApplyEffects()
        {
            // Invoca um line renderer, ou alguma outra coisa, para ser um trovão do jogador ao inimigo danificado
            foreach (var tr in enemyTransforms)
                lightningBoltManager.SummonBolt(playerTransform.position, tr.position);

            // Chama um efeito sonoro

            // Invoca efeito de partículas

            // Camera treme um pouco
            cameraShake.ApplyEffect(impulseSource, cameraShakeForce, Vector2.zero);

            // Controle treme um pouco 
            controllerRumble.ApplyEffect(lowFrequency, highFrequency, rumbleDuration, Vector2.zero);
        }
    }
}