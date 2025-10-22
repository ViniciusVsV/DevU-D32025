using Effects.Simple;
using Unity.Cinemachine;
using UnityEngine;

namespace Effects.Complex.Objects
{
    public class BlockSmashEffects : MonoBehaviour
    {
        public static BlockSmashEffects Instance;

        [Header("Objects")]
        [SerializeField] private CinemachineImpulseSource impulseSource;

        private CameraShake cameraShake;
        private ControllerRumble controllerRumble;

        [Header("Parameters")]
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
        }

        public void ApplyEffects(bool hittingWall, Vector2 position)
        {
            finishedPlaying = false;

            if (hittingWall)
            {
                //Faz a câmera tremer
                cameraShake.ApplyEffect(impulseSource, cameraShakeForce, position);

                //Faz o controle tremer um pouc
                controllerRumble.ApplyEffect(lowFrequency, highFrequency, rumbleDuration, position);
            }

            finishedPlaying = true;
        }
    }
}